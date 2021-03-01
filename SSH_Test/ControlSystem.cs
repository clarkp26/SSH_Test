using System;
using Crestron.SimplSharp;                          	// For Basic SIMPL# Classes
using Crestron.SimplSharpPro;                       	// For Basic SIMPL#Pro classes
using Crestron.SimplSharpPro.CrestronThread;        	// For Threading
using Crestron.SimplSharpPro.Diagnostics;		    	// For System Monitor Access
using Crestron.SimplSharpPro.DeviceSupport;         	// For Generic Device Support
using Crestron.SimplSharp.Ssh;
using Crestron.SimplSharp.Ssh.Common;
using Crestron.SimplSharpPro.UC;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using Crestron.SimplSharp.CrestronSockets;
using System.Text;
using Crestron.SimplSharp.CrestronWebSocketClient;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using static Crestron.SimplSharp.CrestronWebSocketClient.WebSocketClient;
using Crestron.SimplSharp.Cryptography;

namespace SSH_Test
{
    public class ControlSystem : CrestronControlSystem
    {
        /// <summary>
        /// ControlSystem Constructor. Starting point for the SIMPL#Pro program.
        /// Use the constructor to:
        /// * Initialize the maximum number of threads (max = 400)
        /// * Register devices
        /// * Register event handlers
        /// * Add Console Commands
        /// 
        /// Please be aware that the constructor needs to exit quickly; if it doesn't
        /// exit in time, the SIMPL#Pro program will exit.
        /// 
        /// You cannot send / receive data in the constructor
        /// </summary>
        public ControlSystem()
            : base()
        {
            try
            {
                Thread.MaxNumberOfUserThreads = 20;

                //Subscribe to the controller events (System, Program, and Ethernet)
                CrestronEnvironment.SystemEventHandler += new SystemEventHandler(_ControllerSystemEventHandler);
                CrestronEnvironment.ProgramStatusEventHandler += new ProgramStatusEventHandler(_ControllerProgramEventHandler);
                CrestronEnvironment.EthernetEventHandler += new EthernetEventHandler(_ControllerEthernetEventHandler);

                UcEngine ucEngine = new UcEngine(0x03, this);

                //WebSocketClient webSocketclient = new WebSocketClient
                //{
                //    KeepAlive = false,
                //    VerifyServerCertificate = false,
                //    Port = 443,
                //    //Host = "C4-54B203910FDC"
                //    Host = "10.25.94.48"
                //    //SSL = true
                //};
                //WebSocketClientAsyncConnectCallback webSocketClientAsyncConnectCallback = del;
                //var err = webSocketclient.ConnectEx();
                //ErrorLog.Info("URL: " +(int) err);
                //string send = "reboot";
                //byte[] bytes = Encoding.ASCII.GetBytes(send);
                //System.Security.Cryptography.X509Certificates.X509Certificate x509Certificate = new X509Certificate();
                //Crestron.SimplSharp.Cryptography.X509Certificates.X509Certificate x509Certificate1 = new Crestron.SimplSharp.Cryptography.X509Certificates.X509Certificate(bytes);


                //webSocketclient.SetClientCertificate(x509Certificate1);
                //webSocketclient.Connect();
                ////webSocketClient.Host = "10.25.94.48";
                //ErrorLog.Info("Client Connected: " + webSocketClient.Connected);
                //webSocketClient.Port = 443;
                //webSocketClient.SSL = true;


                //var connectCode = webSocketClient.Connect();

                //ErrorLog.Info("Err Code: " + (int)connectCode);

                //string server = "10.25.94.48";
                //TcpClient client = new TcpClient(server, 41797);
                //SslStream sslStream = new SslStream(client.GetStream(), false, new RemoteCertificateValidationCallback(ValidateServerCertificate), null);
                ////ErrorLog.Info("SSL: " + sslStream.SslProtocol);
                string send = "reboot\r";
                byte[] bytes = Encoding.ASCII.GetBytes(send);
                string send1 = "reboot";
                byte[] bytes1 = Encoding.ASCII.GetBytes(send1);
                //using (sslStream)
                //{
                //    sslStream.AuthenticateAsClient(server);
                //    ErrorLog.Info("SSL: " + sslStream.SslProtocol);
                //    ErrorLog.Info("Can Write: " + sslStream.CanWrite);
                //    //sslStream.Begi
                //    sslStream.Write(bytes);
                //    ErrorLog.Info("Can Write: " + sslStream.CanWrite);

                // This is where you read and send data
                //}
                //client.Close();


                
                SecureTCPClient secureTCPClient = new SecureTCPClient("10.25.94.48",41797,bytes.Length);
                var err1 = secureTCPClient.ConnectToServer();
                ErrorLog.Info("Secure Error Code after connect: "+ (int)err1);
                var sendErr = secureTCPClient.SendData(bytes, bytes.Length);
                ErrorLog.Info("Secure Error Code after Send: " + (int)sendErr);
                ErrorLog.Info("Server Address: " + secureTCPClient.AddressClientConnectedTo);
                ErrorLog.Info("Socket Status: " + (int) secureTCPClient.ClientStatus);


                var sendErr2 = secureTCPClient.SendData(bytes1, bytes1.Length);
                ErrorLog.Info("Secure Error Code after Send2: " + (int)sendErr2);
                var ids =secureTCPClient.DisconnectFromServer();
                ErrorLog.Info("Secure Error Code after Disconnect: " + (int)ids);


            //    TCPClient tCPClient = new TCPClient("10.25.94.48", 41797, 1000);
            //    var err = tCPClient.ConnectToServer();
            //    ErrorLog.Info("Err code: " + (int)err);
            //tCPClient.SendData(bytes,0,1000);
            //    tCPClient.DisconnectFromServer();


                //System.Net.Sockets.TcpClient csnative = new System.Net.Sockets.TcpClient("10.25.94.48", 443);
                //    ErrorLog.Info("Connected: " + csnative.Connected);
                //    SslStream sslStream = new SslStream(csnative.GetStream());
                //    ErrorLog.Info("SSL: " + sslStream.IsAuthenticated);
                //    sslStream.AuthenticateAsClient("10.25.94.24");

                //    ErrorLog.Info("SSL: " + sslStream.SslProtocol);
                //sslStream.AuthenticateAsClient("10.25.94.48");
                //SshClient sshClient = new SshClient("C4-54B203910FDC", 22, "admin", "sfb");
                //sshClient.ErrorOccurred += SshClient_ErrorOccurred1;
                //ErrorLog.Info("Connected? " + (int)tCPClient.ClientStatus);

                //SocketErrorCodes err = tCPClient.ConnectToServer();
                //ErrorLog.Info("SocketErrorCode: " + (int)err);
                //ErrorLog.Info("Connected? " + (int)tCPClient.ClientStatus);

                //sslStream.Write(bytes);
                //tCPClient.SendData(bytes, 0, 1000);
                //SshClient sshClient = new SshClient("C4-54B203910FDC", 4443, "hrav", "2Loughmuck!");
                //sshClient.ErrorOccurred += SshClient_ErrorOccurred;
                //sshClient.Connect();

                //IEnumerable<ForwardedPort> s = sshClient.ForwardedPorts;
                //List<ForwardedPort> list = s.ToList();
                //foreach( var a in list)
                //{
                //    ErrorLog.Info("" + a.IsStarted);

                //}

                //SshClient sshClient = new SshClient("10.25.94.48", 4443, "hrav", "2Loughmuck!");


                //var stream = sshClient.CreateShellStream("terminal", 80, 24, 800, 600, 1024);
                //stream.Write("Reboot");
                //SshCommand sshCommand = new SshCommand();
                //ErrorLog.Info("Connected: " + sshClient.IsConnected);
                //SshCommand cmd = sshClient.RunCommand("info");
                //string res = cmd.Execute();
                //ErrorLog.Info("Response: " + res);
            }
            catch (Exception e)
            {
                //if(e.GetType == SshConnectionException)
                //ErrorLog.Info("Errrrrr : " + (int)e.DisconnectReason);
                ErrorLog.Error("Error in the constructor: {0}", e.Message);
                ErrorLog.Error("Error in the constructor: {0}", e.InnerException);
                ErrorLog.Error("Error in the constructor: {0}", e.ToString());
                //ErrorLog.Error("Error in the constructor: {0}", e.Data);



            }
        }
        public delegate void del();
        
        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
        private void SshClient_ErrorOccurred1(object sender, ExceptionEventArgs e)
        {
            ErrorLog.Info("Error as String: " + e.ToString());
            ErrorLog.Info("Error Type: " + e.GetType());
            ErrorLog.Info("Error Message: " + e.Exception.Message);
        }

