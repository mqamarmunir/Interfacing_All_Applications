using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Configuration;
using MySql.Data.MySqlClient;
using BusinessLayer;
using System.Data.OleDb;
using DataModel;
using BusinessEntities;
using System.Threading.Tasks;

namespace TestService
{
    public partial class Service1 : ServiceBase
    {
        private readonly UnitOfWork _unitOfWork;
        delegate void _remoteinserter(DataTable dt);
        private StringBuilder sb_port1 = new StringBuilder();
        private StringBuilder sb_port2 = new StringBuilder();
        private Timer timer;
        private Timer timer_deleteolddata;
        public Service1()
        {
            InitializeComponent();
            _unitOfWork = new UnitOfWork();
            //eventLog1.LogDisplayName = "TestService";
            if (!System.Diagnostics.EventLog.SourceExists("WindowCopyServiceSource"))
            {
                System.Diagnostics.EventLog.CreateEventSource(
                    "WindowCopyServiceSource", "WindowCopyServiceSourcelog");
            }
            eventLog1.Source = "WindowCopyServiceSource";
            eventLog1.Log = "WindowCopyServiceSourcelog";

        }

        protected override void OnStart(string[] args)
        {
            eventLog1.WriteEntry("In Onstart");
            eventLog1.ModifyOverflowPolicy(OverflowAction.OverwriteAsNeeded, 1);

            serialPort1.Open();
            //   Main(null, null);
            this.timer = new System.Timers.Timer(60000D);  // 30000 milliseconds = 30 seconds
            this.timer.AutoReset = true;
            this.timer.Elapsed += new System.Timers.ElapsedEventHandler(this.Main);
            if(System.Configuration.ConfigurationSettings.AppSettings["IsUpdateRemoteDatabase"].ToString().Trim()=="Y")
                this.timer.Start();

            this.timer_deleteolddata = new System.Timers.Timer(24 * 60 * 60 * 1000);//Run this timer method after one day
            this.timer_deleteolddata.AutoReset = true;
            this.timer_deleteolddata.Elapsed += new System.Timers.ElapsedEventHandler(this.Deleteolddate);
        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {

            string data = "";
            try
            {
                data = serialPort1.ReadExisting();
                if (data.Length > 0)
                {
                    var thismachinesettings = _unitOfWork.InstrumentsRepository.GetSingle(x => x.Active == "Y" && x.PORT == "COM1");
                    string MachineID = thismachinesettings.CliqInstrumentID.ToString().Trim();
                    if (!String.IsNullOrEmpty(thismachinesettings.Acknowledgement_code))
                        serialPort1.Write(new byte[] { 0x06 }, 0, 1);//send Ack to machine
                    sb_port1.Append(data);
                    //eventLog1.WriteEntry(data);
                    System.IO.File.AppendAllText(System.Configuration.ConfigurationSettings.AppSettings["ReceivedDataLogFile"].ToString().Trim(), data);
                    if (sb_port1.ToString().Split(new string[] { "D ", "DR", "DH", "DQ", "d ", "DA", "dH", "DE" }, StringSplitOptions.RemoveEmptyEntries).Length>0)//For Au480 temporary//L|1 is a terminator record according to astm standards
                    {
                        ///if the recieved string contains the terminator
                        ///then parse the record and Clear the string
                        ///Builder for next Record.
                        ///

                        eventLog1.WriteEntry("data after Terminator: " + sb_port1.ToString());
                        string parsingdata = sb_port1.ToString();
                        sb_port1.Clear();
                         
                       // System.Threading.Thread t = new System.Threading.Thread(Parsethisandinsert(parsingdata,thismachinesettings.ParsingAlgorithm,MachineID));
                        Parsethisandinsert(parsingdata, thismachinesettings.ParsingAlgorithm, MachineID);
                       // parsingdata = string.Empty;


                    }
                    //else if (sb_port1.ToString().Split(new char[1] { Convert.ToChar(3) }).Length > 0)
                    //{
                    //
                    //    eventLog1.WriteEntry("data after Terminator2: " + sb_port1.ToString());
                    //    Parsethisandinsert(sb_port1.ToString().Substring(0, sb_port1.ToString().LastIndexOf(Convert.ToChar(3))), thismachinesettings.ParsingAlgorithm, MachineID);
                    //}

                }


            }
            catch (Exception ee)
            {
                eventLog1.WriteEntry("Following Exception occured in serialport datarecieved method please check." + ee.ToString());
            }

        }
        private void Parsethisandinsert(string data, int Parsingalgorithm, string MachineID)
        {

            switch (Parsingalgorithm)
            {
                ///According to ASTM standard 
                ///tested on 
                ///sysmex xs800i,cobase411,cobasu411(urine analyzer)
                ///
                case 1:
                    ParseASTMData(data, MachineID);
                    break;//case 1 ends here
                case 2://AU480 Beckman
                    ParseAu480(data, MachineID);
                    break;
            }
        }
        private void ParseASTMData(string data, string MachineID)
        {
            string datetime = "";
            string labid = "";
            string attribresult = "";
            string attribcode = "";
            string patid = "";

            string[] sep1 = { "L|1" };

            char[] sep2 = { Convert.ToChar(13) };
            string[] abc = data.Split(sep1, StringSplitOptions.RemoveEmptyEntries);
            char[] sep3 = { '|' };
            char[] sep4 = { '^' };
            for (int i = 0; i <= abc.GetUpperBound(0); i++)
            {
                string[] def = abc[i].Split(sep2);
                for (int j = 0; j < def.GetUpperBound(0); j++)
                {
                    if (def[j].Contains("H|") && !def[j].Contains("O|") && !def[j].Contains("R|"))
                    {
                        // Get date time
                        // def[j] = def[j].Replace("||", "");

                        string[] header = def[j].Split(sep3);
                        try
                        {
                            datetime = header[13].ToString();
                        }
                        catch { }
                    }
                    else if (def[j].Contains("P|1") && (!def[j].Contains("O|") || def[j].IndexOf("P|") < def[j].IndexOf("O|")) && (!def[j].Contains("R|") || def[j].IndexOf("P|") < def[j].IndexOf("R|")))
                    {
                        string[] patinfo = def[j].Split(sep3);
                        try
                        {
                            patid = patinfo[4].ToString();
                        }
                        catch (Exception ee)
                        {
                            eventLog1.WriteEntry("Exception on getting Patientid: " + ee.ToString());
                        }
                    }
                    else if (def[j].Contains("O|") && def[j].Contains("R|") && def[j].IndexOf("O|") < def[j].IndexOf("R|"))
                    {
                        ///Get lab ID
                        string[] order = def[j].Split(sep3);
                        labid = order[2].ToString();
                        if (labid.Contains("^"))
                        {
                            string[] splitlabid = labid.Split(sep4);
                            labid = splitlabid[1].ToString().Trim();
                        }
                    }
                    else if (def[j].Contains("R|"))
                    {
                        //Get Result
                        string[] result = def[j].Split(sep3);
                        attribresult = result[3].ToString();
                        string[] attcode = result[2].Split(sep4);
                        //writeLog("Result[2]: " + result[2]);
                        if (attcode[3] != "")
                        {
                            attribcode = attcode[3].ToString();
                        }
                        else
                        {
                            attribcode = attcode[4].ToString();
                        }
                        if (attribcode.Contains(@"/"))
                        {
                            attribcode = attribcode.Replace(@"/", "");
                        }
                        if (attribcode.ToLower() == "wbc" || attribcode.ToLower() == "plt")
                        {

                            try
                            {
                                attribresult = ((Convert.ToDecimal(attribresult)) * 1000).ToString();
                                if (attribresult.Contains("."))
                                {
                                    attribresult = attribresult.Substring(0, attribresult.IndexOf('.'));
                                }
                            }
                            catch (Exception ee)
                            {
                                eventLog1.WriteEntry("Error Converting Result: " + attribresult);
                            }


                        }
                        else if (attribcode.ToLower().Equals("900") || attribcode.ToLower().Equals("999") || attribcode.ToLower().Equals("102"))
                        {
                            if (attribresult.Contains("-1^"))
                            {
                                attribresult = attribresult.Replace("-1^", "Negative  \r\n");

                            }
                            else if (attribresult.Contains("1^"))
                            {
                                attribresult = attribresult.Replace("1^", "Positive  \r\n");

                            }

                        }
                        else if (attribcode.ToLower().Equals("eo%") || attribcode.ToLower().Equals("mono%") || attribcode.ToLower().Equals("neut%") || attribcode.ToLower().Equals("lymph%"))
                        {
                            try
                            {
                                attribresult = Math.Round(Convert.ToDecimal(attribresult)).ToString().Trim();
                                if (attribresult.Contains("."))
                                {
                                    attribresult = attribresult.Substring(0, attribresult.IndexOf('.'));
                                }
                            }
                            catch
                            { }
                        }
                        if (labid == "")
                        {
                            labid = patid;
                        }

                        var objresult = new DataModel.mi_tresult
                        {
                            BookingID = labid,
                            AttributeID = attribcode,
                            ClientID = System.Configuration.ConfigurationSettings.AppSettings["BranchID"].ToString().Trim(),
                            EnteredBy = 1,
                            EnteredOn = System.DateTime.Now,//.ToString("yyyy-MM-dd hh:mm:ss tt"),
                            machinename = MachineID,
                            Result = attribresult,
                            Status = "N"
                        };

                        _unitOfWork.ResultsRepository.Insert(objresult);
                        //string pars = labid + "," + attribcode + "," + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "," + attribresult;
                        ////writeLog("parsed data: " + pars);
                        ////eventLog1.WriteEntry("parsed string:" + pars);
                        //InsertBooking(pars);
                    }


                }
            }
            try
            {
                _unitOfWork.Save();
            }
            catch (Exception ee)
            {
                eventLog1.WriteEntry("On Saving to local results table: " + ee.ToString(), EventLogEntryType.Error);
            }

        }
        private void ParseAu480(string data, string MachineID)
        {

            var text = data;
            var splitter1 = new string[] { "D ", "DR", "DH", "DQ", "d ", "DA", "dH", "DE" };
            var splitter2 = new string[] { " " };
            var arrayafter1stseperator = text.Split(splitter1, StringSplitOptions.RemoveEmptyEntries);
            string labid = "";

            //List<DataModel.mi_tresult> result = new List<DataModel.mi_tresult>();
            foreach (string str1 in arrayafter1stseperator)
            {
                try
                {
                    eventLog1.WriteEntry("this is going to be parsed after first seperator: " + str1);
                    if (str1.Contains("DB") || str1.Contains("DE") || string.IsNullOrEmpty(str1.Trim()) || str1.Length < 41)//skip start and end strings
                        continue;

                    var arrayafter2ndseperator = str1.Substring(0, 40).Split(splitter2, StringSplitOptions.RemoveEmptyEntries);
                    //eventLog1.WriteEntry(str1.Substring(0,40));
                    if (arrayafter2ndseperator.Length == 3)
                    {
                        labid = arrayafter2ndseperator[2];
                        //eventLog1.WriteEntry(str1.Substring(0, 40));
                        string testsandresults = str1.Substring(40).TrimStart().Replace("E", "");

                        int thisordertestscount = Convert.ToInt32(Math.Round(testsandresults.Length / 11.0));
                        string[] indtestanditsresult = new string[thisordertestscount];
                        for (int i = 0; i < thisordertestscount; i++)
                        {
                            if (11 * i + 11 <= testsandresults.Length)
                                indtestanditsresult[i] = testsandresults.Substring(11 * i, 11);
                            else
                                indtestanditsresult[i] = testsandresults.Substring(11 * i);
                        }
                        foreach (string thistestandresult in indtestanditsresult)
                        {


                            if (thistestandresult.Length > 1)
                            {
                                string machinetestcode = thistestandresult.Substring(0, 3).Trim();
                                string resultsingle = thistestandresult.Substring(3, 6).Trim();

                                clsBLMain objMain = new clsBLMain();
                                objMain.BookingID = labid;
                                objMain.AttributeID = machinetestcode;
                                objMain.Result = resultsingle;
                                DataView dv = objMain.GetAll(8);
                                if (dv.Count > 0)
                                    continue;
                                else
                                {
                                    var objresult = new DataModel.mi_tresult
                                    {
                                        BookingID = labid,
                                        AttributeID = machinetestcode,
                                        ClientID = System.Configuration.ConfigurationSettings.AppSettings["BranchID"].ToString().Trim(),
                                        EnteredBy = 1,
                                        EnteredOn = System.DateTime.Now,//.ToString("yyyy-MM-dd hh:mm:ss tt"),
                                        machinename = MachineID,
                                        Result = resultsingle,
                                        Status = "N"
                                    };
                                    var resultserialized = Newtonsoft.Json.JsonConvert.SerializeObject(objresult);
                                    eventLog1.WriteEntry("Serialized result: " + resultserialized);
                                    _unitOfWork.ResultsRepository.Insert(objresult);

                                }
                            }
                        }

                    }
                    else
                    {
                        eventLog1.WriteEntry("String not correct. It can not be processed: " + str1);
                    }
                }

                catch (Exception ee)
                {
                    eventLog1.WriteEntry("Error in following line: " + str1 + "-----------" + ee.ToString(), EventLogEntryType.Error);
                }

            }
            try
            {

                _unitOfWork.Save();
                //eventLog1.WriteEntry("Result Data saved to database", EventLogEntryType.SuccessAudit);
            }
            catch (Exception ee)
            {
                eventLog1.WriteEntry("On Saving to local results table: " + ee.ToString(), EventLogEntryType.Error);
                //log.Error("On Saving:", ee);
            }

        }
        private async void Main(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Stop();
            //eventLog1.WriteEntry("In remote Sending method.");
            // WindowsImpersonationContext impersonationContext = null;
            //// Program g = new Program();
            //getservercredetials();
            //if (!sendingmethod.ToLower().Equals("direct db transfer"))
            //{

            //    SendbyXMLFiles();

            //}
            ////else
            //{
            #region Web Service Methodology
            clsBLMain objMai = new clsBLMain();
            DataView dv = objMai.GetAll(7);
            if (dv.Count > 0)
            {
                //eventLog1.WriteEntry("Found results:" + dv.Count.ToString());
                try
                {
                    List<cliqresults> lstresults = new List<cliqresults>();
                    for (int i = 0; i < dv.Count; i++)
                    {
                        lstresults.Add(new cliqresults
                        {
                            ResultID = Convert.ToInt32(dv[i]["ResultID"].ToString().Trim()),
                            BookingID = dv[i]["BookingID"].ToString().Trim(),
                            ClientID = dv[i]["ClientID"].ToString().Trim(),
                            CliqAttributeID = dv[i]["CliqAttributeID"].ToString().Trim(),
                            CliqTestID = dv[i]["CliqTestID"].ToString().Trim(),
                            MachineID = dv[i]["MachineID"].ToString().Trim(),
                            Result = dv[i]["Result"].ToString().Trim(),
                            MachineAttributeCode = dv[i]["MachineAttributeCode"].ToString().Trim()

                        });
                    }

                    var json = Newtonsoft.Json.JsonConvert.SerializeObject(lstresults.Select(x => new { BookingID = x.BookingID, ClientID = x.ClientID, CliqAttributeID = x.CliqAttributeID, CliqTestID = x.CliqTestID, MachineID = x.MachineID, Result = x.Result }).ToList());
                    var content = await Helper.CallCliqApi(System.Configuration.ConfigurationSettings.AppSettings["WebServicebasePath"].ToString().Trim() + "/ricapi/site/curl_data?str=" + json.ToString().Trim());

                    if (content.Length > 0)
                    {

                        var cliqresultresponse = Newtonsoft.Json.JsonConvert.DeserializeObject<List<CliqResultResponse>>(content);

                        clsBLMain objMain = null;
                        foreach (var result in lstresults)
                        {
                            objMain = new clsBLMain();
                            objMain.status = "Y";
                            objMain.Sentto = System.Configuration.ConfigurationSettings.AppSettings["WebServicebasePath"].ToString().Trim() + "/ricapi/site/curl_data";
                            objMain.Senton = System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                            objMain.ResultID = result.ResultID.ToString();
                            try
                            {
                                objMain.Update();

                            }
                            catch (Exception ee)
                            {
                                eventLog1.WriteEntry("Error while updating local record id: " + result.ResultID.ToString() + "-------" + ee.ToString(), EventLogEntryType.Error);
                            }

                        }


                    }
                    else
                    {
                        clsBLMain objMain = null;
                        eventLog1.WriteEntry("Some Problem occured in remote call. Call: " + System.Configuration.ConfigurationSettings.AppSettings["WebServicebasePath"].ToString().Trim() + "/ricapi/site/curl_data?str=" + json.ToString().Trim(), EventLogEntryType.FailureAudit);
                        foreach (var result in lstresults)
                        {
                            objMain = new clsBLMain();
                            objMain.status = "X";
                            objMain.Sentto = System.Configuration.ConfigurationSettings.AppSettings["WebServicebasePath"].ToString().Trim() + "/ricapi/site/curl_data";
                            objMain.Senton = System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                            objMain.ResultID = result.ResultID.ToString();
                            try
                            {
                                objMain.Update();

                            }
                            catch (Exception ee)
                            {
                                eventLog1.WriteEntry("Error while updating local record id: " + result.ResultID.ToString() + "-------" + ee.ToString(), EventLogEntryType.Error);
                            }

                        }
                    }
                }
                catch (Exception ee)
                {
                    eventLog1.WriteEntry(ee.ToString(), EventLogEntryType.Error);
                    // MessageBox.Show(ee.Message);
                }
                finally
                {
                    timer.Start();
                }


            }
            else
            {
                eventLog1.WriteEntry("No pending results");
                timer.Start();
            }

            #endregion
            #region Old methodology
            //DataTable dt = getDatatable();
            //eventLog1.WriteEntry(dt.Rows.Count.ToString() + " new rows found");
            //if (dt != null)
            //{
            //    _remoteinserter ri = null;
            //    IAsyncResult _result;// = new IAsyncResult;

            //    ri = insertRemoteData;
            //    _result = ri.BeginInvoke(dt, new AsyncCallback(callbackmethod), dt.Rows[0]["Bookingid"].ToString().Trim());
            //    eventLog1.WriteEntry("Now filling remote database");
            //    // WriteXml(dt);

            //    //DoWork();
            //}
            #endregion

        }

        protected override void OnStop()
        {
            //eventLog1.WriteEntry("In on Stop");
            eventLog1.WriteEntry("In on stop");
            serialPort1.Close();
            //if (serialPort2.IsOpen)
            //{
            //    serialPort2.Close();
            //}
            eventLog1.Clear();
        }

        private void Deleteolddate(object sender, ElapsedEventArgs e)
        {
            clsBLMain _main = new clsBLMain();
            if (_main.Deleteolddata())
            {
                eventLog1.WriteEntry("4 days old data successfully deleted");

            }
            else
            {
                eventLog1.WriteEntry("Error while deleting old data: " + _main.Error);
            }
        }



    }
}
