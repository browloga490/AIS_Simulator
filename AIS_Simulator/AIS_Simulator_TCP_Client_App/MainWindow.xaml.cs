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
using SimpleTCP;

namespace AIS_Simulator_TCP_Client_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
            client.Connect(txtHost.Text, Convert.ToInt32(txtPort.Text));
            client.WriteLineAndGetReply("CLIENT: Verifying Connection\n" TimeSpan.FromSeconds(3));
            txtRecievedData.Text += "Connected Successfully\n";
        }
    }
}
