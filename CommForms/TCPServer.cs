using BusinessLayer;
using BusinessLayer.Parsers;
using Common;
using DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsApplication5.CommForms
{
    public partial class TCPServer : Form
    {
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        private TabControl tabCtrl;
        private TabPage tabPag;
        private Thread th;
        
        public volatile bool m_StopThread;

        public TCPServer()
        {
            InitializeComponent();
        }


        public TabPage TabPag
        {
            get
            {
                return tabPag;
            }
            set
            {
                tabPag = value;
            }
        }

        public TabControl TabCtrl
        {
            set
            {
                tabCtrl = value;
            }
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            if (btnStartServer.Tag.ToString() == "DisConnected")
            {
                th = new Thread(new ThreadStart(StartListening));
                th.IsBackground = true;
                th.Start();
            }
            else
            {
                btnStartServer.Text = "Start Listening";
                btnStartServer.BackColor = SystemColors.Control;
                btnStartServer.Tag = "DisConnected";
                m_StopThread = true;
               // th.Abort();
                //th = null;


            }
            // StartListening(IpAddress,Port);


        }
        public void StartListening1()
        {
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".  
            // IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());


            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");//ipHostInfo.AddressList.Length>1?ipHostInfo.AddressList[1]:ipHostInfo.AddressList[0];
                                                               //Form1 frm = new Form1();
                                                               //frm.ChangeText("Listening on IP:" + ipAddress.ToString());

            //if (txtEvents.InvokeRequired)
            //{
            //    txtEvents.Invoke(new MethodInvoker(delegate { txtEvents.Text = "Listening on IP:" + ipAddress.ToString(); }));
            //}
            // readData = "Listening on IP:" + ipAddress.ToString();
            //Log(1, readData);
            // SetText();
            // UpdateClientList("Listening on IP:" + ipAddress.ToString());
            //        IPAddress ipAddress = IPAddress.Parse(System.Configuration.ConfigurationSettings.AppSettings["IpAddress"].ToString().Trim());//ipHostInfo.AddressList.Length>1?ipHostInfo.AddressList[1]:ipHostInfo.AddressList[0];
            // Console.WriteLine("Listening on IP:" + ipAddress.ToString());

            int Port = 0;
            bool x = int.TryParse("5555", out Port);
            //bool x = int.TryParse(System.Configuration.ConfigurationSettings.AppSettings["Port"].ToString().Trim(), out Port);
            //if (!x)
            //    if (txtEvents.InvokeRequired)
            //    {
            //        txtEvents.Invoke(new MethodInvoker(delegate { txtEvents.Text = "Port not correct"; }));
            //    }
            //                Console.WriteLine("Port not correct");

            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, Port);

            // Create a TCP/IP socket.  
            Socket listener = new Socket(ipAddress.AddressFamily,
                SocketType.Stream, ProtocolType.Tcp);


            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);

                while (true)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();


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
                //LogExceptions("\r\n" + e.ToString());
                // frm.txtEvents.Text += e.ToString();
                //Console.WriteLine(e.ToString());
            }

            //Console.WriteLine("\nPress ENTER to continue...");
            //Console.Read();

        }
        public void StartListening()
        {
            string IpAddress = txtIpAddress.Text;
            string Portt = txtPort.Text;
            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.  
            // The DNS name of the computer  
            // running the listener is "host.contoso.com".  
            // IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());

            IPAddress ipAddress = IPAddress.Parse(IpAddress);//ipHostInfo.AddressList.Length>1?ipHostInfo.AddressList[1]:ipHostInfo.AddressList[0];
            //Console.WriteLine("Listening on IP:" + ipAddress.ToString());
            int Port = 0;
            bool x = int.TryParse(Portt, out Port);
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
                AppendToRichTextBox("Server Started Port:" + Port.ToString());
                btnStartServer.Invoke(new EventHandler(delegate
                {
                    btnStartServer.Enabled = false;// = Color.Green;
                    txtIpAddress.Enabled = false;
                    txtPort.Enabled = false;

                }));
                while (!m_StopThread)
                {
                    // Set the event to nonsignaled state.  
                    allDone.Reset();
                   
                   

                    // Start an asynchronous socket to listen for connections.  
                    //Console.WriteLine("Waiting for a connection at: " + localEndPoint.Address.ToString() + ":" + localEndPoint.Port.ToString());
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
                //Console.WriteLine(e.ToString());
            }

            //Console.WriteLine("\nPress ENTER to continue...");
            //Console.Read();

        }

        public void AcceptCallback(IAsyncResult ar)
        {
            // Signal the main thread to continue.  
            allDone.Set();

            // Get the socket that handles the client request.  
            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);
            //Console.WriteLine("Connected To: " + (handler.RemoteEndPoint as IPEndPoint).Address.ToString() + ":" + (handler.RemoteEndPoint as IPEndPoint).Port);
            // Create the state object.  
            StateObject state = new StateObject();
            state.workSocket = handler;

            var machineSettings = StaticCache.GetAllInstruments(true).Where(x => x.IpAddress == (handler.RemoteEndPoint as IPEndPoint).Address.ToString()).FirstOrDefault();
            if (machineSettings != null)
            {
                state.thisInstrument = machineSettings;
                handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
                AppendToRichTextBox("New Client Connected IP:"+ (handler.RemoteEndPoint as IPEndPoint).Address.ToString()+" Machine:"+machineSettings.Instrument_Name);

            }
            else
            {
                Logger.LogExceptions("Unregistered machine connected from: " + (handler.RemoteEndPoint as IPEndPoint).Address.ToString());

            }


        }
        public void ReadCallback(IAsyncResult ar)
        {

            try
            {
                StateObject state = (StateObject)ar.AsyncState;

                Socket handler = state.workSocket;
                //Console.WriteLine("In read call back. IP:" + (handler.RemoteEndPoint as IPEndPoint).Address.ToString());
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
                        content = fullText.Substring(0, fullText.IndexOf(machineSettings.RecordTerminator) + machineSettings.RecordTerminator.Length);
                        state.sb.Clear();
                        if (fullText.LastIndexOf(@"H|\^&") > 0)
                        {
                            string remainingContent = fullText.Substring(fullText.LastIndexOf(@"H|\^&"));

                            state.sb.Append(remainingContent);
                        }


                        if (machineSettings != null && machineSettings.CliqInstrumentID.HasValue)
                        {
                            //Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                            //content.Length, content);
                            //if(!File.Exists(System.Configuration.ConfigurationSettings.AppSettings["WriteFilePath"]))
                            //    File.Create(System.Configuration.ConfigurationSettings.AppSettings["WriteFilePath"]);
                            AppendToRichTextBox("Result Packet Received Machine Name: " + machineSettings.Instrument_Name);
                            Logger.LogReceivedData(machineSettings.Instrument_Name, content);
                            ParserDecision.Parsethisandinsert(content, machineSettings);
                        }
                        else
                        {
                            ///Log here Invalid Machine
                            ///
                            //Console.WriteLine("Machine not registered");
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
                //Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                //handler.Shutdown(SocketShutdown.Both);
                //handler.Close();

            }
            catch (Exception e)
            {
                Logger.LogExceptions(e.ToString());
                //Console.WriteLine(e.ToString());
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
        public mi_tinstruments thisInstrument = new mi_tinstruments();
    }
}
