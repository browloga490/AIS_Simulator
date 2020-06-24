using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using GalaSoft.MvvmLight;
using SimpleTCP;

namespace AIS_Simulator_TCP_Server_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            var viewModel = new ViewModel();

            viewModel.ShipList.Add(new Ship() { vesselName = "Ship_1", broadcastStatus = "OFF" });

            DataContext = viewModel;
            InitializeComponent();

            lbxShipList.ItemsSource = viewModel.ShipList;
        }

        SimpleTcpServer server;
        private void MainWindow_Load(object sender, EventArgs e)
        {
            server = new SimpleTcpServer();
            server.Delimiter = 0x13;
            server.StringEncoder = Encoding.UTF8;
            server.DataReceived += Server_DataRecieved;

        }

        private void Server_DataRecieved(object sender, SimpleTCP.Message e)
        {
            txtStatus.Dispatcher.Invoke(delegate ()
            {
                if (e.MessageString.Contains("Verifying Connection"))
                {
                    txtStatus.Text += e.MessageString;
                    e.ReplyLine(string.Format("SERVER: Connected successfully!", e.MessageString));
                    txtStatus.Text += "SERVER: Client connected successfully!\n";
                }
                txtStatus.Text += e.MessageString;
                e.ReplyLine(string.Format("You said: {0}\n", e.MessageString));
            });
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            if (!server.IsStarted)
            {
                txtStatus.Text += "Server starting...\n";
                txtStatus.ScrollToEnd();
                System.Net.IPAddress ip = System.Net.IPAddress.Parse(txtHost.Text);
                server.Start(ip, Convert.ToInt32(txtPort.Text));
                txtStatus.Text += "Server ON\n";
                txtStatus.ScrollToEnd();
            }
            
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            
            if (server.IsStarted)
            {
                txtStatus.Text += "Server stopping...\n";
                txtStatus.ScrollToEnd();
                server.Stop();
                txtStatus.Text += "Server OFF\n";
                txtStatus.ScrollToEnd();
            }
                
        }
    }

    public class ViewModel : ViewModelBase
    {
        private List<Ship> _shipList;
        public List<Ship> ShipList
        {
            get => _shipList;
            set => Set(ref _shipList, value);
        }

        public string _serverLog;
        public string ServerLog
        {
            get => _serverLog;
            set => Set(ref _serverLog, value);
        }

        public ViewModel()
        {
            this.ShipList = new List<Ship>();
            this.ServerLog = "";
        }
    }

    public class Ship
    {
    public string vesselName { get; set; }
    public string broadcastStatus { get; set; }

    }
}
