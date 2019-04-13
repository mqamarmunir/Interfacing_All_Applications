using Common;
using DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Parsers
{
    public class AU480:IParser
    {
        public void Parse(string data,mi_tinstruments machineSettings)
        {

            var text = data;
            var splitter1 = new char[] { Convert.ToChar(3) };// "D ", "DR", "DH", "DQ", "d ", "DA", "dH", "DE" 
            var splitter2 = new string[] { " " };
            var arrayafter1stseperator = text.Split(splitter1, StringSplitOptions.RemoveEmptyEntries);
            string labid = "";
            using (var _unitOfWork = new UnitOfWork())
            {
                //List<DataModel.mi_tresult> result = new List<DataModel.mi_tresult>();
                foreach (string str1 in arrayafter1stseperator)
                {
                    try
                    {
                        Logger.LogParsedData("this is going to be parsed after first seperator: " + str1);
                        if (str1.Contains("DB") || str1.Contains("DE") || string.IsNullOrEmpty(str1.Trim()) || str1.Length < 41)//skip start and end strings
                            continue;

                        var arrayafter2ndseperator = str1.Substring(0, 40).Split(splitter2, StringSplitOptions.RemoveEmptyEntries);
                        //eventLog1.WriteEntry(str1.Substring(0,40));
                        if (arrayafter2ndseperator.Length > 3)
                        {
                            labid = arrayafter2ndseperator[3];
                            //eventLog1.WriteEntry(str1.Substring(0, 40));
                            //string testsandresults = str1.Substring(40).TrimStart().Replace("E", "");
                            string testsandresults = str1.Substring(45);
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
                                            InstrumentId = machineSettings.InstrumentID,
                                            Result = resultsingle,
                                            Status = "N"
                                        };
                                        var resultserialized = Newtonsoft.Json.JsonConvert.SerializeObject(objresult);
                                        // Logger.(MachineID + " Serialized result: " + resultserialized);
                                        _unitOfWork.ResultsRepository.Insert(objresult);

                                    }
                                }
                            }

                        }
                        else
                        {
                            Logger.LogExceptions("String not correct. It can not be processed: " + str1);
                        }
                    }

                    catch (Exception ee)
                    {
                        Logger.LogExceptions("Error in following line: " + str1 + "-----------" + ee.ToString());
                    }

                }
                try
                {

                    _unitOfWork.Save();
                    //eventLog1.WriteEntry("Result Data saved to database", EventLogEntryType.SuccessAudit);
                }
                catch (Exception ee)
                {
                    Logger.LogExceptions("On Saving to local results table: " + ee.ToString());
                    //log.Error("On Saving:", ee);
                }
            }

        }
    }
}
