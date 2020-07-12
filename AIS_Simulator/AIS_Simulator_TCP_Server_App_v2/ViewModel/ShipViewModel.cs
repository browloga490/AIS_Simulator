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
                            //Waits for a client conection request
                            TcpClient client = Server.Listener.AcceptTcpClient();
                            NetworkStream stream = client.GetStream();

                            //Adds client to the server's list of clients once a conncetion is established
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
            //Generates a new sentence based on the new/updated payload values
            //Creates a new template ship if a new ship was just been created

            if (!shipName.Equals("[ADD NEW SHIP]"))
            {
                SelectedShip.IsNewShip = false;

                Console.WriteLine("MMSI VALUE IS :: {0}", SelectedShip.MMSI);

                if (!(ShipList[ShipList.Count - 1].MTypeFive.VesselName.Equals("[ADD NEW SHIP]")))
                {
                    ShipList.Add(new ShipModel());
                }
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
            ShipModel tempShip = ShipList[ShipList.IndexOf(SelectedShip)];

            if (tempShip.BroadcastStatus.Equals("OFF") && !tempShip.IsNewShip && Server.ServerOn)
            {
                tempShip.BroadcastStatus = "ON";
                Server.ServerStatus += String.Format("\nShip {0} :: Broadcast ON\n", tempShip.MTypeFive.VesselName);

                byte[] posRepMessage;
                byte[] statVoyMessage;

                //Add new tasks here to implement the other broadcasted sentences

                //Send the Position Report Class A message (Types 1, 2, and 3) to the clients
                Task.Run(() =>
                {
                    while (Server.ServerOn && tempShip.BroadcastStatus.Equals("ON"))
                    {
                        try
                        {
                            //Convert the ship's Position Report Class A message sentence to bytes 
                            //and send them to the connected clients
                            posRepMessage = Encoding.UTF8.GetBytes(tempShip.MTypeOne.Sentence);
                            Server.SendToClients(posRepMessage);
                            Server.ServerStatus += String.Format("\nShip {0} :: Sent the message {1} to the clients\n", tempShip.MTypeFive.VesselName, tempShip.MTypeOne.Sentence);

                            //Update the longitude and latitude values of the ship to simulate its motion
                            moveShip(tempShip);

                            //Make the thread wait for the desired broadcast delay time before sending the next broadcast
                            Thread.Sleep(tempShip.MTypeOne.BroadcastDelay * 1000);
                        }
                        catch (SocketException socExp) { }
                    }
                }, Server.CancelTokenSource.Token);

                //Send the Static and Voyage Related Data messages (Type 5) to the clients
                Task.Run(() =>
                {
                    while (Server.ServerOn && tempShip.BroadcastStatus.Equals("ON"))
                    {
                        try
                        {
                            //Convert the ship's Static and Voyage Related Data message sentences to bytes 
                            //and send them to the connected clients
                            statVoyMessage = Encoding.UTF8.GetBytes(tempShip.MTypeFive.SentenceOne);
                            Server.SendToClients(statVoyMessage);
                            Server.ServerStatus += String.Format("\nShip {0} :: Sent the message {1} to the clients\n", tempShip.MTypeFive.VesselName, tempShip.MTypeFive.SentenceOne);

                            statVoyMessage = Encoding.UTF8.GetBytes(tempShip.MTypeFive.SentenceTwo);
                            Server.SendToClients(statVoyMessage);
                            Server.ServerStatus += String.Format("\nShip {0} :: Sent the message {1} to the clients\n", tempShip.MTypeFive.VesselName, tempShip.MTypeFive.SentenceTwo);

                            //Make the thread wait for the desired broadcast delay time before sending the next broadcast
                            Thread.Sleep(tempShip.MTypeOne.BroadcastDelay * 1000);
                        }
                        catch (SocketException socExp) { }
                    }
                }, Server.CancelTokenSource.Token);

            }
            
        }

        public void stopBroadcast()
        {
            //Turn off the currently selected ship's broadcast
            if (SelectedShip.BroadcastStatus.Equals("ON"))
            {
                SelectedShip.BroadcastStatus = "OFF";
                Server.ServerStatus += String.Format("\nShip {0} :: Broadcast OFF\n", SelectedShip.MTypeFive.VesselName);
            }
        }

        public void moveShip(ShipModel ship)
        {
            //Based on the set langitude, latitude, heading, speed, and broadcast delay of a ship, simulate its motion

            //Retrieve the longitude, latitude, and heading values of the ship and convert them to radians
            double longitudeOne = double.Parse(ship.MTypeOne.Longitude) * Math.PI / 180;
            double latitudeOne = double.Parse(ship.MTypeOne.Latitude) * Math.PI / 180;
            int heading = (int) (int.Parse(ship.MTypeOne.Heading) * Math.PI / 180);

            //Multiply the speed by 1.852 to convert it from knots to km/h
            double speed = double.Parse(ship.MTypeOne.Speed)*1.852; 

            int broadcastDelay = ship.MTypeOne.BroadcastDelay;

            //Calculate the distance that the ship should travel between each 
            //broadcast given the speed (km/h) and broadcast delay (seconds)
            double distance = speed * broadcastDelay / 60 / 60;
            
            //R is a constant representing the radius of the Earth
            const double R = 6371;

            //Calculate the new latitude and longitude values that the ship will have once its travel is complete
            double latitudeTwo = Math.Asin((Math.Sin(latitudeOne) * Math.Cos(distance / R)) +
                      (Math.Cos(latitudeOne) * Math.Sin(distance / R) * Math.Cos(heading)));

            double longitudeTwo = longitudeOne + Math.Atan2(Math.Sin(heading) * Math.Sin(distance / R) * Math.Cos(latitudeOne),
                           Math.Cos(distance / R) - (Math.Sin(latitudeOne) * Math.Sin(latitudeTwo)));

            //Update the ship's longitude and latitude values with the previously calculated values
            ship.MTypeOne.Latitude = Convert.ToString(latitudeTwo * 180 / Math.PI);
            ship.MTypeOne.Longitude = Convert.ToString(longitudeTwo * 180 / Math.PI);
        }
    }
}
