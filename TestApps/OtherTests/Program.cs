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
using Common;
using BusinessLayer.Parsers;

namespace OtherTests
{

    public class AsynchronousSocketListener
    {
        private static System.Timers.Timer timer;//=new System.Timers.Timer(60000D);
        private static UnitOfWork _unitOfWork = new UnitOfWork();
        private static Parser parser = new Parser();
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
                Logger.LogExceptions("\r\n" + e.ToString());
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
                            Logger.LogReceivedData(machineSettings.Instrument_Name + "_" + machineSettings.Model, content);
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
                Logger.LogExceptions("\r\n" + ee.ToString());

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
                Logger.LogExceptions("\r\n" + e.ToString());
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
            StartListening();
            //ParseBeckManHematology();
            // UpdateRemoteDatabase(null,null);
            Console.ReadLine();
            return 0;
        }

        private static void UpdateRemoteDatabase(object sender, ElapsedEventArgs e)
        {
            timer.Stop();
            #region Web Service Methodology
            clsBLMain objMai = new clsBLMain();
            DataView dv = objMai.GetAll(9);
            if (dv.Count > 0)
            {
                //Console.WriteLine("Found results:" + dv.Count.ToString());
                try
                {
                    List<cliqresultsNew> lstresults = new List<cliqresultsNew>();
                    for (int i = 0; i < dv.Count; i++)
                    {
                        lstresults.Add(new cliqresultsNew
                        {
                            ResultID = Convert.ToInt32(dv[i]["ResultID"].ToString().Trim()),
                            BookingID = Convert.ToInt64(dv[i]["BookingID"].ToString().Trim()),
                            ClientID = Convert.ToInt32(dv[i]["ClientID"].ToString().Trim()),

                            CliqMachineID = Convert.ToInt32(dv[i]["CliqInstrumentId"].ToString().Trim()),
                            Result = dv[i]["Result"].ToString().Trim(),
                            MachineAttributeCode = dv[i]["AttributeId"].ToString().Trim()

                        });
                    }

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
                    Logger.LogSentDataToServer(json);
                    var content = Helper.PostResultsToCliq(System.Configuration.ConfigurationSettings.AppSettings["WebServicebasePath"].ToString().Trim(), json);
                    Logger.LogReceivedDataFromServer(Newtonsoft.Json.JsonConvert.SerializeObject(new { content.StatusCode, content.Content, content.StatusDescription }));
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
                                Logger.LogExceptions(ee.ToString());
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
                                objMain.Update();

                            }
                            catch (Exception ee)
                            {
                                Logger.LogExceptions(ee.ToString());

                                Console.WriteLine("Error while updating local record id: " + result.ResultID.ToString() + "-------" + ee.ToString());
                            }

                        }
                    }
                }
                catch (Exception ee)
                {
                    Logger.LogExceptions("\r\n" + ee.ToString());
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


        private static void Parsethisandinsert(string data, int Parsingalgorithm, long MachineID)
        {

            switch (Parsingalgorithm)
            {
                ///According to ASTM standard 
                ///tested on 
                ///sysmex xs800i,cobase411,cobasu411(urine analyzer)
                ///
                case 1:
                    parser.ParseASTMData(data, MachineID);
                    break;//case 1 ends here
                case 2://AU480 Beckman
                       //ParseAu480(data, MachineID);
                    break;
            }
        }


    }
}