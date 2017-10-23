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
using MySql.Data.MySqlClient;
using System.Data;

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
            ParseAu480("");
           // Parsethisandinsertsysmex(System.IO.File.ReadAllText("e:\\sysmex8000idata.txt"), 1);
            //_serial.PortName = System.Configuration.ConfigurationSettings.AppSettings["PortName"].ToString();
            //_serial.BaudRate = Convert.ToInt16(System.Configuration.ConfigurationSettings.AppSettings["BaudRate"].ToString());
            //_serial.StopBits = System.Configuration.ConfigurationSettings.AppSettings["StopBits"].ToString() == "1" ? StopBits.One : StopBits.None;
            //_serial.DataBits = Convert.ToInt16(System.Configuration.ConfigurationSettings.AppSettings["DataBits"].ToString());
            //_serial.Parity = Parity.None;
            //try
            //{
            //    _serial.Open();
            //}
            //catch (Exception ee)
            //{
            //    Console.WriteLine(ee.ToString().Trim());
            //}
            //_serial.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(serialPort1_DataReceived);
 
            
            
            
            
            
            Console.ReadLine();
        }
        private static void Parsethisandinsertsysmex(string data, int Parsingalgorithm)
        {
            string str1 = "";
            string str2 = "";
            string str3 = "";
            if (Parsingalgorithm != 1)
                return;
            string[] separator = new string[1] { "L|1" };
            char[] chArray1 = new char[1] { Convert.ToChar(13) };
            string[] strArray1 = data.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            char[] chArray2 = new char[1] { '|' };
            char[] chArray3 = new char[1] { '^' };
            for (int index1 = 0; index1 <= strArray1.GetUpperBound(0); ++index1)
            {
                string[] strArray2 = strArray1[index1].Split(new string[1]{"\r\n"},StringSplitOptions.None);
                for (int index2 = 0; index2 < strArray2.GetUpperBound(0); ++index2)
                {
                    if (strArray2[index2].Contains("H|") && !strArray2[index2].Contains("O|") && !strArray2[index2].Contains("R|"))
                    {
                        string[] strArray3 = strArray2[index2].Split(chArray2);
                        try
                        {
                            str1 = strArray3[13].ToString();
                        }
                        catch
                        {
                        }
                    }
                    else if (strArray2[index2].Contains("P|1") && (!strArray2[index2].Contains("O|") || strArray2[index2].IndexOf("P|") < strArray2[index2].IndexOf("O|")) && (!strArray2[index2].Contains("R|") || strArray2[index2].IndexOf("P|") < strArray2[index2].IndexOf("R|")))
                    {
                        string[] strArray3 = strArray2[index2].Split(chArray2);
                        try
                        {
                            str3 = strArray3[4].ToString();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Exception on getting Patientid: " + ex.ToString());
                        }
                    }
                    else if (strArray2[index2].Contains("O|") && strArray2[index2].Contains("R|") && strArray2[index2].IndexOf("O|") < strArray2[index2].IndexOf("R|"))
                    {
                        str2 = strArray2[index2].Split(chArray2)[2].ToString();
                        if (str2.Contains("^"))
                            str2 = str2.Split(chArray3)[1].ToString().Trim();
                    }
                    else if (strArray2[index2].Length>5 && strArray2[index2].Substring(2,2).Equals("O|"))
                    {
                        str2 = strArray2[index2].Split(chArray2)[3].ToString();
                        if (str2.Contains("^"))
                            str2 = str2.Split(chArray3)[2].ToString().Trim();
                    }
                    else if (strArray2[index2].Contains("R|"))
                    {
                        string[] strArray3 = strArray2[index2].Split(chArray2);
                        string str4 = strArray3[3].ToString();
                        string[] strArray4 = strArray3[2].Split(chArray3);
                        string str5 = !(strArray4[3] != "") ? strArray4[4].ToString() : strArray4[3].ToString();
                        if (str5.Contains("/"))
                            str5 = str5.Replace("/", "");
                        if (str5.ToLower() == "wbc" || str5.ToLower() == "plt")
                        {
                            try
                            {
                                str4 = (Convert.ToDecimal(str4) * new Decimal(1000)).ToString();
                                if (str4.Contains("."))
                                    str4 = str4.Substring(0, str4.IndexOf('.'));
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error Converting Result: " + str4);
                            }
                        }
                        else if (str5.ToLower().Equals("900") || str5.ToLower().Equals("999") || str5.ToLower().Equals("102"))
                        {
                            if (str4.Contains("-1^"))
                                str4 = str4.Replace("-1^", "Negative  \r\n");
                            else if (str4.Contains("1^"))
                                str4 = str4.Replace("1^", "Positive  \r\n");
                        }
                        else if (str5.ToLower().Equals("eo%") || str5.ToLower().Equals("mono%") || str5.ToLower().Equals("neut%") || str5.ToLower().Equals("lymph%"))
                        {
                            try
                            {
                                str4 = Math.Round(Convert.ToDecimal(str4)).ToString().Trim();
                                if (str4.Contains("."))
                                    str4 = str4.Substring(0, str4.IndexOf('.'));
                            }
                            catch
                            {
                            }
                        }
                        if (str2 == "")
                            str2 = str3;
                        var x = str2 + "," + str5 + "," + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "," + str4;
                        Console.WriteLine(x);
                        InsertBooking(str2 + "," + str5 + "," + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "," + str4);
                    }
                }
            }
        }
        private static bool IsNaturalNumber(string strNumber)
        {
            System.Text.RegularExpressions.Regex objNotNaturalPattern = new System.Text.RegularExpressions.Regex("[^0-9]");
            System.Text.RegularExpressions.Regex objNaturalPattern = new System.Text.RegularExpressions.Regex("0*[1-9][0-9]*");
            return !objNotNaturalPattern.IsMatch(strNumber) &&
                objNaturalPattern.IsMatch(strNumber);
        }
        private static void InsertBooking(string Msg)
        {
            MySqlConnection objConn = new MySqlConnection("Server = localhost; Port = 3306; Database = mi_sysmex; Uid = root; Pwd = trees");
            //MySqlConnection objConn = new MySqlConnection("Server = localhost; Port = 3306; Database = MI; Uid = root; Pwd = 123");
            MySqlCommand objCmd = new MySqlCommand();
            MySqlDataAdapter da = new MySqlDataAdapter(objCmd);
            DataSet DS = new DataSet();
            string myquerystring = "";
            string EnteredOn = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");

            string[] str = { "", "", "", "", "", "", "" };//BookingID,LABID,SendOn,ReceivedOn,Result,AttributeCode,AttributeID

            str[3] = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt");
            ///
            /// Create Quries for Selected Machine
            /// 
            try
            {
                objConn.Open();
                objCmd.Connection = objConn;





                string[] Delimeter = { "," };
                string[] Tmp = Msg.Split(Delimeter, StringSplitOptions.RemoveEmptyEntries);

                str[1] = Tmp[0].Trim();//Labid
                str[4] = Tmp[3].Trim();//Result
                str[5] = Tmp[1].Trim();//AttributeCode
                str[2] = Tmp[2];//sendon



                if (!IsNaturalNumber(str[1].Trim()))
                {
                    return;
                }

                myquerystring = "SELECT a.attributeid,a.MachineAttributeCode,t.Machine_Test_name, t.Machine_testid,t.MachineTestCode, t.Instrumentid,b.bookingid FROM mi_ttestattribute a,mi_ttests t left outer join mi_tbooking b on b.Machine_TestID=t.Machine_TestID and b.labid='" + str[1].Trim() + "' and b.Test_code='" + str[5].Trim() + "' where a.Machine_TestID=t.Machine_TestID and a.MachineAttributeCode='" + str[5].Trim() + "'  and t.Instrumentid=" + 20 + "";

                objCmd.CommandText = myquerystring;
                da.Fill(DS, "TestInfo");

                if (DS.Tables[0].Rows.Count > 0)
                {
                    str[0] = DS.Tables["TestInfo"].Rows[0]["BookingID"].ToString();
                    if (str[0].Equals(""))
                    {
                        myquerystring = "INSERT INTO mi_tbooking(LABID,Test_Code,Machine_TestID,Test_name,InstrumentID,EnteredBy,EnteredOn,ClientID";
                        if (!str[2].Equals(""))
                        {
                            myquerystring += ",SendON";
                        }
                        if (!str[3].Equals(""))
                        {
                            myquerystring += ",ReceivedON";
                        }
                        myquerystring += ",Active) VALUES('" + str[1].Trim() + "','" + DS.Tables["TestInfo"].Rows[0]["MachineTestCode"].ToString() + "','" + DS.Tables["TestInfo"].Rows[0]["Machine_testid"].ToString() + "','" + DS.Tables["TestInfo"].Rows[0]["Machine_Test_name"].ToString() + "','" + DS.Tables["TestInfo"].Rows[0]["Instrumentid"].ToString() + "'," + 1 + ",str_to_date('" + EnteredOn + "','%d/%m/%Y %h:%i %p')  ,'" + 005 + "'";
                        if (!str[2].Equals(""))
                        {
                            myquerystring += ",str_to_date('" + str[2].Trim() + "','%d/%m/%Y %h:%i %p')";
                        }
                        if (!str[3].Equals(""))
                        {
                            myquerystring += ",str_to_date('" + str[3].Trim() + "','%d/%m/%Y %h:%i %p')";
                        }
                        myquerystring += ",'Y')";
                    }
                    str[6] = DS.Tables["TestInfo"].Rows[0]["attributeid"].ToString();
                }

                if (!str[6].Equals(""))
                {
                    if (str[0].Equals(""))
                    {
                        objCmd.CommandText = myquerystring;
                        objCmd.ExecuteNonQuery();

                        myquerystring = "SELECT ifnull(max(bookingid),1) as bookingid FROM mi_tbooking m";
                        objCmd.CommandText = myquerystring;

                        da.Fill(DS, "BookingID");
                        str[0] = DS.Tables["BookingID"].Rows[0]["bookingid"].ToString();
                    }
                    myquerystring = "SELECT Resultid FROM mi_tresult m where bookingID=" + str[0] + " and AttributeID=" + str[6];
                    objCmd.CommandText = myquerystring;

                    da.Fill(DS, "ResultDup");

                    if (DS.Tables["ResultDup"].Rows.Count == 0)
                    {
                        myquerystring = "INSERT INTO mi_tresult(BookingID, AttributeID, Result, EnteredBy, EnteredOn, ClientID, Status ) values(" + str[0] + "," + str[6] + ",'" + str[4] + "'," + 1 + ",str_to_date('" + EnteredOn + "','%d/%m/%Y %h:%i %p')  ,'" + 005 + "','0' )";

                        objCmd.CommandText = myquerystring;
                        objCmd.ExecuteNonQuery();

                        //System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;

                        //GetTodayTotalTest();
                        //GetTodayDetailTest();
                        // WeeklyGraph();
                        // MonthlyGraph();

                        //System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = true ;
                    }
                }
            }
            catch (Exception exc)
            {
                //writeLog(DateTime.Now.ToString("yyyyMMddhhmmss") + " : Insert Booking : " + exc.Message);
                // MessageBox.Show(exc.Message);
            }
            finally
            {
                objConn.Close();
                //lblToday.Tag = "";
            }
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
            var text = System.IO.File.ReadAllText(@"E:\TestData.txt");
            //if (text.Contains(Convert.ToChar(3).ToString()))
            //{
            //    Console.WriteLine("Found");
            //}
            var splitter1 = new string[] { "D ", "DR", "DH", "DQ", "d ", "DA", "dH" } ;
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
                    if (arrayafter2ndseperator.Length > 2)
                    {
                        labid = arrayafter2ndseperator[2];

                        string testsandresults = str1.Substring(40).TrimStart().Replace("E","");

                        int thisordertestscount = Convert.ToInt32(Math.Round(testsandresults.Length / 11.0));
                        string[] indtestanditsresult = new string[thisordertestscount];
                        for (int i = 0; i < thisordertestscount; i++)
                        {
                            if (11 * i + 11 <= testsandresults.Length)
                                indtestanditsresult[i] = testsandresults.Substring(11 * i, 11);
                            else
                                indtestanditsresult[i] = testsandresults.Substring(11 * i);
                        }
                        //var splitter3 = new string[] { "r", "nr" };
                        //var arrayafter3rdseperator = testsandresults.Split(splitter3, StringSplitOptions.None);
                        List<DataModel.mi_tresult> lstresults = new List<DataModel.mi_tresult>();
                        foreach (string thistestandresult in indtestanditsresult)
                        {
                            //string testresultsingle = testresultall.Replace("E", "").Trim();
                            if (thistestandresult.Length > 1)
                            {
                                string machinetestcode = thistestandresult.Substring(0, 3).Trim();
                                string resultsingle = thistestandresult.Substring(3, 6).Trim();
                                
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
                                lstresults.Add(objresult);
                                //_unitofwork.ResultsRepository.Insert(objresult);
                               
                                
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

                  //  _unitofwork.Save();
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
