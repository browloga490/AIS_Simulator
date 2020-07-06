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
using SimpleTCP;

namespace AIS_Simulator_TCP_Client_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string dataBox;

        public MainWindow()
        {
            InitializeComponent();

            dataBox = "";
        }

        SimpleTcpClient client;

        private void MainWindow_Load(object sender, EventArgs e)
        {
            client = new SimpleTcpClient();
            client.StringEncoder = Encoding.UTF8;
            client.DataReceived += Client_DataRecieved;
        }

        private void Client_DataRecieved(object sender, SimpleTCP.Message e)
        {
            txtRecievedData.Dispatcher.Invoke(delegate ()
            {
                txtRecievedData.Text += e.MessageString;
            });
        }

        private void BtnConnect_Click(object sender, EventArgs e)
        {
            //client.Connect(txtHost.Text, Convert.ToInt32(txtPort.Text));
            //client.WriteLineAndGetReply("CLIENT: Verifying Connection\n", TimeSpan.FromSeconds(3));
            //txtRecievedData.Text += "Connected Successfully\n";

            try
            {
                TcpClient client = new TcpClient(txtHost.Text, Convert.ToInt32(txtPort.Text));

                Byte[] data = new Byte[1024];

                String responseData = String.Empty;

                Task.Run(() =>
                {
                    Console.WriteLine("Thread ID : {0}", Thread.CurrentThread.GetHashCode());
                    while (client.Connected)
                    {
                        List<byte> recievedBytes = new List<byte>();

                        if (client.GetStream().DataAvailable)
                        {
                            while (client.GetStream().DataAvailable)
                            {
                                byte[] buffer = new byte[1024];

                                client.GetStream().Read(buffer, 0, buffer.Length);

                                recievedBytes.AddRange(buffer);
                            }

                            recievedBytes.RemoveAll(b => b == 0);
                            data = recievedBytes.ToArray();

                            string message = Encoding.UTF8.GetString(data);

                            //txtRecievedData.Text += String.Format("Recieved : {0}\n", message);

                            Console.WriteLine(String.Format("Recieved : {0}\n", message));
                        }
                        Thread.Sleep(100);
                        
                    }
                });
            }
            catch (SocketException se) { }
            
        }
    }
}
