using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.IO;
using System.Text.RegularExpressions;

namespace SerialTest
{
    class Program
    {
        private static StringBuilder sb = new StringBuilder();
        static SerialPort _serial = new SerialPort();
        static void Main(string[] args)
        {
            var doubleArray = Regex.Split(Convert.ToChar(32)+" Ac", @"[^0-9\.-]+")
    .Where(c => c != "." && c.Trim() != "").FirstOrDefault();
            Console.WriteLine(Convert.ToString(doubleArray));
            //string abc="H|\\^&||||||||||P||\rP|1||||||||||||||||||||||||||||||||||\r O|1|000004|40^0^5^^SAMPLE^NORMAL|ALL|R|20051220095504|||||X ||||||||||||||O\r R|1|^^^10^^0|1.25|ulU/ml|0.270^4.20|N||F|||20051220095534| 20051220101604|\r R|2|^^^30^2^1|1.52|ng/dl|1.01^1.79|N||F|||20051220103034| 20051220105004|\r R|3|^^^40^^0|1.17|ulU/ml|0.846^2.02|N||F|||20051220110034| 20051220112004|\r L|1";
            //string replaced = abc.Replace('\r', Convert.ToChar(13));
            // serial port comm settings
            //_serial.PortName = System.Configuration.ConfigurationSettings.AppSettings["PortName"].ToString();
            //_serial.BaudRate = Convert.ToInt16(System.Configuration.ConfigurationSettings.AppSettings["BaudRate"].ToString());
            //_serial.StopBits = System.Configuration.ConfigurationSettings.AppSettings["StopBits"].ToString() == "1" ? StopBits.One : StopBits.None;
            //_serial.DataBits = Convert.ToInt16(System.Configuration.ConfigurationSettings.AppSettings["DataBits"].ToString());
            //_serial.Parity = Parity.None;
            //try
            //{
            //    _serial.Open();
            //    Console.WriteLine("Port Opened");
            //}
            //catch (Exception ee)
            //{
            //    Console.WriteLine(ee.ToString().Trim());
            //}
            //_serial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPort2_DataReceived);
            // MyNewParser("003 27.52r 006      / 007  72.3r 014  1.59nr016    14nr018    76nr019114.29r 023 102.7r 026103.19r 031  0.81r 034    24nr035282.49r 061 24.83r 062 12.86r 063 20.64r 097   139r 098  3.43r 099   100r");
            // string alldata = File.ReadAllText("E:\\TetData.txt");
            // Parsethisandinsert(alldata, 2);
            Console.ReadLine();

        }

        private static void MyNewParser(string data)
        {
            int thisordertestscount = Convert.ToInt32(Math.Round(data.Length / 11.0));
            string[] indtestanditsresult = new string[thisordertestscount];
            for (int i = 0; i < thisordertestscount; i++)
            {
                if (11 * i + 11 <= data.Length)
                    indtestanditsresult[i] = data.Substring(11 * i, 11);
                else
                    indtestanditsresult[i] = data.Substring(11 * i);
            }

        }
        static bool is_FirstSYN = true;
        static bool ReceiveBlockCount = false;
        static int BlocksCount = 0;
        static StringBuilder sb_Blocks = new StringBuilder();
        static StringBuilder sb_thisBlock = new StringBuilder();
        static Boolean startBlockReceiving = false;
        static void serialPort2_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            string data = "";
            try
            {
                string PortName = ((SerialPort)sender).PortName;
                Console.WriteLine("Data received from Port:" + PortName);
                data = _serial.ReadExisting();
                System.IO.File.AppendAllText(System.Configuration.ConfigurationSettings.AppSettings["WriteFilePath"].ToString().Trim(), data);
                if (data.Length > 0)
                {
                    // var thismachinesettings = _unitOfWork.InstrumentsRepository.GetSingle(x => x.Active == "Y" && x.PORT == "COM1");
                    //string MachineID = thismachinesettings.CliqInstrumentID.ToString().Trim();
                    // if (!String.IsNullOrEmpty(thismachinesettings.Acknowledgement_code))

                    if (data[0] == Convert.ToChar(22))
                    {
                        if (is_FirstSYN)
                        {
                            _serial.Write(new byte[] { 0x16 }, 0, 1);
                            ReceiveBlockCount=true;
                            is_FirstSYN = false;
                        }
                        else
                        {
                            _serial.Write(new byte[] { 0x06 }, 0, 1);
                            Parsethisandinsert(sb_Blocks.ToString(), 3);
                            sb_Blocks.Clear();
                            is_FirstSYN = true;
                        }
                    }
                    else
                    {

                        if (ReceiveBlockCount)
                        {
                            if (int.Parse(data) > 0)
                            {
                                BlocksCount = int.Parse(data);
                                ReceiveBlockCount = false;
                                startBlockReceiving = true;
                                _serial.Write(new byte[] { 0x06 }, 0, 1);//send Ack to machine
                                return;
                            }
                            else
                                return;
                            

                        }
                        if (startBlockReceiving)
                        {
                            if (data.IndexOf(Convert.ToChar(3)) == -1)
                            {
                                sb_thisBlock.Append(data);
                                return;
                            }
                            sb_thisBlock.Append(data);
                            sb_Blocks.Append(sb_thisBlock.ToString().Substring(0,sb_thisBlock.ToString().Length-4));
                            if (sb_thisBlock.ToString().StartsWith(Convert.ToChar(2) + BlocksCount.ToString().PadLeft(2, '0')))
                            {
                                System.IO.File.AppendAllText("E:\\AllBlocks.txt", sb_Blocks.ToString());
                               
                                
                                startBlockReceiving = false;
                            }
                            sb_thisBlock.Clear();
                            _serial.Write(new byte[] { 0x06 }, 0, 1);


                        }

                        //send Ack to machine

                    }
                    sb.Append(data);
                    Console.WriteLine(data);
                    
                 

                }


            }
            catch (Exception ee)
            {
                Console.WriteLine("Following Exception occured in serialport datarecieved method please check." + ee.ToString());
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
                #endregion
                #region 3rd parser
                case 3:
                    ParseBeckmanHematology(data);
                    break;
                #endregion

            }

        }

        private static void ParseBeckmanHematology(string data)
        {
            var x = data.Split(new string[1]{"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
            var validentries=new string[]{"ID1 ","WBC","LY#","MO#","BA#","EO#","RBC","HGB","HCT","MCV","MCH","MCHC","RDW","PLT","MPV","PCT","RDW","LY%","MO%","EO%","BA%"};
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

                        var xxxx = Newtonsoft.Json.JsonConvert.SerializeObject(new { attribute = attrib, result = res });
                        System.IO.File.AppendAllText("E:\\Parsedresults.txt", xxxx + "\r\n");

                    }
                }
                catch (Exception ee)
                {
 
                }

            }
        }
        private static void Writedatatofile(string data)
        {
            StreamWriter sw = new StreamWriter(System.Configuration.ConfigurationSettings.AppSettings["WriteFilePath"].ToString(), true);
            sw.Write(data);
            sw.Dispose();


            //   throw new NotImplementedException();
        }

    }
}
