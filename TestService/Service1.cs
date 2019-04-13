using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;
using System.Configuration;
using MySql.Data.MySqlClient;
using BusinessLayer;
using System.Data.OleDb;
using DataModel;
using BusinessEntities;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using BusinessLayer.Parsers;
using Common;
using System.IO.Ports;

namespace TestService
{
    public partial class Service1 : ServiceBase
    {
        #region global variables
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
        #endregion
        public Service1()
        {
            InitializeComponent();
            //_unitOfWork = new UnitOfWork();
            //eventLog1.LogDisplayName = "TestService";



        }

        protected override void OnStart(string[] args)
        {
            if (ConfigurationManager.AppSettings["RequireOpen_1"] == "Y")
            {
                serialPort1.PortName = ConfigurationManager.AppSettings["PortName_1"].ToString();
                serialPort1.BaudRate = Convert.ToInt16(ConfigurationManager.AppSettings["BaudRate_1"].ToString());
                serialPort1.StopBits = ConfigurationManager.AppSettings["StopBits_1"].ToString() == "1" ? StopBits.One : StopBits.None;
                serialPort1.DataBits = Convert.ToInt16(ConfigurationManager.AppSettings["DataBits_1"].ToString());
                serialPort1.Parity = Parity.None;
                serialPort1.DataReceived += SerialPort1_DataReceived;
                try
                {
                    serialPort1.Open();
                    Logger.LogSerialPortRelatedData("Listening at Port:  " + serialPort1.PortName + ", at BaudRate: " + serialPort1.BaudRate.ToString() + ", DataBits: " + serialPort1.DataBits.ToString());
                }
                catch (Exception ee)
                {
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
                    Logger.LogSerialPortRelatedData("Listening at Port:  " + serialPort2.PortName + ", at BaudRate: " +
                                                                             serialPort2.BaudRate.ToString() + ", DataBits: " +
                                                                             serialPort2.DataBits.ToString());
                }
                catch (Exception ee)
                {
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
                    Logger.LogSerialPortRelatedData("Listening at Port:  " + serialPort3.PortName + ", at BaudRate: " +
                                                                             serialPort3.BaudRate.ToString() + ", DataBits: " +
                                                                             serialPort3.DataBits.ToString());
                }
                catch (Exception ee)
                {
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
                    Logger.LogSerialPortRelatedData("Listening at Port:  " + serialPort4.PortName + ", at BaudRate: " +
                                                                             serialPort4.BaudRate.ToString() + ", DataBits: " +
                                                                             serialPort4.DataBits.ToString());
                }
                catch (Exception ee)
                {
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
                    Logger.LogSerialPortRelatedData("Listening at Port:  " + serialPort5.PortName + ", at BaudRate: " +
                                                                             serialPort5.BaudRate.ToString() + ", DataBits: " +
                                                                             serialPort5.DataBits.ToString());
                }
                catch (Exception ee)
                {
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
                    Logger.LogSerialPortRelatedData("Listening at Port:  " + serialPort6.PortName + ", at BaudRate: " +
                                                                             serialPort6.BaudRate.ToString() + ", DataBits: " +
                                                                             serialPort6.DataBits.ToString());
                }
                catch (Exception ee)
                {
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
                    Logger.LogSerialPortRelatedData("Listening at Port:  " + serialPort7.PortName + ", at BaudRate: " +
                                                                             serialPort7.BaudRate.ToString() + ", DataBits: " +
                                                                             serialPort7.DataBits.ToString());
                }
                catch (Exception ee)
                {
                    Logger.LogExceptions(ee.ToString());

                }

            }




        }


