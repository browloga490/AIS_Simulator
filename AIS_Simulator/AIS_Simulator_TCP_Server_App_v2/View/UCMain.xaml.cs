using AIS_Simulator_TCP_Server_App_v2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AIS_Simulator_TCP_Server_App_v2.View
{
    /// <summary>
    /// Interaction logic for UCMain.xaml
    /// </summary>
    public partial class UCMain : UserControl
    {
        private static TcpListener _listener;
        private static bool serverON;
        private static List<TcpClient> clientList;

        CancellationTokenSource cancelTokenSource = new CancellationTokenSource();

        public UCMain()
        {
            InitializeComponent();

            serverON = false;
        }

        private void BtnConfigueShip_Click(object sender, RoutedEventArgs e)
        {
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (!serverON)
            {
                clientList = new List<TcpClient>();

                txtStatus.Text += "Server starting...\n";
                txtStatus.ScrollToEnd();

                System.Net.IPAddress ip = System.Net.IPAddress.Parse(txtHost.Text);
                _listener = new TcpListener(ip, Convert.ToInt32(txtPort.Text));
                _listener.Start();
                serverON = true;

                txtStatus.Text += "Server ON\n";
                txtStatus.ScrollToEnd();

                Task.Run(() => 
                {
                    while (serverON)
                    {
                        try
                        {
                            TcpClient client = _listener.AcceptTcpClient(); //Waits for a client conection request

                            NetworkStream stream = client.GetStream();

                            clientList.Add(client);
                        }
                        catch (SocketException socExp){}
                    }
                }, cancelTokenSource.Token);
                
            }
            
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            if (serverON)
            {
                txtStatus.Text += "Server stopping...\n";
                txtStatus.ScrollToEnd();

                serverON = false;
                cancelTokenSource.Cancel();
                _listener.Stop();

                txtStatus.Text += "Server OFF\n";
                txtStatus.ScrollToEnd();
            }
        }

        private static byte[] ReadToEnd(NetworkStream stream)
        {
            List<byte> recievedBytes = new List<byte>();

            while (stream.DataAvailable)
            {
                byte[] buffer = new byte[1024];

                stream.Read(buffer, 0, buffer.Length);

                recievedBytes.AddRange(buffer);
            }

            recievedBytes.RemoveAll(b => b == 0);

            return recievedBytes.ToArray();
        }

        private static void SendToClient(NetworkStream stream, byte[] data)
        {
            stream.Write(data, 0, data.Length);
        }
    }
}
