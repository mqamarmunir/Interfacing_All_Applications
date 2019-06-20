using Common;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Parsers
{
    public class ASTM_CellDyn : IParser
    {
        public void Parse(string data, mi_tinstruments machineSettings)
        {
            try
            {
                long MachineID = machineSettings.InstrumentID;
                string datetime = "";
                string labid = "";
                string attribresult = "";
                string attribcode = "";
                string patid = "";
                string machineName = "";

                string[] sep1 = { machineSettings.RecordTerminator };

                char[] sep2 = { Convert.ToChar(13) };
                string[] abc = data.Split(sep1, StringSplitOptions.RemoveEmptyEntries);
                char[] sep3 = { '|' };
                char[] sep4 = { '^' };

                using (var _unitOfWork = new UnitOfWork())
                {

                    for (int i = 0; i <= abc.GetUpperBound(0); i++)
                    {
                        string[] def = abc[i].Split(sep2);
                        for (int j = 0; j < def.GetUpperBound(0); j++)
                        {
                            try
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
                                        patid = string.IsNullOrEmpty(patinfo[4].ToString()) ? patinfo[3].ToString() : patinfo[4].ToString().Trim();
                                    }
                                    catch (Exception ee)
                                    {
                                        Logger.LogExceptions(ee.ToString());
                                        //Console.WriteLine("Exception on getting Patientid: " + ee.ToString());
                                    }
                                }
                                else if (def[j].Length > 5 && (def[j].Substring(3, 2).Equals("O|") || def[j].StartsWith("O|")))
                                {
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
                                    labid = labid.Trim();
                                    //labid = def[j].Split(sep3)[3].ToString();
                                    //if (labid.Contains("^"))
                                    //    labid = labid.Split(sep4)[2].ToString().Trim();
                                }
                                else if (def[j].Contains("R|"))
                                {
                                    //Get Result
                                    string[] result = def[j].Split(sep3);
                                    attribresult = result[3].ToString();
                                   
                                    string[] attcode = result[2].Split(sep4);
                                    attribcode = attcode[6];
                                    //writeLog("Result[2]: " + result[2]);


                                    if (attribresult.Trim().StartsWith("."))
                                    {
                                        attribresult = "0" + attribresult;
                                    }
                                    else if (attribresult.EndsWith("."))
                                    {
                                        attribresult = attribresult.TrimEnd('.');
                                    }
                                    if (string.IsNullOrEmpty(labid))
                                    {
                                        labid = patid.Trim();
                                    }
                                    if (string.IsNullOrEmpty(labid))
                                    {
                                        ///
                                        /// 
                                        /// 
                                        ///Generate Notification here
                                        ///
                                        break;//No neeed to insert empty booking ids
                                    }
                                    if (string.IsNullOrEmpty(attribresult))
                                    {
                                        continue;//no need to insert and process attributes with emty results

                                    }
                                    //try
                                    //{

                                    //    var sameResultExists = _unitOfWork.ResultsRepository.GetManyQueryable(x => x.BookingID == labid && x.AttributeID == attribcode && x.Result == attribresult && x.InstrumentId == MachineID).Any();
                                    //    if (sameResultExists)
                                    //        continue;
                                    //}
                                    //catch (Exception ee)
                                    //{
                                    //    Logger.LogExceptions(ee.ToString());
                                    //}
                                    //    clsBLMain objMain = new clsBLMain();
                                    //objMain.BookingID = labid;
                                    //objMain.AttributeID = attribcode;
                                    //objMain.Result = attribresult;
                                    //objMain.InstrumentID = MachineID;

                                    //DataView dv = objMain.GetAll(8);
                                    //if (dv.Count > 0)
                                    //    continue;

                                    string parsedData = labid + "," + attribcode + "," + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "," + attribresult;
                                    Logger.LogParsedData(parsedData);


                                    mi_tresult objresult = new DataModel.mi_tresult
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
                                    // var resultserialized = Newtonsoft.Json.JsonConvert.SerializeObject(objresult);

                                    //Console.WriteLine(MachineID + " Serialized result: " + resultserialized);
                                    _unitOfWork.ResultsRepository.Insert(objresult);


                                    ////writeLog("parsed data: " + pars);
                                    ////Console.WriteLine("parsed string:" + pars);
                                    //InsertBooking(pars);
                                }
                            }
                            catch (Exception ee)
                            {
                                Logger.LogExceptions(ee.ToString());
                            }
                        }
                    }
                    try
                    {
                        _unitOfWork.Save();
                    }

                    catch (Exception ee)
                    {
                        if (!string.IsNullOrEmpty(_unitOfWork.EntityValidationErrors))
                        {
                            Logger.LogExceptions(_unitOfWork.EntityValidationErrors);
                        }
                        else
                            Logger.LogExceptions(ee.ToString());
                        //Console.WriteLine("On Saving to local results table: " + ee.ToString());//, EventLogEntryType.Error);
                    }
                }
            }
            catch (Exception ee)
            {

                Logger.LogExceptions(ee.ToString());
            }

        }
    }
}
