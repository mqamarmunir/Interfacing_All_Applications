using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;
using DataModel;

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
    private static UnitOfWork _unitOfWork;
    // Thread signal.  
    public static ManualResetEvent allDone = new ManualResetEvent(false);

    public AsynchronousSocketListener()
    {
        _unitOfWork = new UnitOfWork();
    }

    public static void StartListening()
    {
        // Data buffer for incoming data.  
        byte[] bytes = new Byte[1024];

        // Establish the local endpoint for the socket.  
        // The DNS name of the computer  
        // running the listener is "host.contoso.com".  
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

        IPAddress ipAddress = IPAddress.Parse(System.Configuration.ConfigurationSettings.AppSettings["IpAddress"].ToString().Trim());//ipHostInfo.AddressList.Length>1?ipHostInfo.AddressList[1]:ipHostInfo.AddressList[0];
        Console.WriteLine("Listening on IP:"+ ipAddress.ToString());
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
                Console.WriteLine("Waiting for a connection...");
                listener.BeginAccept(
                    new AsyncCallback(AcceptCallback),
                    listener);

                // Wait until a connection is made before continuing.  
                allDone.WaitOne();
            }

        }
        catch (Exception e)
        {
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

        // Create the state object.  
        StateObject state = new StateObject();
        state.workSocket = handler;
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
            new AsyncCallback(ReadCallback), state);
    }

    public static void ReadCallback(IAsyncResult ar)
    {
        String content = String.Empty;

        // Retrieve the state object and the handler socket  
        // from the asynchronous state object.  
        StateObject state = (StateObject)ar.AsyncState;
        Socket handler = state.workSocket;

        // Read data from the client socket.   
        int bytesRead = handler.EndReceive(ar);

        if (bytesRead > 0)
        {
            // There  might be more data, so store the data received so far.  
            state.sb.Append(Encoding.ASCII.GetString(
                state.buffer, 0, bytesRead));

            //byte[] ack=;
            

            // Check for end-of-file tag. If it is not there, read   
            // more data.  
            content = state.sb.ToString();
            if (content.IndexOf("L|1") > -1)
            {
                // All the data has been read from the   
                // client. Display it on the console.  
                Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                    content.Length, content);
                if(!File.Exists(System.Configuration.ConfigurationSettings.AppSettings["WriteFilePath"]))
                    File.Create(System.Configuration.ConfigurationSettings.AppSettings["WriteFilePath"]);
                File.AppendAllText(System.Configuration.ConfigurationSettings.AppSettings["WriteFilePath"].ToString(), content);
                Parsethisandinsert(content, 1, "3");
                // Echo the data back to the client.  
                Send(handler, new byte[] { 0x06 });//send ack
            }
            else
            {
                // Not all data received. Get more.  
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
            }
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

            handler.Shutdown(SocketShutdown.Both);
            handler.Close();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static int Main(String[] args)
    {
        //StartListening();
        ParseBeckManHematology();

        Console.ReadLine();
        return 0;
    }

    private static void ParseBeckManHematology()
    {
        var data = System.IO.File.ReadAllText("E:\\WriteMe.txt");
        var x = data.Split(new char[]{Convert.ToChar(3)});
        Parsethisandinsert(data, 1, "1");
        Console.WriteLine("abc");
        //foreach (char c in data.ToCharArray())
        //{
        //    
        //    Console.WriteLine(c+"     "+Convert.ToByte(c));
        //    System.IO.File.AppendAllText("E:\\writebytes.txt",c + "      " + Convert.ToByte(c)+"\n");
        //}
    }
    private static void Parsethisandinsert(string data, int Parsingalgorithm, string MachineID)
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
    private static void ParseASTMData(string data, string MachineID)
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
                        Console.WriteLine("Exception on getting Patientid: " + ee.ToString());
                    }
                }
                else if (def[j].Contains("O|") && def[j].Contains("R|") && def[j].IndexOf("O|") < def[j].IndexOf("R|"))
                {
                    ///Get lab ID
                    string[] order = def[j].Split(sep3);
                    labid = order[2].ToString();
                    if (String.IsNullOrEmpty(labid))
                    {
                        labid = order[3];
                    }
                    if (labid.Contains("^"))
                    {
                        string[] splitlabid = labid.Split(sep4);
                        labid = splitlabid[1].ToString().Trim().Length<4 ? splitlabid[2].Trim():splitlabid[1].Trim();

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

                    var objresult = new DataModel.mi_tresult
                    {
                        BookingID = labid,
                        AttributeID = attribcode,
                        ClientID = "1",//System.Configuration.ConfigurationSettings.AppSettings["BranchID"].ToString().Trim(),
                        EnteredBy = 1,
                        EnteredOn = System.DateTime.Now,//.ToString("yyyy-MM-dd hh:mm:ss tt"),
                        machinename = MachineID,
                        Result = attribresult,
                        Status = "N"
                    };
                    var resultserialized = Newtonsoft.Json.JsonConvert.SerializeObject(objresult);
                    Console.WriteLine(MachineID + " Serialized result: " + resultserialized);
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
           // _unitOfWork.Save();
        }
        catch (Exception ee)
        {
            //Console.WriteLine("On Saving to local results table: " + ee.ToString(), EventLogEntryType.Error);
        }

    }
}