        private void SerialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            try
            {


                Logger.LogSerialPortRelatedData("Data received at Port serial port 1");
                string PortName = ((SerialPort)sender).PortName.ToString();
                mi_tinstruments thismachinesettings;
                using (_unitOfWork = new UnitOfWork())
                {
                    thismachinesettings = _unitOfWork.InstrumentsRepository.GetSingle(x => x.Active == "Y" && x.PORT == PortName.Trim());
                    if (thismachinesettings == null)
                    {
                        Logger.LogExceptions("Machine at Port: " + PortName + " not registered");
                        return;
                    }
                }
                //DataReceived(sender, machineSettings);
                string data = "";


                data = ((System.IO.Ports.SerialPort)sender).ReadExisting();

                if (data.Length > 0)
                {

                    
                    if (thismachinesettings.Communication_Stnadard == "ASTM")
                    {
                        serialPort1.Write(new byte[] { 0x06 }, 0, 1);
                        sb_port1.Append(data);
                        if (data.Contains(thismachinesettings.RecordTerminator))
                        {
                            //Console.WriteLine("In after terminator");

                            string fullText = sb_port1.ToString();
                            string content = fullText.Substring(0, fullText.IndexOf(thismachinesettings.RecordTerminator) + thismachinesettings.RecordTerminator.Length);
                            //Console.WriteLine(content);
                            sb_port1.Clear();
                            if (fullText.LastIndexOf(@"H|\^&") > 0)
                            {
                                string remainingContent = fullText.Substring(fullText.LastIndexOf(@"H|\^&"));

                                sb_port1.Append(remainingContent);
                            }
                            Logger.LogReceivedData(thismachinesettings.Instrument_Name, content);
                            Parsethisandinsert(content, thismachinesettings);
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
                            Parsethisandinsert(parsingdata, thismachinesettings);
                            //t.Start();
                            //Parsethisandinsert(parsingdata, thismachinesettings.ParsingAlgorithm, MachineID);
                            // parsingdata = string.Empty;


                        }
                        else if (sb_port1.ToString().Contains("D ") && sb_port1.ToString().Contains(Convert.ToChar(3)) && sb_port1.ToString().Contains("          "))
                        {


                            string data_tobeparsed = sb_port1.ToString().Substring(sb_port1.ToString().LastIndexOf(Convert.ToChar(3)));
                            Parsethisandinsert(sb_port1.ToString().Substring(0, sb_port1.ToString().LastIndexOf(Convert.ToChar(3))), thismachinesettings);
                            sb_port1.Clear();
                            sb_port1.Append(data_tobeparsed);
                            //t.Start();
                            //  Parsethisandinsert(sb_port1.ToString().Substring(0, sb_port1.ToStripng().LastIndexOf(Convert.ToChar(3))), thismachinesettings.ParsingAlgorithm, MachineID);
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
                                Parsethisandinsert(data_toparse, thismachinesettings);
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

                }




            }
            catch (Exception ee)
            {

                Logger.LogExceptions("Following Exception occured in serialport1 datarecieved method please check." + ee.ToString());

            }

        }
        private void serialPort2_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string PortName = ((SerialPort)sender).PortName.ToString();
            mi_tinstruments thismachinesettings;
            using (_unitOfWork = new UnitOfWork())
            {
                thismachinesettings = _unitOfWork.InstrumentsRepository.GetSingle(x => x.Active == "Y" && x.PORT == PortName.Trim());
                if (thismachinesettings == null)
                {
                    Logger.LogExceptions("Machine at Port: " + PortName + " not registered");
                    return;
                }
            }
            string data = "";
            try
            {

                data = ((System.IO.Ports.SerialPort)sender).ReadExisting();

                if (data.Length > 0)
                {

                    if (thismachinesettings.Communication_Stnadard == "ASTM")
                    {
                        serialPort2.Write(new byte[] { 0x06 }, 0, 1);
                        sb_port2.Append(data);
                        if (data.Contains(thismachinesettings.RecordTerminator))
                        {
                            //Console.WriteLine("In after terminator");

                            string fullText = sb_port2.ToString();
                            string content = fullText.Substring(0, fullText.IndexOf(thismachinesettings.RecordTerminator) + thismachinesettings.RecordTerminator.Length);
                            //Console.WriteLine(content);
                            sb_port2.Clear();
                            if (fullText.LastIndexOf(@"H|\^&") > 0)
                            {
                                string remainingContent = fullText.Substring(fullText.LastIndexOf(@"H|\^&"));

                                sb_port2.Append(remainingContent);
                            }
                            Logger.LogReceivedData(thismachinesettings.Instrument_Name, content);
                            Parsethisandinsert(content, thismachinesettings);
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
                            Parsethisandinsert(parsingdata, thismachinesettings);
                            //t.Start();
                            //Parsethisandinsert(parsingdata, thismachinesettings.ParsingAlgorithm, MachineID);
                            // parsingdata = string.Empty;


                        }
                        else if (sb_port1.ToString().Contains("D ") && sb_port1.ToString().Contains(Convert.ToChar(3)) && sb_port1.ToString().Contains("          "))
                        {


                            string data_tobeparsed = sb_port1.ToString().Substring(sb_port1.ToString().LastIndexOf(Convert.ToChar(3)));
                            Parsethisandinsert(sb_port1.ToString().Substring(0, sb_port1.ToString().LastIndexOf(Convert.ToChar(3))), thismachinesettings);
                            sb_port1.Clear();
                            sb_port1.Append(data_tobeparsed);
                            //t.Start();
                            //  Parsethisandinsert(sb_port1.ToString().Substring(0, sb_port1.ToStripng().LastIndexOf(Convert.ToChar(3))), thismachinesettings.ParsingAlgorithm, MachineID);
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
                                Parsethisandinsert(data_toparse, thismachinesettings);
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

                }


            }
            catch (Exception ee)
            {
                Logger.LogExceptions("Following Exception occured in serialport1 datarecieved method please check." + ee.ToString());

            }

        }

        private void serialPort3_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string PortName = ((SerialPort)sender).PortName.ToString();
            mi_tinstruments thismachinesettings;
            using (_unitOfWork = new UnitOfWork())
            {
                thismachinesettings = _unitOfWork.InstrumentsRepository.GetSingle(x => x.Active == "Y" && x.PORT == PortName.Trim());
                if (thismachinesettings == null)
                {
                    Logger.LogExceptions("Machine at Port: " + PortName + " not registered");
                    return;
                }
            }//DataReceived(sender, machineSettings);
            string data = "";
            try
            {

                data = ((System.IO.Ports.SerialPort)sender).ReadExisting();

                if (data.Length > 0)
                {

                    if (thismachinesettings.Communication_Stnadard == "ASTM")
                    {
                        serialPort3.Write(new byte[] { 0x06 }, 0, 1);
                        sb_port3.Append(data);
                        if (data.Contains(thismachinesettings.RecordTerminator))
                        {
                            //Console.WriteLine("In after terminator");

                            string fullText = sb_port3.ToString();
                            string content = fullText.Substring(0, fullText.IndexOf(thismachinesettings.RecordTerminator) + thismachinesettings.RecordTerminator.Length);
                            //Console.WriteLine(content);
                            sb_port3.Clear();
                            if (fullText.LastIndexOf(@"H|\^&") > 0)
                            {
                                string remainingContent = fullText.Substring(fullText.LastIndexOf(@"H|\^&"));

                                sb_port3.Append(remainingContent);
                            }
                            Logger.LogReceivedData(thismachinesettings.Instrument_Name, content);
                            Parsethisandinsert(content, thismachinesettings);
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
                            Parsethisandinsert(parsingdata, thismachinesettings);
                            //t.Start();
                            //Parsethisandinsert(parsingdata, thismachinesettings.ParsingAlgorithm, MachineID);
                            // parsingdata = string.Empty;


                        }
                        else if (sb_port1.ToString().Contains("D ") && sb_port1.ToString().Contains(Convert.ToChar(3)) && sb_port1.ToString().Contains("          "))
                        {


                            string data_tobeparsed = sb_port1.ToString().Substring(sb_port1.ToString().LastIndexOf(Convert.ToChar(3)));
                            Parsethisandinsert(sb_port1.ToString().Substring(0, sb_port1.ToString().LastIndexOf(Convert.ToChar(3))), thismachinesettings);
                            sb_port1.Clear();
                            sb_port1.Append(data_tobeparsed);
                            //t.Start();
                            //  Parsethisandinsert(sb_port1.ToString().Substring(0, sb_port1.ToStripng().LastIndexOf(Convert.ToChar(3))), thismachinesettings.ParsingAlgorithm, MachineID);
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
                                Parsethisandinsert(data_toparse, thismachinesettings);
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

                }


            }
            catch (Exception ee)
            {
                Logger.LogExceptions("Following Exception occured in serialport1 datarecieved method please check." + ee.ToString());

            }

        }
        private void serialPort4_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string PortName = ((SerialPort)sender).PortName.ToString();
            mi_tinstruments thismachinesettings;
            using (_unitOfWork = new UnitOfWork())
            {
                thismachinesettings = _unitOfWork.InstrumentsRepository.GetSingle(x => x.Active == "Y" && x.PORT == PortName.Trim());
                if (thismachinesettings == null)
                {
                    Logger.LogExceptions("Machine at Port: " + PortName + " not registered");
                    return;
                }
            }
            string data = "";
            try
            {

                data = ((System.IO.Ports.SerialPort)sender).ReadExisting();

                if (data.Length > 0)
                {

                    if (thismachinesettings.Communication_Stnadard == "ASTM")
                    {
                        serialPort4.Write(new byte[] { 0x06 }, 0, 1);
                        sb_port4.Append(data);
                        if (data.Contains(thismachinesettings.RecordTerminator))
                        {
                            //Console.WriteLine("In after terminator");

                            string fullText = sb_port4.ToString();
                            string content = fullText.Substring(0, fullText.IndexOf(thismachinesettings.RecordTerminator) + thismachinesettings.RecordTerminator.Length);
                            //Console.WriteLine(content);
                            sb_port4.Clear();
                            if (fullText.LastIndexOf(@"H|\^&") > 0)
                            {
                                string remainingContent = fullText.Substring(fullText.LastIndexOf(@"H|\^&"));

                                sb_port4.Append(remainingContent);
                            }
                            Logger.LogReceivedData(thismachinesettings.Instrument_Name, content);
                            Parsethisandinsert(content, thismachinesettings);
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
                            Parsethisandinsert(parsingdata, thismachinesettings);
                            //t.Start();
                            //Parsethisandinsert(parsingdata, thismachinesettings.ParsingAlgorithm, MachineID);
                            // parsingdata = string.Empty;


                        }
                        else if (sb_port1.ToString().Contains("D ") && sb_port1.ToString().Contains(Convert.ToChar(3)) && sb_port1.ToString().Contains("          "))
                        {


                            string data_tobeparsed = sb_port1.ToString().Substring(sb_port1.ToString().LastIndexOf(Convert.ToChar(3)));
                            Parsethisandinsert(sb_port1.ToString().Substring(0, sb_port1.ToString().LastIndexOf(Convert.ToChar(3))), thismachinesettings);
                            sb_port1.Clear();
                            sb_port1.Append(data_tobeparsed);
                            //t.Start();
                            //  Parsethisandinsert(sb_port1.ToString().Substring(0, sb_port1.ToStripng().LastIndexOf(Convert.ToChar(3))), thismachinesettings.ParsingAlgorithm, MachineID);
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
                                Parsethisandinsert(data_toparse, thismachinesettings);
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

                }


            }
            catch (Exception ee)
            {
                Logger.LogExceptions("Following Exception occured in serialport1 datarecieved method please check." + ee.ToString());

            }

        }
        private void serialPort5_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string PortName = ((SerialPort)sender).PortName.ToString();
            mi_tinstruments thismachinesettings;
            using (_unitOfWork = new UnitOfWork())
            {
                thismachinesettings = _unitOfWork.InstrumentsRepository.GetSingle(x => x.Active == "Y" && x.PORT == PortName.Trim());
                if (thismachinesettings == null)
                {
                    Logger.LogExceptions("Machine at Port: " + PortName + " not registered");
                    return;
                }
            }
            string data = "";
            try
            {

                data = ((System.IO.Ports.SerialPort)sender).ReadExisting();

                if (data.Length > 0)
                {

                    if (thismachinesettings.Communication_Stnadard == "ASTM")
                    {
                        serialPort5.Write(new byte[] { 0x06 }, 0, 1);
                        sb_port5.Append(data);
                        if (data.Contains(thismachinesettings.RecordTerminator))
                        {
                            //Console.WriteLine("In after terminator");

                            string fullText = sb_port5.ToString();
                            string content = fullText.Substring(0, fullText.IndexOf(thismachinesettings.RecordTerminator) + thismachinesettings.RecordTerminator.Length);
                            //Console.WriteLine(content);
                            sb_port5.Clear();
                            if (fullText.LastIndexOf(@"H|\^&") > 0)
                            {
                                string remainingContent = fullText.Substring(fullText.LastIndexOf(@"H|\^&"));

                                sb_port5.Append(remainingContent);
                            }
                            Logger.LogReceivedData(thismachinesettings.Instrument_Name, content);
                            Parsethisandinsert(content, thismachinesettings);
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
                            Parsethisandinsert(parsingdata, thismachinesettings);
                            //t.Start();
                            //Parsethisandinsert(parsingdata, thismachinesettings.ParsingAlgorithm, MachineID);
                            // parsingdata = string.Empty;


                        }
                        else if (sb_port1.ToString().Contains("D ") && sb_port1.ToString().Contains(Convert.ToChar(3)) && sb_port1.ToString().Contains("          "))
                        {


                            string data_tobeparsed = sb_port1.ToString().Substring(sb_port1.ToString().LastIndexOf(Convert.ToChar(3)));
                            Parsethisandinsert(sb_port1.ToString().Substring(0, sb_port1.ToString().LastIndexOf(Convert.ToChar(3))), thismachinesettings);
                            sb_port1.Clear();
                            sb_port1.Append(data_tobeparsed);
                            //t.Start();
                            //  Parsethisandinsert(sb_port1.ToString().Substring(0, sb_port1.ToStripng().LastIndexOf(Convert.ToChar(3))), thismachinesettings.ParsingAlgorithm, MachineID);
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
                                Parsethisandinsert(data_toparse, thismachinesettings);
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

                }


            }
            catch (Exception ee)
            {
                Logger.LogExceptions("Following Exception occured in serialport1 datarecieved method please check." + ee.ToString());

            }

        }
        private void serialPort6_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string PortName = ((SerialPort)sender).PortName.ToString();
            mi_tinstruments thismachinesettings;
            using (_unitOfWork = new UnitOfWork())
            {
                thismachinesettings = _unitOfWork.InstrumentsRepository.GetSingle(x => x.Active == "Y" && x.PORT == PortName.Trim());
                if (thismachinesettings == null)
                {
                    Logger.LogExceptions("Machine at Port: " + PortName + " not registered");
                    return;
                }
            }
            string data = "";
            try
            {

                data = ((System.IO.Ports.SerialPort)sender).ReadExisting();

                if (data.Length > 0)
                {

                    if (thismachinesettings.Communication_Stnadard == "ASTM")
                    {
                        serialPort6.Write(new byte[] { 0x06 }, 0, 1);
                        sb_port6.Append(data);
                        if (data.Contains(thismachinesettings.RecordTerminator))
                        {
                            //Console.WriteLine("In after terminator");

                            string fullText = sb_port6.ToString();
                            string content = fullText.Substring(0, fullText.IndexOf(thismachinesettings.RecordTerminator) + thismachinesettings.RecordTerminator.Length);
                            //Console.WriteLine(content);
                            sb_port6.Clear();
                            if (fullText.LastIndexOf(@"H|\^&") > 0)
                            {
                                string remainingContent = fullText.Substring(fullText.LastIndexOf(@"H|\^&"));

                                sb_port6.Append(remainingContent);
                            }
                            Logger.LogReceivedData(thismachinesettings.Instrument_Name, content);
                            Parsethisandinsert(content, thismachinesettings);
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
                            Parsethisandinsert(parsingdata, thismachinesettings);
                            //t.Start();
                            //Parsethisandinsert(parsingdata, thismachinesettings.ParsingAlgorithm, MachineID);
                            // parsingdata = string.Empty;


                        }
                        else if (sb_port1.ToString().Contains("D ") && sb_port1.ToString().Contains(Convert.ToChar(3)) && sb_port1.ToString().Contains("          "))
                        {


                            string data_tobeparsed = sb_port1.ToString().Substring(sb_port1.ToString().LastIndexOf(Convert.ToChar(3)));
                            Parsethisandinsert(sb_port1.ToString().Substring(0, sb_port1.ToString().LastIndexOf(Convert.ToChar(3))), thismachinesettings);
                            sb_port1.Clear();
                            sb_port1.Append(data_tobeparsed);
                            //t.Start();
                            //  Parsethisandinsert(sb_port1.ToString().Substring(0, sb_port1.ToStripng().LastIndexOf(Convert.ToChar(3))), thismachinesettings.ParsingAlgorithm, MachineID);
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
                                Parsethisandinsert(data_toparse, thismachinesettings);
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

                }


            }
            catch (Exception ee)
            {
                Logger.LogExceptions("Following Exception occured in serialport1 datarecieved method please check." + ee.ToString());

            }

        }
        private void serialPort7_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            string PortName = ((SerialPort)sender).PortName.ToString();
            mi_tinstruments thismachinesettings;
            using (_unitOfWork = new UnitOfWork())
            {
                thismachinesettings = _unitOfWork.InstrumentsRepository.GetSingle(x => x.Active == "Y" && x.PORT == PortName.Trim());
                if (thismachinesettings == null)
                {
                    Logger.LogExceptions("Machine at Port: " + PortName + " not registered");
                    return;
                }
            }
            string data = "";
            try
            {

                data = ((System.IO.Ports.SerialPort)sender).ReadExisting();

                if (data.Length > 0)
                {

                    if (thismachinesettings.Communication_Stnadard == "ASTM")
                    {
                        serialPort7.Write(new byte[] { 0x06 }, 0, 1);
                        sb_port7.Append(data);
                        if (data.Contains(thismachinesettings.RecordTerminator))
                        {
                            //Console.WriteLine("In after terminator");

                            string fullText = sb_port7.ToString();
                            string content = fullText.Substring(0, fullText.IndexOf(thismachinesettings.RecordTerminator) + thismachinesettings.RecordTerminator.Length);
                            //Console.WriteLine(content);
                            sb_port7.Clear();
                            if (fullText.LastIndexOf(@"H|\^&") > 0)
                            {
                                string remainingContent = fullText.Substring(fullText.LastIndexOf(@"H|\^&"));

                                sb_port7.Append(remainingContent);
                            }
                            Logger.LogReceivedData(thismachinesettings.Instrument_Name, content);
                            Parsethisandinsert(content, thismachinesettings);
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
                            Parsethisandinsert(parsingdata, thismachinesettings);
                            //t.Start();
                            //Parsethisandinsert(parsingdata, thismachinesettings.ParsingAlgorithm, MachineID);
                            // parsingdata = string.Empty;


                        }
                        else if (sb_port1.ToString().Contains("D ") && sb_port1.ToString().Contains(Convert.ToChar(3)) && sb_port1.ToString().Contains("          "))
                        {


                            string data_tobeparsed = sb_port1.ToString().Substring(sb_port1.ToString().LastIndexOf(Convert.ToChar(3)));
                            Parsethisandinsert(sb_port1.ToString().Substring(0, sb_port1.ToString().LastIndexOf(Convert.ToChar(3))), thismachinesettings);
                            sb_port1.Clear();
                            sb_port1.Append(data_tobeparsed);
                            //t.Start();
                            //  Parsethisandinsert(sb_port1.ToString().Substring(0, sb_port1.ToStripng().LastIndexOf(Convert.ToChar(3))), thismachinesettings.ParsingAlgorithm, MachineID);
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
                                Parsethisandinsert(data_toparse, thismachinesettings);
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

                }


            }
            catch (Exception ee)
            {
                Logger.LogExceptions("Following Exception occured in serialport1 datarecieved method please check." + ee.ToString());

            }

        }

        private void Parsethisandinsert(string data, mi_tinstruments machineSettings)
        {
            IParser parser;
            switch (machineSettings.ParsingAlgorithm)
            {
                ///According to ASTM standard 
                ///tested on 
                ///sysmex xs800i,cobase411,cobasu411(urine analyzer)
                ///
                case 1:
                    parser = new ASTM();
                    parser.Parse(data, machineSettings);
                    break;//case 1 ends here
                case 2://AU480 Beckman
                    parser = new AU480();
                    parser.Parse(data, machineSettings);
                    break;
                case 3:
                    parser = new BeckManLH750();
                    parser.Parse(data, machineSettings);
                    break;
            }
        }


        protected override void OnStop()
        {
            try
            {
                Logger.LogSerialPortRelatedData("Service Stopped");
                if (serialPort1.IsOpen)
                    serialPort1.Close();
                if (serialPort2.IsOpen)
                    serialPort2.Close();
                if (serialPort3.IsOpen)
                    serialPort3.Close();
                if (serialPort4.IsOpen)
                    serialPort4.Close();
                if (serialPort5.IsOpen)
                    serialPort5.Close();
                if (serialPort6.IsOpen)
                    serialPort6.Close();
                if (serialPort7.IsOpen)
                    serialPort7.Close();

            }
            catch (Exception ee)
            {
                Logger.LogExceptions(ee.ToString());
                throw;
            }
            finally
            {
                GC.Collect();
                _unitOfWork = null;
            }


            //eventLog1.Clear();
        }


    }
}
