using BusinessLayer;
using BusinessLayer.Parsers;
using Common;
using DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsApplication5.Properties;

namespace WindowsApplication5.CommForms
{
    public partial class SerialCommunication : Form
    {
        #region global variables
        private List<mi_tinstruments> allInstruments;
        private UnitOfWork _unitOfWork;

        private StringBuilder sb_port1 = new StringBuilder();
        private StringBuilder sb_port2 = new StringBuilder();
        private StringBuilder sb_port3 = new StringBuilder();
        private StringBuilder sb_port4 = new StringBuilder();
        private StringBuilder sb_port5 = new StringBuilder();
        private StringBuilder sb_port6 = new StringBuilder();
        private StringBuilder sb_port7 = new StringBuilder();

        bool is_FirstSYN = true;
        bool ReceiveBlockCount = false;
        int BlocksCount = 0;
        StringBuilder sb_Blocks = new StringBuilder();
        StringBuilder sb_thisBlock = new StringBuilder();
        Boolean startBlockReceiving = false;
        private TabControl tabCtrl;
        private TabPage tabPag;
        public TabControl TabCtrl
        {
            set
            {
                tabCtrl = value;
            }
        }
        public TabPage TabPag
        {
            set
            {
                tabPag = value;
            }
        }
        #endregion
        public SerialCommunication()
        {
            InitializeComponent();
        }

