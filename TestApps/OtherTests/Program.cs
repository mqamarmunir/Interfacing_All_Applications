using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using DataModel;
using System.Data;
using System.Linq;
using Common;
using BusinessLayer.Parsers;
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
    public mi_tinstruments thisInstrument=new mi_tinstruments();
}

public class AsynchronousSocketListener
{

    // private UnitOfWork _unitOfWork = new UnitOfWork();

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
                Console.WriteLine("Waiting for a connection at: " + localEndPoint.Address.ToString() + ":" + localEndPoint.Port.ToString());
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
            Logger.LogExceptions(e.ToString());
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
        using (var _unitOfWork = new UnitOfWork())
        {
            var machineSettings = _unitOfWork.InstrumentsRepository.GetAll().Where(x => x.IpAddress == (handler.RemoteEndPoint as IPEndPoint).Address.ToString()).FirstOrDefault();
            if (machineSettings != null)
            {
                state.thisInstrument = machineSettings;
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);

            }
            else
            {
                Logger.LogExceptions("Unregistered machine connected from: " + (handler.RemoteEndPoint as IPEndPoint).Address.ToString());

            }
        }
            
    }

    public static void ReadCallback(IAsyncResult ar)
    {

        try
        {
            StateObject state = (StateObject)ar.AsyncState;

            Socket handler = state.workSocket;
            Console.WriteLine("In read call back. IP:" + (handler.RemoteEndPoint as IPEndPoint).Address.ToString());
            SocketError errorCode;

            String content = String.Empty;

            // Retrieve the state object and the handler socket  
            // from the asynchronous state object.  

            // Read data from the client socket.   
            int bytesRead = handler.EndReceive(ar, out errorCode);
            if (errorCode != SocketError.Success)
            {
                Logger.LogExceptions("Socket Error IP: " + (handler.RemoteEndPoint as IPEndPoint).Address.ToString() + " " + errorCode.ToString());
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

                var machineSettings = state.thisInstrument;                
                Send(handler, new byte[] { 0x06 });//send ack
                if (state.sb.ToString().IndexOf(machineSettings.RecordTerminator) > -1)
                {
                    string fullText = state.sb.ToString();
                    content = fullText.Substring(0, fullText.IndexOf(machineSettings.RecordTerminator)+machineSettings.RecordTerminator.Length);
                    state.sb.Clear();
                    if (fullText.LastIndexOf(@"H|\^&") > 0)
                    {
                        string remainingContent = fullText.Substring(fullText.LastIndexOf(@"H|\^&"));

                        state.sb.Append(remainingContent);
                    }
                    
                    
                    if (machineSettings != null && machineSettings.CliqInstrumentID.HasValue)
                    {
                        Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                       content.Length, content);
                        //if(!File.Exists(System.Configuration.ConfigurationSettings.AppSettings["WriteFilePath"]))
                        //    File.Create(System.Configuration.ConfigurationSettings.AppSettings["WriteFilePath"]);
                        Logger.LogReceivedData(machineSettings.Instrument_Name, content);
                        Parsethisandinsert(content,machineSettings);
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
            Logger.LogExceptions(ee.ToString());

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
            Logger.LogExceptions(e.ToString());
            Console.WriteLine(e.ToString());
        }
    }

    public static int Main(String[] args)
    {
        //timer = new System.Timers.Timer(30000D);  // 30000 milliseconds = 30 seconds
        //timer.AutoReset = true;
        //timer.Elapsed += new System.Timers.ElapsedEventHandler(UpdateRemoteDatabase);
        //if (System.Configuration.ConfigurationSettings.AppSettings["IsUpdateRemoteDatabase"].ToString().Trim() == "Y")
        //    timer.Start();
        StartListening();
        //ParseBeckManHematology();
        //  UpdateRemoteDatabase(null,null);
        Console.ReadLine();
        return 0;
    }
    private static void Parsethisandinsert(string data,  mi_tinstruments machineSettings)
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
                //ParseAu480(data, MachineID);
                break;
        }
    }
    





}