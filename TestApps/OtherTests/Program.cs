using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using DataModel;
using System.Timers;

using BusinessLayer;
using BusinessEntities;
using System.Collections.Generic;
using System.Data;
using System.Linq;
// State object for reading client data asynchronously  
public class StateObject
{
    // Client  socket.  
    public Socket workSocket = null;
    // Size of receive buffer.  
    public const int BufferSize = 1024;
    // Receive buffer.  
    public byte[] buffer = new byte[BufferSize];
    // Received data string.  
    public StringBuilder sb = new StringBuilder();
}

public class AsynchronousSocketListener
{
    private static System.Timers.Timer timer;//=new System.Timers.Timer(60000D);
    private static UnitOfWork _unitOfWork = new UnitOfWork();
    // Thread signal.  

    //private static Timer timer_deleteolddata;
    public static ManualResetEvent allDone = new ManualResetEvent(false);

    public AsynchronousSocketListener()
    {

    }

    public static void StartListening()
    {
        // Data buffer for incoming data.  
        byte[] bytes = new Byte[1024];

        // Establish the local endpoint for the socket.  
        // The DNS name of the computer  
        // running the listener is "host.contoso.com".  
        // IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

        IPAddress ipAddress = IPAddress.Parse(System.Configuration.ConfigurationSettings.AppSettings["IpAddress"].ToString().Trim());//ipHostInfo.AddressList.Length>1?ipHostInfo.AddressList[1]:ipHostInfo.AddressList[0];
        Console.WriteLine("Listening on IP:" + ipAddress.ToString());
        int Port = 0;
        bool x = int.TryParse(System.Configuration.ConfigurationSettings.AppSettings["Port"].ToString().Trim(), out Port);
        if (!x)
            Console.WriteLine("Port not correct");

        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, Port);

        // Create a TCP/IP socket.  
        Socket listener = new Socket(ipAddress.AddressFamily,
            SocketType.Stream, ProtocolType.Tcp);

        // Bind the socket to the local endpoint and listen for incoming connections.  
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(100);