        private void SerialCommunication_Load(object sender, EventArgs e)
        {
            allInstruments = StaticCache.GetAllInstruments(true);
            if (ConfigurationManager.AppSettings["RequireOpen_1"] == "Y")
            {
                serialPort1.PortName = ConfigurationManager.AppSettings["PortName_1"].ToString();
                serialPort1.BaudRate = Convert.ToInt16(ConfigurationManager.AppSettings["BaudRate_1"].ToString());
                serialPort1.StopBits = ConfigurationManager.AppSettings["StopBits_1"].ToString() == "1" ? StopBits.One : StopBits.None;
                serialPort1.DataBits = Convert.ToInt16(ConfigurationManager.AppSettings["DataBits_1"].ToString());
                serialPort1.Parity = Parity.None;
                serialPort1.Handshake = Handshake.None;
                try
                {
                    
                    serialPort1.Open();
                    pictureBox1.Image = Resources.circle_16__1_;
                    AppendToRichTextBox("Started Listening on Port: " + serialPort1.PortName);
                    Logger.LogSerialPortRelatedData("Listening at Port:  " + serialPort1.PortName + ", at BaudRate: " + serialPort1.BaudRate.ToString() + ", DataBits: " + serialPort1.DataBits.ToString());
                }
                catch (Exception ee)
                {
                    AppendToRichTextBox(ee.ToString());
                    Logger.LogExceptions(ee.ToString());

                }

            }
            if (ConfigurationManager.AppSettings["RequireOpen_2"] == "Y")
            {
                serialPort2.PortName = ConfigurationManager.AppSettings["PortName_2"].ToString();
                serialPort2.BaudRate = Convert.ToInt16(ConfigurationManager.AppSettings["BaudRate_2"].ToString());
                serialPort2.StopBits = ConfigurationManager.AppSettings["StopBits_2"].ToString() == "1" ? StopBits.One : StopBits.None;
                serialPort2.DataBits = Convert.ToInt16(ConfigurationManager.AppSettings["DataBits_2"].ToString());
                serialPort2.Parity = Parity.None;
                try
                {
                    serialPort2.Open();
                    pictureBox2.Image = Resources.circle_16__1_;
                    AppendToRichTextBox("Started Listening on Port: " + serialPort2.PortName);
                    Logger.LogSerialPortRelatedData("Listening at Port:  " + serialPort2.PortName + ", at BaudRate: " +
                                                                             serialPort2.BaudRate.ToString() + ", DataBits: " +
                                                                             serialPort2.DataBits.ToString());
                }
                catch (Exception ee)
                {
                    AppendToRichTextBox(ee.ToString());
                    Logger.LogExceptions(ee.ToString());

                }

            }
            if (ConfigurationManager.AppSettings["RequireOpen_3"] == "Y")
            {
                serialPort3.PortName = ConfigurationManager.AppSettings["PortName_3"].ToString();
                serialPort3.BaudRate = Convert.ToInt16(ConfigurationManager.AppSettings["BaudRate_3"].ToString());
                serialPort3.StopBits = ConfigurationManager.AppSettings["StopBits_3"].ToString() == "1" ? StopBits.One : StopBits.None;
                serialPort3.DataBits = Convert.ToInt16(ConfigurationManager.AppSettings["DataBits_3"].ToString());
                serialPort3.Parity = Parity.None;
                try
                {
                    serialPort3.Open();
                    pictureBox3.Image = Resources.circle_16__1_;
                    AppendToRichTextBox("Started Listening on Port: " + serialPort3.PortName);
                    Logger.LogSerialPortRelatedData("Listening at Port:  " + serialPort3.PortName + ", at BaudRate: " +
                                                                             serialPort3.BaudRate.ToString() + ", DataBits: " +
                                                                             serialPort3.DataBits.ToString());
                }
                catch (Exception ee)
                {
                    AppendToRichTextBox(ee.ToString());
                    Logger.LogExceptions(ee.ToString());

                }

            }
            if (ConfigurationManager.AppSettings["RequireOpen_4"] == "Y")
            {
                serialPort4.PortName = ConfigurationManager.AppSettings["PortName_4"].ToString();
                serialPort4.BaudRate = Convert.ToInt16(ConfigurationManager.AppSettings["BaudRate_4"].ToString());
                serialPort4.StopBits = ConfigurationManager.AppSettings["StopBits_4"].ToString() == "1" ? StopBits.One : StopBits.None;
                serialPort4.DataBits = Convert.ToInt16(ConfigurationManager.AppSettings["DataBits_4"].ToString());
                serialPort4.Parity = Parity.None;
                try
                {
                    serialPort4.Open();
                    pictureBox4.Image = Resources.circle_16__1_;
                    AppendToRichTextBox("Started Listening on Port: " + serialPort4.PortName);
                    Logger.LogSerialPortRelatedData("Listening at Port:  " + serialPort4.PortName + ", at BaudRate: " +
                                                                             serialPort4.BaudRate.ToString() + ", DataBits: " +
                                                                             serialPort4.DataBits.ToString());
                }
                catch (Exception ee)
                {
                    AppendToRichTextBox(ee.ToString());
                    Logger.LogExceptions(ee.ToString());

                }

            }
            if (ConfigurationManager.AppSettings["RequireOpen_5"] == "Y")
            {
                serialPort5.PortName = ConfigurationManager.AppSettings["PortName_5"].ToString();
                serialPort5.BaudRate = Convert.ToInt16(ConfigurationManager.AppSettings["BaudRate_5"].ToString());
                serialPort5.StopBits = ConfigurationManager.AppSettings["StopBits_5"].ToString() == "1" ? StopBits.One : StopBits.None;
                serialPort5.DataBits = Convert.ToInt16(ConfigurationManager.AppSettings["DataBits_5"].ToString());
                serialPort5.Parity = Parity.None;
                try
                {
                    serialPort5.Open();
                    pictureBox5.Image = Resources.circle_16__1_;
                    AppendToRichTextBox("Started Listening on Port: " + serialPort5.PortName);
                    Logger.LogSerialPortRelatedData("Listening at Port:  " + serialPort5.PortName + ", at BaudRate: " +
                                                                             serialPort5.BaudRate.ToString() + ", DataBits: " +
                                                                             serialPort5.DataBits.ToString());
                }
                catch (Exception ee)
                {
                    AppendToRichTextBox(ee.ToString());
                    Logger.LogExceptions(ee.ToString());

                }

            }
            if (ConfigurationManager.AppSettings["RequireOpen_6"] == "Y")
            {
                serialPort6.PortName = ConfigurationManager.AppSettings["PortName_6"].ToString();
                serialPort6.BaudRate = Convert.ToInt16(ConfigurationManager.AppSettings["BaudRate_6"].ToString());
                serialPort6.StopBits = ConfigurationManager.AppSettings["StopBits_6"].ToString() == "1" ? StopBits.One : StopBits.None;
                serialPort6.DataBits = Convert.ToInt16(ConfigurationManager.AppSettings["DataBits_6"].ToString());
                serialPort6.Parity = Parity.None;
                try
                {
                    serialPort6.Open();
                    pictureBox6.Image = Resources.circle_16__1_;
                    AppendToRichTextBox("Started Listening on Port: " + serialPort6.PortName);
                    Logger.LogSerialPortRelatedData("Listening at Port:  " + serialPort6.PortName + ", at BaudRate: " +
                                                                             serialPort6.BaudRate.ToString() + ", DataBits: " +
                                                                             serialPort6.DataBits.ToString());
                }
                catch (Exception ee)
                {
                    AppendToRichTextBox(ee.ToString());
                    Logger.LogExceptions(ee.ToString());

                }

            }
            if (ConfigurationManager.AppSettings["RequireOpen_7"] == "Y")
            {
                serialPort7.PortName = ConfigurationManager.AppSettings["PortName_7"].ToString();
                serialPort7.BaudRate = Convert.ToInt16(ConfigurationManager.AppSettings["BaudRate_7"].ToString());
                serialPort7.StopBits = ConfigurationManager.AppSettings["StopBits_7"].ToString() == "1" ? StopBits.One : StopBits.None;
                serialPort7.DataBits = Convert.ToInt16(ConfigurationManager.AppSettings["DataBits_7"].ToString());
                serialPort7.Parity = Parity.None;
                try
                {
                    serialPort7.Open();
                    pictureBox7.Image = Resources.circle_16__1_;
                    AppendToRichTextBox("Started Listening on Port: " + serialPort7.PortName);
                    Logger.LogSerialPortRelatedData("Listening at Port:  " + serialPort7.PortName + ", at BaudRate: " +
                                                                             serialPort7.BaudRate.ToString() + ", DataBits: " +
                                                                             serialPort7.DataBits.ToString());
                }
                catch (Exception ee)
                {
                    AppendToRichTextBox(ee.ToString());
                    Logger.LogExceptions(ee.ToString());

                }

            }



        }

        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                string PortName = ((SerialPort)sender).PortName.ToString();
                mi_tinstruments thismachinesettings = allInstruments.Where(x => x.Active == "Y" && x.PORT == PortName.Trim()).FirstOrDefault();// _unitOfWork.InstrumentsRepository.GetSingle(x => x.Active == "Y" && x.PORT == PortName.Trim());
                if (thismachinesettings == null)
                {
                    Logger.LogExceptions("Machine at Port: " + PortName + " not registered");
                    return;
                }

                //DataReceived(sender, machineSettings);
                string data = "";


                data = ((System.IO.Ports.SerialPort)sender).ReadExisting();

