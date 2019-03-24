using Common;
using DataModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BusinessLayer.Parsers
{
    public class Parser
    {
        private static UnitOfWork _unitOfWork = new UnitOfWork();
        public  void ParseASTMData(string data, long MachineID)
        {
            string datetime = "";
            string labid = "";
            string attribresult = "";
            string attribcode = "";
            string patid = "";
            string machineName = "";

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
                            machineName = header[4].Split(sep4)[0].Trim();
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
                            Logger.LogExceptions("\r\n" + ee.ToString());
                            Console.WriteLine("Exception on getting Patientid: " + ee.ToString());
                        }
                    }
                    else if (def[j].Contains("O|") && def[j].Contains("R|") && def[j].IndexOf("O|") < def[j].IndexOf("R|"))
                    {
                        ///Get lab ID
                        string[] order = def[j].Split(sep3);
                        labid = order[2].ToString();
                        if (String.IsNullOrEmpty(labid) && order.Length > 3)
                        {
                            labid = order[3];
                        }
                        if (labid.Contains("^"))
                        {
                            string[] splitlabid = labid.Split(sep4);
                            labid = splitlabid[1].ToString().Trim().Length < 4 ? splitlabid[2].Trim() : splitlabid[1].Trim();

                        }

                    }
                    else if (def[j].Length > 5 && def[j].Substring(3, 2).Equals("O|"))
                    {
                        labid = def[j].Split(sep3)[3].ToString();
                        if (labid.Contains("^"))
                            labid = labid.Split(sep4)[2].ToString().Trim();
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
                                Logger.LogExceptions("\r\n" + ee.ToString());
                                Console.WriteLine("Error Converting Result: " + attribresult);
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

                        clsBLMain objMain = new clsBLMain();
                        objMain.BookingID = labid;
                        objMain.AttributeID = attribcode;
                        objMain.Result = attribresult;
                        objMain.InstrumentID = MachineID;

                        DataView dv = objMain.GetAll(8);
                        if (dv.Count > 0)
                            continue;


                        var objresult = new DataModel.mi_tresult
                        {
                            BookingID = labid,
                            AttributeID = attribcode,
                            ClientID = System.Configuration.ConfigurationSettings.AppSettings["BranchID"].ToString().Trim(),
                            EnteredBy = 1,
                            EnteredOn = System.DateTime.Now,//.ToString("yyyy-MM-dd hh:mm:ss tt"),
                            InstrumentId = MachineID,
                            Result = attribresult,
                            Status = "N"
                        };
                        //var resultserialized = Newtonsoft.Json.JsonConvert.SerializeObject(objresult);
                        //Console.WriteLine(MachineID + " Serialized result: " + resultserialized);
                        _unitOfWork.ResultsRepository.Insert(objresult);

                        //string pars = labid + "," + attribcode + "," + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "," + attribresult;
                        ////writeLog("parsed data: " + pars);
                        ////Console.WriteLine("parsed string:" + pars);
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
                Logger.LogExceptions("\r\n" + ee.ToString());
               
            }

        }
        private void ParseAu480(string data, long MachineID)
        {

            var text = data;
            var splitter1 = new char[] { Convert.ToChar(3) };// "D ", "DR", "DH", "DQ", "d ", "DA", "dH", "DE" 
            var splitter2 = new string[] { " " };
            var arrayafter1stseperator = text.Split(splitter1, StringSplitOptions.RemoveEmptyEntries);
            string labid = "";

            //List<DataModel.mi_tresult> result = new List<DataModel.mi_tresult>();
            foreach (string str1 in arrayafter1stseperator)
            {
                try
                {
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
                                        InstrumentId = MachineID,
                                        Result = resultsingle,
                                        Status = "N"
                                    };
                                    //var resultserialized = Newtonsoft.Json.JsonConvert.SerializeObject(objresult);
                                    //eventLog1.WriteEntry(MachineID + " Serialized result: " + resultserialized);
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

        private void ParseBeckManLH750(string data, long MachineID)
        {
            var x = data.Split(new string[1] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            var validentries = new string[] { "WBC", "LY#", "MO#", "BA#", "EO#", "RBC", "HGB", "HCT", "MCV", "MCH", "MCHC", "RDW", "PLT", "MPV", "PCT", "RDW", "LY%", "MO%", "EO%", "BA%", "NE%", "NE#" };
            string labid = x.Where(v => v.StartsWith("PID ")).Count() > 0 ? x.Where(v => v.StartsWith("PID ")).FirstOrDefault().Substring(4) : "";
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
                        objMain.InstrumentID = MachineID;
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
                            InstrumentId = MachineID,
                            Result = res.Length > 0 ? Convert.ToString(Regex.Split(res, @"[^0-9\.-]+").Where(c => c != "." && c.Trim() != "").FirstOrDefault()).Trim() : "",
                            Status = "N"
                        };
                       
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