        private void SshClient_ErrorOccurred(object sender, ExceptionEventArgs e)
        {
            ErrorLog.Info("Error ssh: " + e.Exception );
            ErrorLog.Info("Error ssh2: " + e.ToString());
            ErrorLog.Info("Error ssh3: " + e.GetType());

        }

        /// <summary>
        /// InitializeSystem - this method gets called after the constructor 
        /// has finished. 
        /// 
        /// Use InitializeSystem to:
        /// * Start threads
        /// * Configure ports, such as serial and verisports
        /// * Start and initialize socket connections
        /// Send initial device configurations
        /// 
        /// Please be aware that InitializeSystem needs to exit quickly also; 
        /// if it doesn't exit in time, the SIMPL#Pro program will exit.
        /// </summary>
        public override void InitializeSystem()
        {
            try
            {

            }
            catch (Exception e)
            {
                ErrorLog.Error("Error in InitializeSystem: {0}", e.Message);
            }
        }

        /// <summary>
        /// Event Handler for Ethernet events: Link Up and Link Down. 
        /// Use these events to close / re-open sockets, etc. 
        /// </summary>
        /// <param name="ethernetEventArgs">This parameter holds the values 
        /// such as whether it's a Link Up or Link Down event. It will also indicate 
        /// wich Ethernet adapter this event belongs to.
        /// </param>
        void _ControllerEthernetEventHandler(EthernetEventArgs ethernetEventArgs)
        {
            switch (ethernetEventArgs.EthernetEventType)
            {//Determine the event type Link Up or Link Down
                case (eEthernetEventType.LinkDown):
                    //Next need to determine which adapter the event is for. 
                    //LAN is the adapter is the port connected to external networks.
                    if (ethernetEventArgs.EthernetAdapter == EthernetAdapterType.EthernetLANAdapter)
                    {
                        //
                    }
                    break;
                case (eEthernetEventType.LinkUp):
                    if (ethernetEventArgs.EthernetAdapter == EthernetAdapterType.EthernetLANAdapter)
                    {

                    }
                    break;
            }
        }

