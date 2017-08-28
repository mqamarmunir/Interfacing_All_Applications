using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataModel;
using System.Web.Script.Serialization;
using System.IO.Ports;
using System.IO;

namespace OtherTests
{
    class Program
    {
        private static StringBuilder sb = new StringBuilder();
        static SerialPort _serial = new SerialPort();
        private static UnitOfWork _unitofwork;

        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        static void Main(string[] args)
        {
            //TestApi();
            //log.Info("Hello logging world!");
            //Console.WriteLine("Hit enter");
            //log.Error("Hello I am an Exception");
            //Console.ReadLine();
            
            _unitofwork = new UnitOfWork();
           // ParseAu480();
            _serial.PortName = System.Configuration.ConfigurationSettings.AppSettings["PortName"].ToString();
            _serial.BaudRate = Convert.ToInt16(System.Configuration.ConfigurationSettings.AppSettings["BaudRate"].ToString());
            _serial.StopBits = System.Configuration.ConfigurationSettings.AppSettings["StopBits"].ToString() == "1" ? StopBits.One : StopBits.None;
            _serial.DataBits = Convert.ToInt16(System.Configuration.ConfigurationSettings.AppSettings["DataBits"].ToString());
            _serial.Parity = Parity.None;
            try
            {
                _serial.Open();
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.ToString().Trim());
            }
            _serial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPort1_DataReceived);
 
            
            
            
            
            
            Console.ReadLine();
        }
        static private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            //if (!_serial.IsOpen)
            //{
            //    _serial.Open();
            //}
            //string data = _serial.ReadExisting();
            //if (data.Length > 0)
            //{

            //    _serial.Write(new byte[1] { 0x06 }, 0, 1);
            //    int _totalbytesrecieved = data.Length;
            //    Console.WriteLine("Total bytes Recieved: " + data.Length);
            //    Console.WriteLine(data);
            //    Writedatatofile(data);

            //}

            //string sb = "";// new StringBuilder();
            string data = "";
            string dataType = "BCAU480";
            if (dataType == "ASTM")
            {

                try
                {
                    data = _serial.ReadExisting();
                    Console.WriteLine(data);
                    // Writedatatofile(data);
                    if (data.Length > 0)
                    {
                        sb.Append(data);
                        _serial.Write(new byte[] { 0x06 }, 0, 1);

                        if (sb.ToString().Contains("L|1"))
                        {
                            data = sb.ToString();
                            //Parsethisandinsert(data,2);
                            Writedatatofile(data);
                            // StreamWriter sw = new StreamWriter("myfile.txt", false);
                            // sw.Write(data);
                            sb.Clear();
                        }
                    }


                }
                catch (Exception ee)
                {
                    Console.WriteLine("Following Exception occured in serialport datarecieved method please check." + ee.ToString());
                }
            }
            else
            {
                Console.WriteLine("In datareceived method");
                try
                {
                    data = _serial.ReadExisting();
                    Console.WriteLine(data);
                    // Writedatatofile(data);
                    if (data.Length > 0)
                    {
                        sb.Append(data);
                        // _serial.Write(new byte[] { 0x06 }, 0, 1);

                        if (sb.ToString().Contains("DE"))
                        {
                            data = sb.ToString();
                            Parsethisandinsert(data,3);
                            Writedatatofile(data);
                            // StreamWriter sw = new StreamWriter("myfile.txt", false);
                            // sw.Write(data);
                            sb.Clear();
                        }
                    }


                }
                catch (Exception ee)
                {
                    Console.WriteLine("Following Exception occured in serialport datarecieved method please check." + ee.ToString());
                }


            }


        }
        private static void Parsethisandinsert(string data, int Parsingalgorithm)
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
            switch (Parsingalgorithm)
            {

                #region 1st parser
                ///According to ASTM standard 
                ///tested on 
                ///sysmex xs800i,cobase411,cobasu411(urine analyzer)
                case 1:

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
                                    Console.WriteLine("Exception on getting Patientid: " + ee.ToString());
                                }
                            }
                            else if (def[j].Contains("O|") && def[j].Contains("R|") && def[j].IndexOf("O|") < def[j].IndexOf("R|"))
                            {
                                ///Get lab ID
                                string[] order = def[j].Split(sep3);
                                labid = order[2].ToString();
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
                                        Console.WriteLine("Error Converting Result: " + attribresult);
                                    }


                                }
                                else if (attribcode.ToLower().Equals("900") || attribcode.ToLower().Equals("999"))
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

                                string pars = labid + "," + attribcode + "," + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "," + attribresult;
                                //writeLog("parsed data: " + pars);
                                Console.WriteLine("parsed string:" + pars);
                                //InsertBooking(pars);
                            }


                        }
                    }
                    break;//case 1 ends here
                #endregion

                #region 2nd parser
                case 2:

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
                                    Console.WriteLine("Exception on getting Patientid: " + ee.ToString());
                                }
                            }
                            else if (def[j].Contains("O|") && def[j].Contains("R|") && def[j].IndexOf("O|") < def[j].IndexOf("R|"))
                            {
                                ///Get lab ID
                                try
                                {
                                    string[] order = def[j].Split(sep3);
                                    labid = order[2].ToString();
                                    if (labid.Contains("^"))
                                    {
                                        string[] splitlabid = labid.Split(sep4);
                                        labid = splitlabid[1].ToString().Trim();
                                    }
                                }
                                catch
                                { }
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
                                if (attribcode.Contains(@"\"))
                                {
                                    attribcode = attribcode.Replace(@"\", "");
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
                                        Console.WriteLine("Error Converting Result: " + attribresult);
                                    }


                                }
                                else if (attribcode.ToLower().Equals("900") || attribcode.ToLower().Equals("999"))
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

                                string pars = labid + "," + attribcode + "," + System.DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "," + attribresult;
                                //writeLog("parsed data: " + pars);
                                Console.WriteLine("parsed string:" + pars);
                                //InsertBooking(pars);
                            }


                        }
                    }//case 2 ends here
                    break;
                case 3:
                    ParseAu480(data);
                    break;
                #endregion
            }

        }
        private static void Writedatatofile(string data)
        {
            StreamWriter sw = new StreamWriter(System.Configuration.ConfigurationSettings.AppSettings["WriteFilePath"].ToString(), true);
            sw.Write(data);
            sw.Dispose();


            //   throw new NotImplementedException();
        }
        private static async void TestApi()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage res = await client.GetAsync("http://olivecliq.com/ricapi/site/TestAtt/bid/2/tid/429");

            if (res.IsSuccessStatusCode)
            {
                var content = await res.Content.ReadAsStringAsync();
                Console.WriteLine(content);
            }

        }
        static void ParseAu480(string data)
        {
            var text = data;//System.IO.File.ReadAllText(@"E:\WriteMe.txt");
            var splitter1 = new char[1] { Convert.ToChar(3) };
            var splitter2 = new string[] { " " };
            var arrayafter1stseperator = text.Split(splitter1, StringSplitOptions.RemoveEmptyEntries);
            string labid = "";
            //List<DataModel.mi_tresult> result = new List<DataModel.mi_tresult>();
            foreach (string str1 in arrayafter1stseperator)
            {
                try
                {
                    if (str1.Contains("DB") || str1.Contains("DE") || string.IsNullOrEmpty(str1))//skip start and end strings
                        continue;

                    var arrayafter2ndseperator = str1.Substring(0, 40).Split(splitter2, StringSplitOptions.RemoveEmptyEntries);
                    if (arrayafter2ndseperator.Length > 3)
                    {
                        labid = arrayafter2ndseperator[3];

                        string testsandresults = str1.Substring(40);
                        var splitter3 = new string[] { "r", "nr" };
                        var arrayafter3rdseperator = testsandresults.Split(splitter3, StringSplitOptions.None);
                        foreach (string testresultall in arrayafter3rdseperator)
                        {
                            string testresultsingle = testresultall.Replace("E", "").Trim();
                            if (testresultsingle.Length > 1)
                            {
                                string machinetestcode = testresultsingle.Substring(0, 3).Trim();
                                string resultsingle = testresultsingle.Substring(3).Trim();
                                var objresult = new DataModel.mi_tresult
                                {
                                    BookingID = labid,
                                    AttributeID = machinetestcode,
                                    ClientID = System.Configuration.ConfigurationSettings.AppSettings["ClientID"].ToString().Trim(),
                                    EnteredBy = 1,
                                    EnteredOn = System.DateTime.Now,//.ToString("yyyy-MM-dd hh:mm:ss tt"),
                                    machinename = "1",
                                    Result = resultsingle,
                                    Status="N"
                                };
                                _unitofwork.ResultsRepository.Insert(objresult);
                               
                                
                            }
                        }

                    }
                }

                catch (Exception ee)
                {
                    Console.WriteLine(str1);
                }
                try
                {

                    _unitofwork.Save();
                    Console.WriteLine("Data saved to database");
                }
                catch (Exception ee)
                {
                    log.Error("On Saving:", ee);
                }
            }



            //foreach(byte a in text)
            //{
            //    Console.Write(a.ToString());
            //    Console.Write('\t');
            //    Console.Write(Convert.ToChar(a).ToString());
            //    Console.Write('\n');
            //    Console.ReadLine();
            //}
            //if (result.Count > 0)
            //{
            //    var jsonSerialiser = new JavaScriptSerializer();
            //  //  var json = jsonSerialiser.Serialize(result);
            //   // System.IO.File.AppendAllText(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "InterfacingJSON.json"), json);
            //}
            Console.WriteLine("Done");
        }
    }
}