                if (data.Length > 0)
                {
                    HandleReceivedData(thismachinesettings, sb_port1,data);

                }

            }
            catch (Exception ee)
            {

                Logger.LogExceptions("Following Exception occured in serialport1 datarecieved method please check." + ee.ToString());

            }

        }

        private void HandleReceivedData(mi_tinstruments thismachinesettings, StringBuilder sb_port1,string data)
        {
            if (thismachinesettings.Communication_Stnadard == "ASTM")
            {
                serialPort1.Write(new byte[] { 0x06 }, 0, 1);
                sb_port1.Append(data);
                if (sb_port1.ToString().IndexOf(thismachinesettings.RecordTerminator) > -1)
                {
                    //Console.WriteLine("In after terminator");

                    string fullText = sb_port1.ToString();
                    string content = fullText.Substring(0, fullText.IndexOf(thismachinesettings.RecordTerminator) + thismachinesettings.RecordTerminator.Length);
                    //Console.WriteLine(content);
                    sb_port1.Clear();
                    //if (fullText.LastIndexOf(@"H|\^&") > 0)
                    //{
                    //    string remainingContent = fullText.Substring(fullText.LastIndexOf(@"H|\^&"));

                    //    sb_port1.Append(remainingContent);
                    //}

                    Logger.LogReceivedData(thismachinesettings.Instrument_Name, content);
                    AppendToRichTextBox("Data Received from " + thismachinesettings.Instrument_Name + " " + content);
                    ParserDecision.Parsethisandinsert(content, thismachinesettings);
                }
            }
            else if (thismachinesettings.Communication_Stnadard == "Other")
            {

                if (!String.IsNullOrEmpty(thismachinesettings.Acknowledgement_code))
                    serialPort1.Write(new byte[] { 0x06 }, 0, 1);//send Ack to machine
                sb_port1.Append(data);
                //eventLog1.WriteEntry(data);

                System.IO.File.AppendAllText(ConfigurationManager.AppSettings["ReceivedDataLogFile"].ToString().Trim(), data);
                if (sb_port1.ToString().Contains("DE"))//.Split(new string[] { "D ", "DR", "DH", "DQ", "d ", "DA", "dH", "DE" }, StringSplitOptions.RemoveEmptyEntries).Length>0//For Au480 temporary//L|1 is a terminator record according to astm standards
                {
                    ///if the recieved string contains the terminator
                    ///then parse the record and Clear the string
                    ///Builder for next Record.
                    ///


                    string parsingdata = sb_port1.ToString();
                    sb_port1.Clear();
                    ParserDecision.Parsethisandinsert(parsingdata, thismachinesettings);
                    //t.Start();
                    //ParserDecision.Parsethisandinsert(parsingdata, thismachinesettings.ParsingAlgorithm, MachineID);
                    // parsingdata = string.Empty;


                }
                else if (sb_port1.ToString().Contains("D ") && sb_port1.ToString().Contains(Convert.ToChar(3)) && sb_port1.ToString().Contains("          "))
                {


                    string data_tobeparsed = sb_port1.ToString().Substring(sb_port1.ToString().LastIndexOf(Convert.ToChar(3)));
                    ParserDecision.Parsethisandinsert(sb_port1.ToString().Substring(0, sb_port1.ToString().LastIndexOf(Convert.ToChar(3))), thismachinesettings);
                    sb_port1.Clear();
                    sb_port1.Append(data_tobeparsed);
                    //t.Start();
                    //  ParserDecision.Parsethisandinsert(sb_port1.ToString().Substring(0, sb_port1.ToStripng().LastIndexOf(Convert.ToChar(3))), thismachinesettings.ParsingAlgorithm, MachineID);
                }
            }
            else if (thismachinesettings.Communication_Stnadard == "LH750")
            {

                if (data[0] == Convert.ToChar(22))
                {
                    if (is_FirstSYN)
                    {
                        serialPort1.Write(new byte[] { 0x16 }, 0, 1);
                        ReceiveBlockCount = true;
                        is_FirstSYN = false;
                    }
                    else
                    {
                        serialPort1.Write(new byte[] { 0x06 }, 0, 1);

                        string data_toparse = sb_Blocks.ToString();
                        sb_Blocks.Clear();
                        is_FirstSYN = true;
                        ParserDecision.Parsethisandinsert(data_toparse, thismachinesettings);
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
                            serialPort1.Write(new byte[] { 0x06 }, 0, 1);//send Ack to machine
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
                        sb_Blocks.Append(sb_thisBlock.ToString().Substring(3, sb_thisBlock.ToString().Length - 8));//Remove initial and trailing block headers
                        if (sb_thisBlock.ToString().StartsWith(Convert.ToChar(2) + BlocksCount.ToString().PadLeft(2, '0')))
                        {
                            //System.IO.File.AppendAllText("E:\\AllBlocks.txt", sb_Blocks.ToString());


                            startBlockReceiving = false;
                        }
                        sb_thisBlock.Clear();
                        serialPort1.Write(new byte[] { 0x06 }, 0, 1);


                    }

                    //send Ack to machine

                }
            }
            else if (thismachinesettings.Communication_Stnadard == "Sysmex-KX21")
            {
                sb_port1.Append(data);
                if (sb_port1.ToString().IndexOf(Convert.ToChar(3)) > -1)//3 i-e ETX is the RecordTerminator of Sysmex-KX21
                {

                    //Console.WriteLine("In after terminator");

                    string fullText = sb_port1.ToString();
                    //string content = fullText.Substring(0, fullText.IndexOf(thismachinesettings.RecordTerminator) + thismachinesettings.RecordTerminator.Length);
                    //Console.WriteLine(content);
                    sb_port1.Clear();


                    Logger.LogReceivedData(thismachinesettings.Instrument_Name, fullText);
                    AppendToRichTextBox("Data Received from " + thismachinesettings.Instrument_Name + " " + fullText);
                    ParserDecision.Parsethisandinsert(fullText, thismachinesettings);
                }
            }
            else if (thismachinesettings.Communication_Stnadard == "DirUIH500")
            {
                sb_port1.Append(data);
                if (sb_port1.ToString().IndexOf(Convert.ToChar(3)) > -1)//3 i-e ETX is the RecordTerminator of Sysmex-KX21
                {

                    //Console.WriteLine("In after terminator");

                    string fullText = sb_port1.ToString();
                    //string content = fullText.Substring(0, fullText.IndexOf(thismachinesettings.RecordTerminator) + thismachinesettings.RecordTerminator.Length);
                    //Console.WriteLine(content);
                    sb_port1.Clear();


                    Logger.LogReceivedData(thismachinesettings.Instrument_Name, fullText);
                    AppendToRichTextBox("Data Received from " + thismachinesettings.Instrument_Name + " " + fullText);
                    ParserDecision.Parsethisandinsert(fullText, thismachinesettings);
                }
            }
        }

        private void serialPort2_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                string PortName = ((SerialPort)sender).PortName.ToString();
                mi_tinstruments thismachinesettings = allInstruments.Where(x => x.Active == "Y" && x.PORT == PortName.Trim()).FirstOrDefault();// _unitOfWork.InstrumentsRepository.GetSingle(x => x.Active == "Y" && x.PORT == PortName.Trim());
                if (thismachinesettings == null)
                {
                    Logger.LogExceptions("Machine at Port: " + PortName + " not registered");
                    return;
                }

                //DataReceived(sender, machineSettings);
                string data = "";


                data = ((System.IO.Ports.SerialPort)sender).ReadExisting();

                if (data.Length > 0)
                {
                    HandleReceivedData(thismachinesettings, sb_port2, data);

                }




            }
            catch (Exception ee)
            {

                Logger.LogExceptions("Following Exception occured in serialPort2 datarecieved method please check." + ee.ToString());

            }

        }
        private void serialPort3_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                string PortName = ((SerialPort)sender).PortName.ToString();
                mi_tinstruments thismachinesettings = allInstruments.Where(x => x.Active == "Y" && x.PORT == PortName.Trim()).FirstOrDefault();// _unitOfWork.InstrumentsRepository.GetSingle(x => x.Active == "Y" && x.PORT == PortName.Trim());
                if (thismachinesettings == null)
                {
                    Logger.LogExceptions("Machine at Port: " + PortName + " not registered");
                    return;
                }

                //DataReceived(sender, machineSettings);
                string data = "";


                data = ((System.IO.Ports.SerialPort)sender).ReadExisting();

                if (data.Length > 0)
                {
                    HandleReceivedData(thismachinesettings, sb_port3, data);

                }




            }
            catch (Exception ee)
            {

                Logger.LogExceptions("Following Exception occured in serialPort3 datarecieved method please check." + ee.ToString());

            }

        }
        private void serialPort4_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                string PortName = ((SerialPort)sender).PortName.ToString();
                mi_tinstruments thismachinesettings = allInstruments.Where(x => x.Active == "Y" && x.PORT == PortName.Trim()).FirstOrDefault();// _unitOfWork.InstrumentsRepository.GetSingle(x => x.Active == "Y" && x.PORT == PortName.Trim());
                if (thismachinesettings == null)
                {
                    Logger.LogExceptions("Machine at Port: " + PortName + " not registered");
                    return;
                }

                //DataReceived(sender, machineSettings);
                string data = "";


                data = ((System.IO.Ports.SerialPort)sender).ReadExisting();

                if (data.Length > 0)
                {


                    if (thismachinesettings.Communication_Stnadard == "ASTM")
                    {
                        serialPort4.Write(new byte[] { 0x06 }, 0, 1);
                        sb_port4.Append(data);
                        if (sb_port4.ToString().IndexOf(thismachinesettings.RecordTerminator) > -1)
                        {
                            //Console.WriteLine("In after terminator");

                            string fullText = sb_port4.ToString();
                            string content = fullText.Substring(0, fullText.IndexOf(thismachinesettings.RecordTerminator) + thismachinesettings.RecordTerminator.Length);
                            //Console.WriteLine(content);
                            sb_port4.Clear();
                            //if (fullText.LastIndexOf(@"H|\^&") > 0)
                            //{
                            //    string remainingContent = fullText.Substring(fullText.LastIndexOf(@"H|\^&"));

                            //    sb_port4.Append(remainingContent);
                            //}

                            Logger.LogReceivedData(thismachinesettings.Instrument_Name, content);
                            AppendToRichTextBox("Data Received from " + thismachinesettings.Instrument_Name + " " + content);
                            ParserDecision.Parsethisandinsert(content, thismachinesettings);
                        }
                    }
                    else if (thismachinesettings.Communication_Stnadard == "Other")
                    {

                        if (!String.IsNullOrEmpty(thismachinesettings.Acknowledgement_code))
                            serialPort4.Write(new byte[] { 0x06 }, 0, 1);//send Ack to machine
                        sb_port4.Append(data);
                        //eventLog1.WriteEntry(data);

                        System.IO.File.AppendAllText(ConfigurationManager.AppSettings["ReceivedDataLogFile"].ToString().Trim(), data);
                        if (sb_port4.ToString().Contains("DE"))//.Split(new string[] { "D ", "DR", "DH", "DQ", "d ", "DA", "dH", "DE" }, StringSplitOptions.RemoveEmptyEntries).Length>0//For Au480 temporary//L|1 is a terminator record according to astm standards
                        {
                            ///if the recieved string contains the terminator
                            ///then parse the record and Clear the string
                            ///Builder for next Record.
                            ///


                            string parsingdata = sb_port4.ToString();
                            sb_port4.Clear();
                            ParserDecision.Parsethisandinsert(parsingdata, thismachinesettings);
                            //t.Start();
                            //ParserDecision.Parsethisandinsert(parsingdata, thismachinesettings.ParsingAlgorithm, MachineID);
                            // parsingdata = string.Empty;


                        }
                        else if (sb_port4.ToString().Contains("D ") && sb_port4.ToString().Contains(Convert.ToChar(3)) && sb_port4.ToString().Contains("          "))
                        {


                            string data_tobeparsed = sb_port4.ToString().Substring(sb_port4.ToString().LastIndexOf(Convert.ToChar(3)));
                            ParserDecision.Parsethisandinsert(sb_port4.ToString().Substring(0, sb_port4.ToString().LastIndexOf(Convert.ToChar(3))), thismachinesettings);
                            sb_port4.Clear();
                            sb_port4.Append(data_tobeparsed);
                            //t.Start();
                            //  ParserDecision.Parsethisandinsert(sb_port4.ToString().Substring(0, sb_port4.ToStripng().LastIndexOf(Convert.ToChar(3))), thismachinesettings.ParsingAlgorithm, MachineID);
                        }
                    }
                    else if (thismachinesettings.Communication_Stnadard == "LH750")
                    {

                        if (data[0] == Convert.ToChar(22))
                        {
                            if (is_FirstSYN)
                            {
                                serialPort4.Write(new byte[] { 0x16 }, 0, 1);
                                ReceiveBlockCount = true;
                                is_FirstSYN = false;
                            }
                            else
                            {
                                serialPort4.Write(new byte[] { 0x06 }, 0, 1);

                                string data_toparse = sb_Blocks.ToString();
                                sb_Blocks.Clear();
                                is_FirstSYN = true;
                                ParserDecision.Parsethisandinsert(data_toparse, thismachinesettings);
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
                                    serialPort4.Write(new byte[] { 0x06 }, 0, 1);//send Ack to machine
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
                                sb_Blocks.Append(sb_thisBlock.ToString().Substring(3, sb_thisBlock.ToString().Length - 8));//Remove initial and trailing block headers
                                if (sb_thisBlock.ToString().StartsWith(Convert.ToChar(2) + BlocksCount.ToString().PadLeft(2, '0')))
                                {
                                    //System.IO.File.AppendAllText("E:\\AllBlocks.txt", sb_Blocks.ToString());


                                    startBlockReceiving = false;
                                }
                                sb_thisBlock.Clear();
                                serialPort4.Write(new byte[] { 0x06 }, 0, 1);


                            }

                            //send Ack to machine

                        }
                    }
                    else if (thismachinesettings.Communication_Stnadard == "Sysmex-KX21")
                    {
                        sb_port4.Append(data);
                        if (sb_port4.ToString().IndexOf(Convert.ToChar(3)) > -1)//3 i-e ETX is the RecordTerminator of Sysmex-KX21
                        {

                            //Console.WriteLine("In after terminator");

                            string fullText = sb_port4.ToString();
                            //string content = fullText.Substring(0, fullText.IndexOf(thismachinesettings.RecordTerminator) + thismachinesettings.RecordTerminator.Length);
                            //Console.WriteLine(content);
                            sb_port4.Clear();


                            Logger.LogReceivedData(thismachinesettings.Instrument_Name, fullText);
                            AppendToRichTextBox("Data Received from " + thismachinesettings.Instrument_Name + " " + fullText);
                            ParserDecision.Parsethisandinsert(fullText, thismachinesettings);
                        }
                    }

                }




            }
            catch (Exception ee)
            {

                Logger.LogExceptions("Following Exception occured in serialPort4 datarecieved method please check." + ee.ToString());

            }

        }
        private void serialPort5_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                string PortName = ((SerialPort)sender).PortName.ToString();
                mi_tinstruments thismachinesettings = allInstruments.Where(x => x.Active == "Y" && x.PORT == PortName.Trim()).FirstOrDefault();// _unitOfWork.InstrumentsRepository.GetSingle(x => x.Active == "Y" && x.PORT == PortName.Trim());
                if (thismachinesettings == null)
                {
                    Logger.LogExceptions("Machine at Port: " + PortName + " not registered");
                    return;
                }

                //DataReceived(sender, machineSettings);
                string data = "";


                data = ((System.IO.Ports.SerialPort)sender).ReadExisting();

                if (data.Length > 0)
                {


                    if (thismachinesettings.Communication_Stnadard == "ASTM")
                    {
                        serialPort5.Write(new byte[] { 0x06 }, 0, 1);
                        sb_port5.Append(data);
                        if (sb_port5.ToString().IndexOf(thismachinesettings.RecordTerminator) > -1)
                        {
                            //Console.WriteLine("In after terminator");

                            string fullText = sb_port5.ToString();
                            string content = fullText.Substring(0, fullText.IndexOf(thismachinesettings.RecordTerminator) + thismachinesettings.RecordTerminator.Length);
                            //Console.WriteLine(content);
                            sb_port5.Clear();
                            //if (fullText.LastIndexOf(@"H|\^&") > 0)
                            //{
                            //    string remainingContent = fullText.Substring(fullText.LastIndexOf(@"H|\^&"));

                            //    sb_port5.Append(remainingContent);
                            //}

                            Logger.LogReceivedData(thismachinesettings.Instrument_Name, content);
                            AppendToRichTextBox("Data Received from " + thismachinesettings.Instrument_Name + " " + content);
                            ParserDecision.Parsethisandinsert(content, thismachinesettings);
                        }
                    }
                    else if (thismachinesettings.Communication_Stnadard == "Other")
                    {

                        if (!String.IsNullOrEmpty(thismachinesettings.Acknowledgement_code))
                            serialPort5.Write(new byte[] { 0x06 }, 0, 1);//send Ack to machine
                        sb_port5.Append(data);
                        //eventLog1.WriteEntry(data);

                        System.IO.File.AppendAllText(ConfigurationManager.AppSettings["ReceivedDataLogFile"].ToString().Trim(), data);
                        if (sb_port5.ToString().Contains("DE"))//.Split(new string[] { "D ", "DR", "DH", "DQ", "d ", "DA", "dH", "DE" }, StringSplitOptions.RemoveEmptyEntries).Length>0//For Au480 temporary//L|1 is a terminator record according to astm standards
                        {
                            ///if the recieved string contains the terminator
                            ///then parse the record and Clear the string
                            ///Builder for next Record.
                            ///


                            string parsingdata = sb_port5.ToString();
                            sb_port5.Clear();
                            ParserDecision.Parsethisandinsert(parsingdata, thismachinesettings);
                            //t.Start();
                            //ParserDecision.Parsethisandinsert(parsingdata, thismachinesettings.ParsingAlgorithm, MachineID);
                            // parsingdata = string.Empty;


                        }
                        else if (sb_port5.ToString().Contains("D ") && sb_port5.ToString().Contains(Convert.ToChar(3)) && sb_port5.ToString().Contains("          "))
                        {


                            string data_tobeparsed = sb_port5.ToString().Substring(sb_port5.ToString().LastIndexOf(Convert.ToChar(3)));
                            ParserDecision.Parsethisandinsert(sb_port5.ToString().Substring(0, sb_port5.ToString().LastIndexOf(Convert.ToChar(3))), thismachinesettings);
                            sb_port5.Clear();
                            sb_port5.Append(data_tobeparsed);
                            //t.Start();
                            //  ParserDecision.Parsethisandinsert(sb_port5.ToString().Substring(0, sb_port5.ToStripng().LastIndexOf(Convert.ToChar(3))), thismachinesettings.ParsingAlgorithm, MachineID);
                        }
                    }
                    else if (thismachinesettings.Communication_Stnadard == "LH750")
                    {

                        if (data[0] == Convert.ToChar(22))
                        {
                            if (is_FirstSYN)
                            {
                                serialPort5.Write(new byte[] { 0x16 }, 0, 1);
                                ReceiveBlockCount = true;
                                is_FirstSYN = false;
                            }
                            else
                            {
                                serialPort5.Write(new byte[] { 0x06 }, 0, 1);

                                string data_toparse = sb_Blocks.ToString();
                                sb_Blocks.Clear();
                                is_FirstSYN = true;
                                ParserDecision.Parsethisandinsert(data_toparse, thismachinesettings);
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
                                    serialPort5.Write(new byte[] { 0x06 }, 0, 1);//send Ack to machine
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
                                sb_Blocks.Append(sb_thisBlock.ToString().Substring(3, sb_thisBlock.ToString().Length - 8));//Remove initial and trailing block headers
                                if (sb_thisBlock.ToString().StartsWith(Convert.ToChar(2) + BlocksCount.ToString().PadLeft(2, '0')))
                                {
                                    //System.IO.File.AppendAllText("E:\\AllBlocks.txt", sb_Blocks.ToString());


                                    startBlockReceiving = false;
                                }
                                sb_thisBlock.Clear();
                                serialPort5.Write(new byte[] { 0x06 }, 0, 1);


                            }

                            //send Ack to machine

                        }
                    }
                    else if (thismachinesettings.Communication_Stnadard == "Sysmex-KX21")
                    {
                        sb_port5.Append(data);
                        if (sb_port5.ToString().IndexOf(Convert.ToChar(3)) > -1)//3 i-e ETX is the RecordTerminator of Sysmex-KX21
                        {

                            //Console.WriteLine("In after terminator");

                            string fullText = sb_port5.ToString();
                            //string content = fullText.Substring(0, fullText.IndexOf(thismachinesettings.RecordTerminator) + thismachinesettings.RecordTerminator.Length);
                            //Console.WriteLine(content);
                            sb_port5.Clear();


                            Logger.LogReceivedData(thismachinesettings.Instrument_Name, fullText);
                            AppendToRichTextBox("Data Received from " + thismachinesettings.Instrument_Name + " " + fullText);
                            ParserDecision.Parsethisandinsert(fullText, thismachinesettings);
                        }
                    }

                }




            }
            catch (Exception ee)
            {

                Logger.LogExceptions("Following Exception occured in serialPort5 datarecieved method please check." + ee.ToString());

            }

        }
        private void serialPort6_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                string PortName = ((SerialPort)sender).PortName.ToString();
                mi_tinstruments thismachinesettings = allInstruments.Where(x => x.Active == "Y" && x.PORT == PortName.Trim()).FirstOrDefault();// _unitOfWork.InstrumentsRepository.GetSingle(x => x.Active == "Y" && x.PORT == PortName.Trim());
                if (thismachinesettings == null)
                {
                    Logger.LogExceptions("Machine at Port: " + PortName + " not registered");
                    return;
                }

                //DataReceived(sender, machineSettings);
                string data = "";


                data = ((System.IO.Ports.SerialPort)sender).ReadExisting();

                if (data.Length > 0)
                {


                    if (thismachinesettings.Communication_Stnadard == "ASTM")
                    {
                        serialPort6.Write(new byte[] { 0x06 }, 0, 1);
                        sb_port6.Append(data);
                        if (sb_port6.ToString().IndexOf(thismachinesettings.RecordTerminator) > -1)
                        {
                            //Console.WriteLine("In after terminator");

                            string fullText = sb_port6.ToString();
                            string content = fullText.Substring(0, fullText.IndexOf(thismachinesettings.RecordTerminator) + thismachinesettings.RecordTerminator.Length);
                            //Console.WriteLine(content);
                            sb_port6.Clear();
                            //if (fullText.LastIndexOf(@"H|\^&") > 0)
                            //{
                            //    string remainingContent = fullText.Substring(fullText.LastIndexOf(@"H|\^&"));

                            //    sb_port6.Append(remainingContent);
                            //}

                            Logger.LogReceivedData(thismachinesettings.Instrument_Name, content);
                            AppendToRichTextBox("Data Received from " + thismachinesettings.Instrument_Name + " " + content);
                            ParserDecision.Parsethisandinsert(content, thismachinesettings);
                        }
                    }
                    else if (thismachinesettings.Communication_Stnadard == "Other")
                    {

                        if (!String.IsNullOrEmpty(thismachinesettings.Acknowledgement_code))
                            serialPort6.Write(new byte[] { 0x06 }, 0, 1);//send Ack to machine
                        sb_port6.Append(data);
                        //eventLog1.WriteEntry(data);

                        System.IO.File.AppendAllText(ConfigurationManager.AppSettings["ReceivedDataLogFile"].ToString().Trim(), data);
                        if (sb_port6.ToString().Contains("DE"))//.Split(new string[] { "D ", "DR", "DH", "DQ", "d ", "DA", "dH", "DE" }, StringSplitOptions.RemoveEmptyEntries).Length>0//For Au480 temporary//L|1 is a terminator record according to astm standards
                        {
                            ///if the recieved string contains the terminator
                            ///then parse the record and Clear the string
                            ///Builder for next Record.
                            ///


                            string parsingdata = sb_port6.ToString();
                            sb_port6.Clear();
                            ParserDecision.Parsethisandinsert(parsingdata, thismachinesettings);
                            //t.Start();
                            //ParserDecision.Parsethisandinsert(parsingdata, thismachinesettings.ParsingAlgorithm, MachineID);
                            // parsingdata = string.Empty;


                        }
                        else if (sb_port6.ToString().Contains("D ") && sb_port6.ToString().Contains(Convert.ToChar(3)) && sb_port6.ToString().Contains("          "))
                        {


                            string data_tobeparsed = sb_port6.ToString().Substring(sb_port6.ToString().LastIndexOf(Convert.ToChar(3)));
                            ParserDecision.Parsethisandinsert(sb_port6.ToString().Substring(0, sb_port6.ToString().LastIndexOf(Convert.ToChar(3))), thismachinesettings);
                            sb_port6.Clear();
                            sb_port6.Append(data_tobeparsed);
                            //t.Start();
                            //  ParserDecision.Parsethisandinsert(sb_port6.ToString().Substring(0, sb_port6.ToStripng().LastIndexOf(Convert.ToChar(3))), thismachinesettings.ParsingAlgorithm, MachineID);
                        }
                    }
                    else if (thismachinesettings.Communication_Stnadard == "LH750")
                    {

                        if (data[0] == Convert.ToChar(22))
                        {
                            if (is_FirstSYN)
                            {
                                serialPort6.Write(new byte[] { 0x16 }, 0, 1);
                                ReceiveBlockCount = true;
                                is_FirstSYN = false;
                            }
                            else
                            {
                                serialPort6.Write(new byte[] { 0x06 }, 0, 1);

                                string data_toparse = sb_Blocks.ToString();
                                sb_Blocks.Clear();
                                is_FirstSYN = true;
                                ParserDecision.Parsethisandinsert(data_toparse, thismachinesettings);
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
                                    serialPort6.Write(new byte[] { 0x06 }, 0, 1);//send Ack to machine
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
                                sb_Blocks.Append(sb_thisBlock.ToString().Substring(3, sb_thisBlock.ToString().Length - 8));//Remove initial and trailing block headers
                                if (sb_thisBlock.ToString().StartsWith(Convert.ToChar(2) + BlocksCount.ToString().PadLeft(2, '0')))
                                {
                                    //System.IO.File.AppendAllText("E:\\AllBlocks.txt", sb_Blocks.ToString());


                                    startBlockReceiving = false;
                                }
                                sb_thisBlock.Clear();
                                serialPort6.Write(new byte[] { 0x06 }, 0, 1);


                            }

                            //send Ack to machine

                        }
                    }
                    else if (thismachinesettings.Communication_Stnadard == "Sysmex-KX21")
                    {
                        sb_port6.Append(data);
                        if (sb_port6.ToString().IndexOf(Convert.ToChar(3)) > -1)//3 i-e ETX is the RecordTerminator of Sysmex-KX21
                        {

                            //Console.WriteLine("In after terminator");

                            string fullText = sb_port6.ToString();
                            //string content = fullText.Substring(0, fullText.IndexOf(thismachinesettings.RecordTerminator) + thismachinesettings.RecordTerminator.Length);
                            //Console.WriteLine(content);
                            sb_port6.Clear();


                            Logger.LogReceivedData(thismachinesettings.Instrument_Name, fullText);
                            AppendToRichTextBox("Data Received from " + thismachinesettings.Instrument_Name + " " + fullText);
                            ParserDecision.Parsethisandinsert(fullText, thismachinesettings);
                        }
                    }

                }




            }
            catch (Exception ee)
            {

                Logger.LogExceptions("Following Exception occured in serialPort6 datarecieved method please check." + ee.ToString());

            }

        }
        private void serialPort7_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {
                string PortName = ((SerialPort)sender).PortName.ToString();
                mi_tinstruments thismachinesettings = allInstruments.Where(x => x.Active == "Y" && x.PORT == PortName.Trim()).FirstOrDefault();// _unitOfWork.InstrumentsRepository.GetSingle(x => x.Active == "Y" && x.PORT == PortName.Trim());
                if (thismachinesettings == null)
                {
                    Logger.LogExceptions("Machine at Port: " + PortName + " not registered");
                    return;
                }

                //DataReceived(sender, machineSettings);
                string data = "";


                data = ((System.IO.Ports.SerialPort)sender).ReadExisting();

                if (data.Length > 0)
                {


                    if (thismachinesettings.Communication_Stnadard == "ASTM")
                    {
                        serialPort7.Write(new byte[] { 0x06 }, 0, 1);
                        sb_port7.Append(data);
                        if (sb_port7.ToString().IndexOf(thismachinesettings.RecordTerminator) > -1)
                        {
                            //Console.WriteLine("In after terminator");

                            string fullText = sb_port7.ToString();
                            string content = fullText.Substring(0, fullText.IndexOf(thismachinesettings.RecordTerminator) + thismachinesettings.RecordTerminator.Length);
                            //Console.WriteLine(content);
                            sb_port7.Clear();
                            //if (fullText.LastIndexOf(@"H|\^&") > 0)
                            //{
                            //    string remainingContent = fullText.Substring(fullText.LastIndexOf(@"H|\^&"));

                            //    sb_port7.Append(remainingContent);
                            //}

                            Logger.LogReceivedData(thismachinesettings.Instrument_Name, content);
                            AppendToRichTextBox("Data Received from " + thismachinesettings.Instrument_Name + " " + content);
                            ParserDecision.Parsethisandinsert(content, thismachinesettings);
                        }
                    }
                    else if (thismachinesettings.Communication_Stnadard == "Other")
                    {

                        if (!String.IsNullOrEmpty(thismachinesettings.Acknowledgement_code))
                            serialPort7.Write(new byte[] { 0x06 }, 0, 1);//send Ack to machine
                        sb_port7.Append(data);
                        //eventLog1.WriteEntry(data);

                        System.IO.File.AppendAllText(ConfigurationManager.AppSettings["ReceivedDataLogFile"].ToString().Trim(), data);
                        if (sb_port7.ToString().Contains("DE"))//.Split(new string[] { "D ", "DR", "DH", "DQ", "d ", "DA", "dH", "DE" }, StringSplitOptions.RemoveEmptyEntries).Length>0//For Au480 temporary//L|1 is a terminator record according to astm standards
                        {
                            ///if the recieved string contains the terminator
                            ///then parse the record and Clear the string
                            ///Builder for next Record.
                            ///


                            string parsingdata = sb_port7.ToString();
                            sb_port7.Clear();
                            ParserDecision.Parsethisandinsert(parsingdata, thismachinesettings);
                            //t.Start();
                            //ParserDecision.Parsethisandinsert(parsingdata, thismachinesettings.ParsingAlgorithm, MachineID);
                            // parsingdata = string.Empty;


                        }
                        else if (sb_port7.ToString().Contains("D ") && sb_port7.ToString().Contains(Convert.ToChar(3)) && sb_port7.ToString().Contains("          "))
                        {


                            string data_tobeparsed = sb_port7.ToString().Substring(sb_port7.ToString().LastIndexOf(Convert.ToChar(3)));
                            ParserDecision.Parsethisandinsert(sb_port7.ToString().Substring(0, sb_port7.ToString().LastIndexOf(Convert.ToChar(3))), thismachinesettings);
                            sb_port7.Clear();
                            sb_port7.Append(data_tobeparsed);
                            //t.Start();
                            //  ParserDecision.Parsethisandinsert(sb_port7.ToString().Substring(0, sb_port7.ToStripng().LastIndexOf(Convert.ToChar(3))), thismachinesettings.ParsingAlgorithm, MachineID);
                        }
                    }
                    else if (thismachinesettings.Communication_Stnadard == "LH750")
                    {

                        if (data[0] == Convert.ToChar(22))
                        {
                            if (is_FirstSYN)
                            {
                                serialPort7.Write(new byte[] { 0x16 }, 0, 1);
                                ReceiveBlockCount = true;
                                is_FirstSYN = false;
                            }
                            else
                            {
                                serialPort7.Write(new byte[] { 0x06 }, 0, 1);

                                string data_toparse = sb_Blocks.ToString();
                                sb_Blocks.Clear();
                                is_FirstSYN = true;
                                ParserDecision.Parsethisandinsert(data_toparse, thismachinesettings);
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
                                    serialPort7.Write(new byte[] { 0x06 }, 0, 1);//send Ack to machine
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
                                sb_Blocks.Append(sb_thisBlock.ToString().Substring(3, sb_thisBlock.ToString().Length - 8));//Remove initial and trailing block headers
                                if (sb_thisBlock.ToString().StartsWith(Convert.ToChar(2) + BlocksCount.ToString().PadLeft(2, '0')))
                                {
                                    //System.IO.File.AppendAllText("E:\\AllBlocks.txt", sb_Blocks.ToString());


                                    startBlockReceiving = false;
                                }
                                sb_thisBlock.Clear();
                                serialPort7.Write(new byte[] { 0x06 }, 0, 1);


                            }

                            //send Ack to machine

                        }
                    }
                    else if (thismachinesettings.Communication_Stnadard == "Sysmex-KX21")
                    {
                        sb_port7.Append(data);
                        if (sb_port7.ToString().IndexOf(Convert.ToChar(3)) > -1)//3 i-e ETX is the RecordTerminator of Sysmex-KX21
                        {

                            //Console.WriteLine("In after terminator");

                            string fullText = sb_port7.ToString();
                            //string content = fullText.Substring(0, fullText.IndexOf(thismachinesettings.RecordTerminator) + thismachinesettings.RecordTerminator.Length);
                            //Console.WriteLine(content);
                            sb_port7.Clear();


                            Logger.LogReceivedData(thismachinesettings.Instrument_Name, fullText);
                            AppendToRichTextBox("Data Received from " + thismachinesettings.Instrument_Name + " " + fullText);
                            ParserDecision.Parsethisandinsert(fullText, thismachinesettings);
                        }
                    }

                }




            }
            catch (Exception ee)
            {

                Logger.LogExceptions("Following Exception occured in serialPort7 datarecieved method please check." + ee.ToString());

            }

        }
        private void AppendToRichTextBox(string Msg)
        {
            if (rtbCommunicationEvents.InvokeRequired)
            {
                rtbCommunicationEvents.Invoke(new EventHandler(delegate
                {
                    if (rtbCommunicationEvents.Text.Length > 5000)
                        rtbCommunicationEvents.Clear();
                    rtbCommunicationEvents.AppendText(Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + "\t" + Msg);
                    rtbCommunicationEvents.ScrollToCaret();
                }));

            }
            else
            {
                if (rtbCommunicationEvents.Text.Length > 5000)
                    rtbCommunicationEvents.Clear();
                rtbCommunicationEvents.AppendText(Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt") + "\t" + Msg);
                rtbCommunicationEvents.ScrollToCaret();
            }
        }
    }
}
