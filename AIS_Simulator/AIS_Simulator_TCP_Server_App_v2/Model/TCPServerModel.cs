using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AIS_Simulator_TCP_Server_App_v2.Model
{
    class TCPServerModel : INotifyPropertyChanged
    {
        private string _serverStatus;
        public string ServerStatus
        {
            get => _serverStatus;
            set
            {
                _serverStatus = value;
                OnPropertyChanged("ServerStatus");
            }
        }

        private string _serverHost;
        public string ServerHost
        {
            get => _serverHost;
            set
            {
                _serverHost = value;
                OnPropertyChanged("ServerHost");
            }
        }

        private string _serverPort;
        public string ServerPort
        {
            get => _serverPort;
            set
            {
                _serverPort = value;
                OnPropertyChanged("ServerPort");
            }
        }

        private TcpListener _listener;
        public TcpListener Listener
        {
            get => _listener;
            set
            {
                _listener = value;
                OnPropertyChanged("Listener");
            }
        }

        private bool _serverOn;
        public bool ServerOn
        {
            get => _serverOn;
            set
            {
                _serverOn = value;
                OnPropertyChanged("ServerOn");
            }
        }

        private List<TcpClient> _clientList;
        public List<TcpClient> ClientList
        {
            get => _clientList;
            set
            {
                _clientList = value;
                OnPropertyChanged("ClientList");
            }
        }

        private CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();
        public CancellationTokenSource CancelTokenSource
        {
            get => _cancelTokenSource;
            set
            {
                _cancelTokenSource = value;
                OnPropertyChanged("CancelTokenSource");
            }
        }

        public TCPServerModel()
        {
            ServerOn = false;
            ClientList = new List<TcpClient>();
            CancelTokenSource = new CancellationTokenSource();
            ServerStatus = "";
            ServerHost = "127.0.0.1";
            ServerPort = "8910";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public byte[] ReadToEnd(NetworkStream stream)
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

        public void SendToClients(byte[] data)
        {
            foreach (TcpClient client in ClientList)
            {
                client.GetStream().Write(data, 0, data.Length);
            }
        }
    }
}