            while (true)
            {
                // Set the event to nonsignaled state.  
                allDone.Reset();

                // Start an asynchronous socket to listen for connections.  
                Console.WriteLine("Waiting for a connection at: "+localEndPoint.Address.ToString()+":"+localEndPoint.Port.ToString());
                if (!listener.Connected)
                    listener.BeginAccept(
                    new AsyncCallback(AcceptCallback),
                    listener);

                // Wait until a connection is made before continuing.  
                allDone.WaitOne();
            }

        }
        catch (Exception e)
        {
            LogExceptions("\r\n" + e.ToString());
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\nPress ENTER to continue...");
        Console.Read();

    }

    public static void AcceptCallback(IAsyncResult ar)
    {
        // Signal the main thread to continue.  
        allDone.Set();

        // Get the socket that handles the client request.  
        Socket listener = (Socket)ar.AsyncState;
        Socket handler = listener.EndAccept(ar);
        Console.WriteLine("Connected To: " + (handler.RemoteEndPoint as IPEndPoint).Address.ToString());
        // Create the state object.  
        StateObject state = new StateObject();
        state.workSocket = handler;
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
            new AsyncCallback(ReadCallback), state);
    }

    public static void ReadCallback(IAsyncResult ar)
    {
        try
        {


            String content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  
            StateObject state = (StateObject)ar.AsyncState;
            Socket handler = state.workSocket;
            SocketError errorCode;
            // Read data from the client socket.   
            int bytesRead = handler.EndReceive(ar, out errorCode);
            if (errorCode != SocketError.Success)
            {
                bytesRead = 0;
            }

            if (bytesRead > 0)
            {
                // There  might be more data, so store the data received so far.  
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                //byte[] ack=;


                // Check for end-of-file tag. If it is not there, read   
                // more data.  
                Send(handler, new byte[] { 0x06 });//send ack

                if (state.sb.ToString().IndexOf("L|1") > -1)
                {
                    content = state.sb.ToString();
                    state.sb.Clear();
                    var machineSettings = _unitOfWork.InstrumentsRepository.GetAll().Where(x => x.IpAddress == (state.workSocket.RemoteEndPoint as IPEndPoint).Address.ToString()).FirstOrDefault();
                    if (machineSettings != null && machineSettings.CliqInstrumentID.HasValue)
                    {
                        Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                       content.Length, content);
                        //if(!File.Exists(System.Configuration.ConfigurationSettings.AppSettings["WriteFilePath"]))
                        //    File.Create(System.Configuration.ConfigurationSettings.AppSettings["WriteFilePath"]);
                        LogReceivedData(machineSettings.Instrument_Name, content);
                        Parsethisandinsert(content, 1, machineSettings.InstrumentID);
                    }
                    else
                    {
                        ///Log here Invalid Machine
                        ///
                        Console.WriteLine("Machine not registered");
                    }
                    // All the data has been read from the   
                    // client. Display it on the console.  

                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                    // Echo the data back to the client.  

                }
                else
                {
                    // Not all data received. Get more.  
                    handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
        }
        catch (Exception ee)
        {
            LogExceptions("\r\n" + ee.ToString());

        }
    }

    private static void Send(Socket handler, String data)
    {
        // Convert the string data to byte data using ASCII encoding.  
        byte[] byteData = Encoding.ASCII.GetBytes(data);

        // Begin sending the data to the remote device.  
        handler.BeginSend(byteData, 0, byteData.Length, 0,
            new AsyncCallback(SendCallback), handler);
    }
    private static void Send(Socket handler, byte[] byteData)
    {
        // Convert the string data to byte data using ASCII encoding.  
        //byte[] byteData = Encoding.ASCII.GetBytes(data);

        // Begin sending the data to the remote device.  
        handler.BeginSend(byteData, 0, byteData.Length, 0,
            new AsyncCallback(SendCallback), handler);
    }

    private static void SendCallback(IAsyncResult ar)
    {
        try
        {
            // Retrieve the socket from the state object.  
            Socket handler = (Socket)ar.AsyncState;

            // Complete sending the data to the remote device.  
            int bytesSent = handler.EndSend(ar);
            Console.WriteLine("Sent {0} bytes to client.", bytesSent);

            //handler.Shutdown(SocketShutdown.Both);
            //handler.Close();

        }
        catch (Exception e)
        {
            LogExceptions("\r\n" + e.ToString());
            Console.WriteLine(e.ToString());
        }
    }

    public static int Main(String[] args)
    {
        timer = new System.Timers.Timer(30000D);  // 30000 milliseconds = 30 seconds
        timer.AutoReset = true;
        timer.Elapsed += new System.Timers.ElapsedEventHandler(UpdateRemoteDatabase);
        if (System.Configuration.ConfigurationSettings.AppSettings["IsUpdateRemoteDatabase"].ToString().Trim() == "Y")
            timer.Start();
        //StartListening();
        //ParseBeckManHematology();
         UpdateRemoteDatabase(null,null);
        Console.ReadLine();
        return 0;
    }

    private static void UpdateRemoteDatabase(object sender, ElapsedEventArgs e)
    {
        timer.Stop();
        #region Web Service Methodology
        clsBLMain objMai = new clsBLMain();
        var lstresults = objMai.GetAll<cliqresultsNew>(1);
        if (lstresults.ToList().Count > 0)
        {
            //Console.WriteLine("Found results:" + dv.Count.ToString());
            try
            {
                //List<cliqresultsNew> lstresults = new List<cliqresultsNew>();
                //for (int i = 0; i < dv.Count; i++)
                //{
                //    lstresults.Add(new cliqresultsNew
                //    {
                //        ResultID = Convert.ToInt32(dv[i]["ResultID"].ToString().Trim()),
                //        BookingID = Convert.ToInt64(dv[i]["BookingID"].ToString().Trim()),
                //        ClientID = Convert.ToInt32(dv[i]["ClientID"].ToString().Trim()),

                //        CliqMachineID = Convert.ToInt32(dv[i]["CliqInstrumentId"].ToString().Trim()),
                //        Result = dv[i]["Result"].ToString().Trim(),
                //        MachineAttributeCode = dv[i]["AttributeId"].ToString().Trim()

                //    });
                //}

                var groupedresults = (from p in lstresults
                                      group p by new { p.BookingID, p.ClientID } into g
                                      select new cliqResultsBookingwise
                                      {
                                          branch_id = g.Key.ClientID,

                                          order_no = g.Key.BookingID,
                                          data = g.Select(c => new cliqResultBookingwiseDetail
                                          {
                                              attribute_id = c.MachineAttributeCode,
                                              attribute_result = c.Result,
                                              machine_id = c.CliqMachineID
                                          }).ToList()
                                      }
                                    );

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(groupedresults);
                LogSentDataToServer(json);
                var content = Helper.PostResultsToCliq(System.Configuration.ConfigurationSettings.AppSettings["WebServicebasePath"].ToString().Trim(), json);
                LogReceivedDataFromServer(Newtonsoft.Json.JsonConvert.SerializeObject(new { content.StatusCode, content.Content, content.StatusDescription }));
                if (content.IsSuccessful)
                {

                    //var cliqresultresponse = Newtonsoft.Json.JsonConvert.DeserializeObject<CliqResultResponseNew>(content.Content);

                    clsBLMain objMain = new clsBLMain();
                    foreach (var result in lstresults)
                    {
                        //objMain = new clsBLMain();
                        objMain.status = "Y";
                        objMain.Sentto = System.Configuration.ConfigurationSettings.AppSettings["WebServicebasePath"].ToString().Trim();
                        objMain.Senton = System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                        objMain.ResultID = result.ResultID.ToString();
                        try
                        {
                            objMain.Update();

                        }
                        catch (Exception ee)
                        {
                            LogExceptions(ee.ToString());
                            Console.WriteLine("Error while updating local record id: " + result.ResultID.ToString() + "-------" + ee.ToString());
                        }

                    }


                }
                else
                {
                    clsBLMain objMain = new clsBLMain();
                    Console.WriteLine("Some Problem occured in remote call. Call: " + System.Configuration.ConfigurationSettings.AppSettings["WebServicebasePath"].ToString().Trim() + "/ricapi/site/curl_data?str=" + json.ToString().Trim());
                    foreach (var result in lstresults)
                    {
                        //objMain = new clsBLMain();
                        objMain.status = "X";
                        objMain.Sentto = System.Configuration.ConfigurationSettings.AppSettings["WebServicebasePath"].ToString().Trim();
                        objMain.Senton = System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt");
                        objMain.ResultID = result.ResultID.ToString();
                        try
                        {
                            if (!objMain.Update())
                            {
                                LogExceptions(objMain.Error);

                            }

                        }
                        catch (Exception ee)
                        {
                            LogExceptions(ee.ToString());

                            Console.WriteLine("Error while updating local record id: " + result.ResultID.ToString() + "-------" + ee.ToString());
                        }

                    }
                }
            }
            catch (Exception ee)
            {
                LogExceptions("\r\n" + ee.ToString());
                Console.WriteLine(ee.Message.ToString());
                //Console.WriteLine(ee.ToString(), EventLogEntryType.Error);
                // MessageBox.Show(ee.Message);
            }
            finally
            {
                timer.Start();
            }


        }
        else
        {
            Console.WriteLine("No pending results");
            timer.Start();
        }

        #endregion
    }

    private static void ParseBeckManHematology()
    {
        var data = System.IO.File.ReadAllText("E:\\WriteMe.txt");
        var x = data.Split(new char[] { Convert.ToChar(3) });
        Parsethisandinsert(data, 1, 1);
        Console.WriteLine("abc");
        //foreach (char c in data.ToCharArray())
        //{
        //    
        //    Console.WriteLine(c+"     "+Convert.ToByte(c));
        //    System.IO.File.AppendAllText("E:\\writebytes.txt",c + "      " + Convert.ToByte(c)+"\n");
        //}
    }
    private static void Parsethisandinsert(string data, int Parsingalgorithm, long MachineID)
    {

        switch (Parsingalgorithm)
        {
            ///According to ASTM standard 
            ///tested on 
            ///sysmex xs800i,cobase411,cobasu411(urine analyzer)
            ///
            case 1:
                ParseASTMData(data, MachineID);
                break;//case 1 ends here
            case 2://AU480 Beckman
                //ParseAu480(data, MachineID);
                break;
        }
    }
    private static void ParseASTMData(string data, long MachineID)
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
                        LogExceptions("\r\n" + ee.ToString());
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
                            LogExceptions("\r\n" + ee.ToString());
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
            LogExceptions("\r\n" + ee.ToString());
            Console.WriteLine("On Saving to local results table: " + ee.ToString());//, EventLogEntryType.Error);
        }

    }
    public static void LogReceivedData(string MachineID, string data)
    {
        string DirInfo = AppDomain.CurrentDomain.BaseDirectory;

        if (!Directory.Exists(Path.Combine(DirInfo, "ReceivedDataLogFiles")))
            Directory.CreateDirectory(Path.Combine(DirInfo, "ReceivedDataLogFiles"));
        File.AppendAllText(Path.Combine(Path.Combine(DirInfo, "ReceivedDataLogFiles"), MachineID + ".txt"), data);
    }
    public static void LogSentDataToServer(string data)
    {
        string DirInfo = AppDomain.CurrentDomain.BaseDirectory;

        if (!Directory.Exists(Path.Combine(DirInfo, "SentDataToServer")))
            Directory.CreateDirectory(Path.Combine(DirInfo, "SentDataToServer"));
        File.AppendAllText(Path.Combine(Path.Combine(DirInfo, "SentDataToServer"), System.DateTime.Now.ToString("ddMMyyyy") + ".txt"), data);
    }
    public static void LogReceivedDataFromServer(string data)
    {
        string DirInfo = AppDomain.CurrentDomain.BaseDirectory;

        if (!Directory.Exists(Path.Combine(DirInfo, "ReceivedDataFromServer")))
            Directory.CreateDirectory(Path.Combine(DirInfo, "ReceivedDataFromServer"));
        File.AppendAllText(Path.Combine(Path.Combine(DirInfo, "ReceivedDataFromServer"), System.DateTime.Now.ToString("ddMMyyyy") + ".txt"), data);
    }
    public static void LogExceptions(string data)
    {
        try
        {



            string DirInfo = AppDomain.CurrentDomain.BaseDirectory;

            if (!Directory.Exists(Path.Combine(DirInfo, "Exceptions")))
                Directory.CreateDirectory(Path.Combine(DirInfo, "Exceptions"));
            File.AppendAllText(Path.Combine(Path.Combine(DirInfo, "Exceptions"), System.DateTime.Now.ToString("ddMMyyyy") + ".txt"), data);
        }
        catch 
        {

           
        }
    }
}