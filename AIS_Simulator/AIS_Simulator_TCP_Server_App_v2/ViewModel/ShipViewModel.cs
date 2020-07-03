using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using AIS_Simulator_TCP_Server_App_v2.Model;
using Microsoft.SqlServer.Server;

namespace AIS_Simulator_TCP_Server_App_v2.ViewModel
{
    class ShipViewModel
    {
        private TCPServerModel _server;
        public TCPServerModel Server
        {
            get => _server;
            set => _server = value;
        }

        private ObservableCollection<ShipModel> _shipList;
        public ObservableCollection<ShipModel> ShipList
        {
            get => _shipList;
            set => _shipList = value;
        }

        private ShipModel _selectedShip;
        public ShipModel SelectedShip
        {
            get => _selectedShip;
            set => _selectedShip = value;
        }

        private ICommand mUpdater;
        public ICommand UpdateCommand
        {
            get
            {
                if (mUpdater == null)
                    mUpdater = new Updater();
                return mUpdater;
            }
            set => mUpdater = value;
        }

        private class Updater : ICommand
        {

            public bool CanExecute(object parameter) { return true; }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter) { }
        }

        public ShipViewModel()
        {
            ShipList = new ObservableCollection<ShipModel>() { new ShipModel() };
            Server = new TCPServerModel();
            SelectedShip = ShipList[0];
        }

        public void startTCPServer()
        {
            if (!Server.ServerOn)
            {
                Server.ClientList = new List<TcpClient>();

                Server.ServerStatus += "Server starting...\n";

                System.Net.IPAddress ip = System.Net.IPAddress.Parse(Server.ServerHost);
                Server.Listener = new TcpListener(ip, Convert.ToInt32(Server.ServerPort));
                Server.Listener.Start();
                Server.ServerOn = true;

                Server.ServerStatus += "Server ON\n";

                Task.Run(() =>
                {
                    while (Server.ServerOn)
                    {
                        try
                        {
                            TcpClient client = Server.Listener.AcceptTcpClient(); //Waits for a client conection request

                            NetworkStream stream = client.GetStream();

                            Server.ClientList.Add(client);
                        }
                        catch (SocketException socExp) { }
                    }
                }, Server.CancelTokenSource.Token);
            }
        }

        public void stopTCPServer()
        {
            if (Server.ServerOn)
            {
                Server.ServerStatus += "Server stopping...\n";

                Server.ServerOn = false;
                Server.CancelTokenSource.Cancel();
                Server.Listener.Stop();

                Server.ServerStatus += "Server OFF\n";
            }
        }

        public void saveConfiguration(string shipName)
        {
            SelectedShip.IsNewShip = false;

            if (!(ShipList[ShipList.Count-1].StatVoyData.vesselName.Equals("[ADD NEW SHIP]")))
            {
                ShipList.Add(new ShipModel());
            }
        }

        public void removeShip()
        {
            if (!SelectedShip.IsNewShip)
            {
                ShipList.Remove(SelectedShip);
            }
        }

        private static readonly KeyValuePair<int, string>[] messageTypePosRepList = {
            new KeyValuePair<int, string>(0, "Position Report Class A"),
            new KeyValuePair<int, string>(1, "Position Report Class A (Assigned schedule)"),
            new KeyValuePair<int, string>(2, "Position Report Class A (Response to interrogation)"),
        };

        public KeyValuePair<int, string>[] MessageTypePosRepList
        {
            get => messageTypePosRepList;
        }

        public void startBroadcast ()
        {
            //Ship's broadcasting value is set to true
            //This code runs on a loop

            ShipModel tempShip = ShipList[ShipList.IndexOf(SelectedShip)];

            Task.Run(() =>
            {
                if (tempShip.BroadcastStatus.Equals("OFF"))
                {
                    tempShip.BroadcastStatus = "ON";

                    byte[] message = new byte[1024];

                    foreach (char c in tempShip.PosRepClassA.sentence)
                    {
                        message.Append(Convert.ToByte(c));
                    }

                    Server.ServerStatus += String.Format("Ship {0} :: Broadcast ON\n", tempShip.StatVoyData.vesselName);

                    while (Server.ServerOn && tempShip.BroadcastStatus.Equals("ON"))
                    {
                        try
                        {
                            
                            Server.SendToClients(message);
                            Server.ServerStatus += String.Format("Ship {0} :: Sent the message {1} to the clients\n", tempShip.StatVoyData.vesselName, tempShip.PosRepClassA.sentence);
                            Thread.Sleep(tempShip.PosRepClassA.broadcastDelay*1000);

                            //Add new tasks here to implement the other broadcasted sentences
                        }
                        catch (SocketException socExp) { }
                    }
                }
            }, Server.CancelTokenSource.Token);
        }

        public void stopBroadcast()
        {
            if (SelectedShip.BroadcastStatus.Equals("ON"))
            {
                SelectedShip.BroadcastStatus = "OFF";
                Server.ServerStatus += String.Format("Ship {0} :: Broadcast OFF\n", SelectedShip.StatVoyData.vesselName);
            }
        }
    }
}
