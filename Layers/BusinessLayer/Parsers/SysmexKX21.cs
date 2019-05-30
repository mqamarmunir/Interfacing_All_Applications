using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataModel;

namespace BusinessLayer.Parsers
{
    public class SysmexKX21 : IParser
    {
        public void Parse(string data, mi_tinstruments machineSettings)
        {
            try
            {
                long MachineID = machineSettings.InstrumentID;

                string[] attribcodes = new string[] { "WBC", "RBC", "HGB", "HCT", "MCV", "MCH", "MCHC", "PLT", "LYM%", "MXD%", "NEUT%", "LYM#", "MXD#", "NEUT#", "RDW-SD/CV", "PDW", "MPV", "P-LCR" };
                string labid = "";
                string[] attribResult = new string[18];

                string[] sep1 = { Convert.ToChar(3).ToString() };
                string[] abc = data.Split(sep1, StringSplitOptions.RemoveEmptyEntries);
                string DataType;
                using (var _unitOfWork = new UnitOfWork())
                {

                    for (int i = 0; i <= abc.GetUpperBound(0); i++)
                    {
                        abc[i] = abc[i].Replace("\r\n", "");
                        DataType = abc[i].Substring(3, 1);//C for Quality Control and U for Analysis
                        labid = abc[i].Substring(11, 12);
                        for (int j = 0; j < 18; j++)
                        {
                            attribResult[j] = j < 17 ? abc[i].Substring(31 + (5 * j), 5) : abc[i].Substring(31 + (5 * j), 4);
                            if (attribcodes[j] == "RBC")
                            {
                                try
                                {
                                    attribResult[j] = (Convert.ToDecimal(attribResult[j]) / 10000).ToString();
                                }
                                catch
                                {
                                }

                            }
                            else if (attribcodes[j] == "PLT" || attribcodes[j] == "P-LCR")
                            {
                                try
                                {
                                    attribResult[j] = (Convert.ToDecimal(attribResult[j]) / 100).ToString();
                                }
                                catch
                                {
                                }
                            }
                            else
                            {
                                try
                                {
                                    attribResult[j] = (Convert.ToDecimal(attribResult[j]) / 1000).ToString();
                                }
                                catch
                                {
                                }
                            }

                            string parsedData = labid + "," + attribcodes[j] + "," + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "," + attribResult[j];
                            Logger.LogParsedData(parsedData);
                            mi_tresult objresult = new DataModel.mi_tresult
                            {
                                BookingID = labid,
                                AttributeID = attribcodes[j],
                                ClientID = System.Configuration.ConfigurationSettings.AppSettings["BranchID"].ToString().Trim(),
                                EnteredBy = 1,
                                EnteredOn = System.DateTime.Now,//.ToString("yyyy-MM-dd hh:mm:ss tt"),
                                InstrumentId = MachineID,
                                Result = attribResult[j],
                                Status = "N"
                            };
                            // var resultserialized = Newtonsoft.Json.JsonConvert.SerializeObject(objresult);

                            //Console.WriteLine(MachineID + " Serialized result: " + resultserialized);
                            _unitOfWork.ResultsRepository.Insert(objresult);
                        }
                    }
                    _unitOfWork.Save();
                }
            }
            catch (Exception ee)
            {
                Logger.LogExceptions(ee.ToString());
            }
        }

    }
}
