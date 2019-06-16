using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel;

namespace BusinessLayer.Parsers
{
    public class DIRUIH500 : IParser
    {
        public void Parse(string data, mi_tinstruments machineSettings)
        {
            long MachineID = machineSettings.InstrumentID;
            string labid = "";
            string[] sep1 = { Convert.ToChar(3).ToString() };
            string[] sep2 = { Convert.ToChar(13).ToString() };
            string[] abc = data.Split(sep1, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> attributeCodeAndResults = new Dictionary<string, string>();
            using (var _unitOfWork = new UnitOfWork())
            {

                for (int i = 0; i <= abc.GetUpperBound(0); i++)
                {
                    string[] arrAfterSep2 = abc[i].Split(sep2, StringSplitOptions.None);

                    foreach (string line in arrAfterSep2)
                    {
                        if (line.Contains(Convert.ToChar(2))|| line.Length<5 || line.Contains("No."))
                            continue;

                        if (line.Contains("ID"))
                        {
                            ///Fetch LabId here
                            ///
                            string[] attribandResult = line.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            labid = attribandResult[1].Replace("-", "");

                        }
                        else if (line.Contains("Date") || line.Contains("Color") || line.Contains("Clarity"))
                        {
                            string[] attribandResult = line.Split(new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries);
                            if (line.Contains("Date"))
                                attributeCodeAndResults.Add(attribandResult[0].Trim(), attribandResult[1].Trim() + " :" + attribandResult[2]);
                            else
                                attributeCodeAndResults.Add(attribandResult[0].Trim(), attribandResult[1].Trim());

                        }
                        else
                        {
                            string[] attribandResult = line.Split(new char[] { Convert.ToChar(32) }, StringSplitOptions.RemoveEmptyEntries);
                            if (line.StartsWith("\n*"))
                            {
                                attributeCodeAndResults.Add(attribandResult[0].Trim().Replace("*",""), attribandResult[1].Trim() + (attribandResult.Length>3?" "+attribandResult[3]:""));
                            }
                            else
                                attributeCodeAndResults.Add(attribandResult[1].Trim(), attribandResult[2].Trim() + (attribandResult.Length > 3 ? " " + attribandResult[3] : ""));
                        }
                    }

                    foreach (var item in attributeCodeAndResults)
                    {
                        mi_tresult objresult = new DataModel.mi_tresult
                        {
                            BookingID = labid,
                            AttributeID = item.Key,
                            ClientID = System.Configuration.ConfigurationSettings.AppSettings["BranchID"].ToString().Trim(),
                            EnteredBy = 1,
                            EnteredOn = System.DateTime.Now,//.ToString("yyyy-MM-dd hh:mm:ss tt"),
                            InstrumentId = MachineID,
                            Result = item.Value,
                            Status = "N"
                        };
                        // var resultserialized = Newtonsoft.Json.JsonConvert.SerializeObject(objresult);

                        //Console.WriteLine(MachineID + " Serialized result: " + resultserialized);
                        _unitOfWork.ResultsRepository.Insert(objresult);
                    }
                    attributeCodeAndResults.Clear();
                }
                _unitOfWork.Save();
            }

        }
    }
}

