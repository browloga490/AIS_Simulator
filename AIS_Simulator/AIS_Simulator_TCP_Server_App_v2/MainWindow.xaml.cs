using AIS_Simulator_TCP_Server_App_v2.View;
using AIS_Simulator_TCP_Server_App_v2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace AIS_Simulator_TCP_Server_App_v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ShipViewModel VM = new ShipViewModel();

        private static TcpListener _listener;
        private static bool serverON;
        private static List<TcpClient> clientList;

        private CancellationTokenSource cancelTokenSource = new CancellationTokenSource(); //Try removing private if this doesn't work

        public MainWindow()
        {
            DataContext = VM;
            InitializeComponent();

            serverON = false;
            MainView.Visibility = Visibility.Visible;
            ConfigureView.Visibility = Visibility.Hidden;

            string num = "45323.23451112";

            
            String integralNum = num.Substring(0, num.IndexOf('.'));
            String fractionalNum = num.Substring(num.IndexOf('.') + 1, num.Length - (num.IndexOf('.') + 1));
            float flt = float.Parse("0");

            //Console.WriteLine(flt);
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            /*
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
                        catch (SocketException socExp) { }
                    }
                }, cancelTokenSource.Token);

            }
            */

            VM.startTCPServer();
            txtStatus.ScrollToEnd();
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            /*
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
            */

            VM.stopTCPServer();
            txtStatus.ScrollToEnd();
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

        private void BtnConfigueShip_Click(object sender, RoutedEventArgs e)
        {
            MainView.Visibility = Visibility.Hidden;
            ConfigureView.Visibility = Visibility.Visible;
            svStatVoyData.Visibility = Visibility.Hidden;
            svPosRep.Visibility = Visibility.Visible;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            ConfigureView.Visibility = Visibility.Hidden;
            MainView.Visibility = Visibility.Visible;
            VM.saveConfiguration(txtVesselName.Text);
        }

        private void StatVoyDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            svPosRep.Visibility = Visibility.Hidden;
            svStatVoyData.Visibility = Visibility.Visible;
        }

        private void PosRepMenuItem_Click(object sender, RoutedEventArgs e)
        {
            svStatVoyData.Visibility = Visibility.Hidden;
            svPosRep.Visibility = Visibility.Visible;
        }

        private void btnStartBrodcasting_Click(object sender, RoutedEventArgs e)
        {
            VM.startBroadcast();
        }

        private void btnStopBrodasting_Click(object sender, RoutedEventArgs e)
        {
            VM.stopBroadcast();
        }

        private void btnRemoveShip_Click(object sender, RoutedEventArgs e)
        {
            VM.removeShip();
        }
    }
}
