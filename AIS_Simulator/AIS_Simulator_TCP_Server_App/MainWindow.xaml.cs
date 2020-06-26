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
using System.Net;
using System.Net.Sockets;
using System.Threading;
using AIS_Simulator_TCP_Server_App.Resource_Dictionaries;

namespace AIS_Simulator_TCP_Server_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        private ViewModel viewModel;
        private static TcpListener _listener;
        private static bool serverON;
        private static List<TcpClient> clientList;

        public MainWindow()
        {
            DataContext = viewModel;
            InitializeComponent();

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