        /// <summary>
        /// Event Handler for Programmatic events: Stop, Pause, Resume.
        /// Use this event to clean up when a program is stopping, pausing, and resuming.
        /// This event only applies to this SIMPL#Pro program, it doesn't receive events
        /// for other programs stopping
        /// </summary>
        /// <param name="programStatusEventType"></param>
        void _ControllerProgramEventHandler(eProgramStatusEventType programStatusEventType)
        {
            switch (programStatusEventType)
            {
                case (eProgramStatusEventType.Paused):
                    //The program has been paused.  Pause all user threads/timers as needed.
                    break;
                case (eProgramStatusEventType.Resumed):
                    //The program has been resumed. Resume all the user threads/timers as needed.
                    break;
                case (eProgramStatusEventType.Stopping):
                    //The program has been stopped.
                    //Close all threads. 
                    //Shutdown all Client/Servers in the system.
                    //General cleanup.
                    //Unsubscribe to all System Monitor events
                    break;
            }

        }

        /// <summary>
        /// Event Handler for system events, Disk Inserted/Ejected, and Reboot
        /// Use this event to clean up when someone types in reboot, or when your SD /USB
        /// removable media is ejected / re-inserted.
        /// </summary>
        /// <param name="systemEventType"></param>
        void _ControllerSystemEventHandler(eSystemEventType systemEventType)
        {
            switch (systemEventType)
            {
                case (eSystemEventType.DiskInserted):
                    //Removable media was detected on the system
                    break;
                case (eSystemEventType.DiskRemoved):
                    //Removable media was detached from the system
                    break;
                case (eSystemEventType.Rebooting):
                    //The system is rebooting. 
                    //Very limited time to preform clean up and save any settings to disk.
                    break;
            }

        }
    }
}