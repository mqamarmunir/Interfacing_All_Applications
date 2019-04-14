using Common;
using DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.Parsers
{
    public class BeckManLH750:IParser
    {
        public void Parse(string data, mi_tinstruments machineSettings)
        {
            Logger.LogReceivedData(machineSettings.Instrument_Name.ToString(),data);

            var x = data.Split(new string[1] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var validentries = new string[] { "WBC", "LY#", "MO#", "BA#", "EO#", "RBC", "HGB", "HCT", "MCV", "MCH", "MCHC", "RDW", "PLT", "MPV", "PCT", "RDW", "LY%", "MO%", "EO%", "BA%", "NE%", "NE#" };
            string labid = x.Where(v => v.StartsWith("PID ")).Count() > 0 ? x.Where(v => v.StartsWith("PID ")).FirstOrDefault().Substring(4) : "";
            using (var _unitOfWork = new UnitOfWork())
            {
                foreach (string s in x)
                {
                    try
                    {
                        bool isprocessingrequired = false;
                        foreach (string validattribute in validentries)
                        {

                            if (s.StartsWith(validattribute))
                            {
                                isprocessingrequired = true;
                                break;
                            }
                        }
                        if (isprocessingrequired)
                        {

                            var attrib = s.Substring(0, 4);
                            var res = s.Length > 10 ? s.Substring(4, 6) : s.Substring(4);

                            clsBLMain objMain = new clsBLMain();
                            objMain.BookingID = labid;
                            objMain.AttributeID = attrib;
                            objMain.InstrumentID = machineSettings.InstrumentID;
                            objMain.Result = res.Length > 0 ? Convert.ToString(Regex.Split(res, @"[^0-9\.-]+").Where(c => c != "." && c.Trim() != "").FirstOrDefault()) : "";
                            DataView dv = objMain.GetAll(8);
                            if (dv.Count > 0)
                                continue;
                            var objresult = new DataModel.mi_tresult
                            {
                                BookingID = labid,
                                AttributeID = attrib,
                                ClientID = System.Configuration.ConfigurationSettings.AppSettings["BranchID"].ToString().Trim(),
                                EnteredBy = 1,
                                EnteredOn = System.DateTime.Now,//.ToString("yyyy-MM-dd hh:mm:ss tt"),
                                InstrumentId = machineSettings.InstrumentID,
                                Result = res.Length > 0 ? Convert.ToString(Regex.Split(res, @"[^0-9\.-]+").Where(c => c != "." && c.Trim() != "").FirstOrDefault()).Trim() : "",
                                Status = "N"
                            };
                            //var resultserialized = Newtonsoft.Json.JsonConvert.SerializeObject(objresult);
                            // eventLog1.WriteEntry(MachineID + " Serialized result: " + resultserialized);
                            _unitOfWork.ResultsRepository.Insert(objresult);



                            // System.IO.File.AppendAllText("E:\\Parsedresults.txt", xxxx + "\r\n");

                        }

                    }
                    catch (Exception ee)
                    {
                        Logger.LogExceptions("Error in following line: " + s + "-----------" + ee.ToString());
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
