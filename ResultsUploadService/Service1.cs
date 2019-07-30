using BusinessEntities;
using BusinessLayer;
using Common;
using DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace ResultsUploadService
{
    public partial class Service1 : ServiceBase
    {
        private Timer timer;
        private Timer deleteOldData_Timer;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {

            this.timer = new System.Timers.Timer(30000D);  // 30000 milliseconds = 30 seconds
            this.timer.AutoReset = true;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.UpdateRemoteDatabase);
            if (System.Configuration.ConfigurationSettings.AppSettings["IsUpdateRemoteDatabase"].ToString().Trim() == "Y")
                this.timer.Start();
         
            DeleteOldData_Timer_Elapsed(null, null);
            deleteOldData_Timer = new Timer(3600000D);
            deleteOldData_Timer.AutoReset = true;
            deleteOldData_Timer.Elapsed += DeleteOldData_Timer_Elapsed;
            deleteOldData_Timer.Start();
        }

        private void DeleteOldData_Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {

           
            Logger.LogTimerExecution("Delete Timer called");
            clsBLMain _main = new clsBLMain();
            if (_main.Deleteolddata())
            {
                Logger.LogTimerExecution("Records older than threshold time deleted");

            }
            else
            {
                Logger.LogTimerExecution("Error Deleting Data: " + _main.Error);
            }
            }
            catch (Exception ee)
            {

                Logger.LogExceptions(ee.ToString());
            }
        }

        private void UpdateRemoteDatabase(object sender, ElapsedEventArgs e)
        {
            //timer.Stop();
            #region Web Service Methodology
            try
            {
                clsBLMain objMai = new clsBLMain();

                var lstresults = objMai.GetAll<cliqresultsNew>(1).ToList();

                if (lstresults.Count > 0)
                {
                    Logger.LogTimerExecution("Found results: " + lstresults.Count.ToString());



                    var groupedresults = (from p in lstresults
                                          group p by new { p.BookingID, p.ClientID } into g
                                          select new cliqResultsBookingwise
                                          {
                                              branch_id = g.Key.ClientID,

                                              order_no = g.Key.BookingID,
                                              data = g.Select(c => new cliqResultBookingwiseDetail
                                              {
                                                  attribute_id = c.MachineAttributeCode,
                                                  attribute_result = c.Result,
                                                  machine_id = c.CliqMachineID
                                              }).ToList()
                                          }
                                        );

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(groupedresults);
                    Logger.LogSentDataToServer(json);
                    var content = Helper.PostResultsToCliq(System.Configuration.ConfigurationSettings.AppSettings["WebServicebasePath"].ToString().Trim(), json);
                    Logger.LogReceivedDataFromServer(Newtonsoft.Json.JsonConvert.SerializeObject(new { content.StatusCode, content.Content, content.StatusDescription }));
                    if (content.IsSuccessful)
                    {

                        //var cliqresultresponse = Newtonsoft.Json.JsonConvert.DeserializeObject<CliqResultResponseNew>(content.Content);

                        clsBLMain objMain = new clsBLMain();
                        foreach (var result in lstresults)
                        {
                            //objMain = new clsBLMain();
                            objMain.status = "Y";
                            objMain.Sentto = System.Configuration.ConfigurationSettings.AppSettings["WebServicebasePath"].ToString().Trim();
                            objMain.Senton = System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                            objMain.ResultID = result.ResultID.ToString();
                            try
                            {
                                objMain.Update();

                            }
                            catch (Exception ee)
                            {
                                Logger.LogExceptions(ee.ToString());
                                // Console.WriteLine("Error while updating local record id: " + result.ResultID.ToString() + "-------" + ee.ToString());
                            }

                        }


                    }
                    else
                    {
                        clsBLMain objMain = new clsBLMain();
                        //Console.WriteLine("Some Problem occured in remote call. Call: " + System.Configuration.ConfigurationSettings.AppSettings["WebServicebasePath"].ToString().Trim() + "/ricapi/site/curl_data?str=" + json.ToString().Trim());
                        foreach (var result in lstresults)
                        {
                            //objMain = new clsBLMain();
                            objMain.status = "X";
                            objMain.Sentto = System.Configuration.ConfigurationSettings.AppSettings["WebServicebasePath"].ToString().Trim();
                            objMain.Senton = System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                            objMain.ResultID = result.ResultID.ToString();
                            try
                            {
                                if (!objMain.Update())
                                {
                                    Logger.LogExceptions(objMain.Error);

                                }

                            }
                            catch (Exception ee)
                            {
                                Logger.LogExceptions(ee.ToString());

                                Console.WriteLine("Error while updating local record id: " + result.ResultID.ToString() + "-------" + ee.ToString());
                            }


                        }

                    }
                }





                else
                {
                    Logger.LogTimerExecution("No pending results");
                    //timer.Start();
                }
            }
            catch (Exception ee)
            {
                Logger.LogExceptions(ee.ToString());
            }

            #endregion
        }

        protected override void OnStop()
        {
            timer.Stop();
            timer.Close();
            GC.Collect();
        }
    }
}
