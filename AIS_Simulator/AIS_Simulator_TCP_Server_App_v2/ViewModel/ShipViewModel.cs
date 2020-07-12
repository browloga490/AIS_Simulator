﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        private ComboBox _movementTypeComboBox;
        public ComboBox MovementTypeComboBox
        {
            get => _movementTypeComboBox;
            set => _movementTypeComboBox = value;
        }

        private static readonly KeyValuePair<int, string>[] messageTypePosRepList = {
            new KeyValuePair<int, string>(1, "Position Report Class A"),
            new KeyValuePair<int, string>(2, "Position Report Class A (Assigned schedule)"),
            new KeyValuePair<int, string>(3, "Position Report Class A (Response to interrogation)"),
        };

        public KeyValuePair<int, string>[] MessageTypePosRepList
        {
            get => messageTypePosRepList;
        }

        private static readonly KeyValuePair<int, string>[] movementTypeList = {
            new KeyValuePair<int, string>(0, "Linear Movement"),
            new KeyValuePair<int, string>(1, "Circular Movement"),
        };

        public KeyValuePair<int, string>[] MovementTypeList
        {
            get => movementTypeList;
        }

        private ComboBoxItem test;

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

                //Retrieve the input values for the host's ip address and port from the UI and start the TCP listener
                System.Net.IPAddress ip = System.Net.IPAddress.Parse(Server.ServerHost);
                Server.Listener = new TcpListener(ip, Convert.ToInt32(Server.ServerPort));
                Server.Listener.Start();
                Server.ServerOn = true;

                Server.ServerStatus += "Server ON\n";

                //Create a new thread to wait for connection requests while the rest of the app runs
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

                if (!(ShipList[ShipList.Count - 1].MTypeFive.VesselName.Equals("[ADD NEW SHIP]")))
                {
                    ShipList.Add(new ShipModel());
                }
            } 
        }

        public void removeShip()
        {
            //Removes the ship that is currently selected in the listbox if it is not the placeholder ship
            if (!SelectedShip.IsNewShip)
            {
                ShipList.Remove(SelectedShip);
            }
        }

        public void startBroadcast ()
        {
            ShipModel tempShip = ShipList[ShipList.IndexOf(SelectedShip)];
            ComboBox tempCBX = new ComboBox();
            tempCBX.SelectedItem = MovementTypeComboBox.SelectedItem;

            //Ensure that the selected ship is not already broadcasting and that the server is currently on
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
                            moveShip(tempShip, tempCBX);

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

        public void moveShip(ShipModel ship, ComboBox cbx)
        {
            if (ship.IsNewShip) //Change this so that it will determine if the path will be linear or circular
            {
                Console.WriteLine("Selected Item: {0}", cbx.SelectedIndex);

                //Based on the set langitude, latitude, heading, speed, and broadcast delay of a ship, simulate its motion

                //Retrieve the longitude, latitude, and heading values of the ship and convert them to radians
                double longitudeOne = double.Parse(ship.MTypeOne.Longitude) * Math.PI / 180;
                double latitudeOne = double.Parse(ship.MTypeOne.Latitude) * Math.PI / 180;
                int heading = (int)(int.Parse(ship.MTypeOne.Heading) * Math.PI / 180);

                //Multiply the speed by 1.852 to convert it from knots to km/h
                double speed = double.Parse(ship.MTypeOne.Speed) * 1.852;

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

                Console.WriteLine("New Latitude : {0}", (latitudeTwo * 180 / Math.PI));
                Console.WriteLine("New Longitude : {0}", (longitudeTwo * 180 / Math.PI));

                //Update the ship's longitude and latitude values with the previously calculated values
                ship.MTypeOne.Latitude = Convert.ToString(latitudeTwo * 180 / Math.PI);
                ship.MTypeOne.Longitude = Convert.ToString(longitudeTwo * 180 / Math.PI);
            }
            else
            {
                double centreLong = double.Parse(ship.MTypeOne.CentrePointLongitude) * Math.PI / 180;
                double centreLat = double.Parse(ship.MTypeOne.CentrePointLatitude) * Math.PI / 180;

                if (double.Parse(ship.MTypeOne.Heading) > 359)
                {
                    //ship.MTypeOne.Heading = "0";
                }

                double heading = (double.Parse(ship.MTypeOne.Heading) * Math.PI / 180);

                //R is a constant representing the radius of the Earth
                const double R = 6371;

                double distance = double.Parse(ship.MTypeOne.Radius);

                double latitudeTwo = Math.Asin((Math.Sin(centreLat) * Math.Cos(distance / R)) +
                              (Math.Cos(centreLat) * Math.Sin(distance / R) * Math.Cos(heading)));

                double longitudeTwo = centreLong + Math.Atan2(Math.Sin(heading) * Math.Sin(distance / R) * Math.Cos(centreLat),
                               Math.Cos(distance / R) - (Math.Sin(centreLat) * Math.Sin(latitudeTwo)));

                ship.MTypeOne.Latitude = Convert.ToString(latitudeTwo * 180 / Math.PI);
                ship.MTypeOne.Longitude = Convert.ToString(longitudeTwo * 180 / Math.PI);

                for (int i=0; i < 500; i++)
                {
                    //Based on the set longitude, latitude, heading, speed, and broadcast delay of a ship, simulate its motion

                    //Retrieve the longitude, latitude, and heading values of the ship and convert them to radians
                    double longitudeOne = double.Parse(ship.MTypeOne.Longitude) * Math.PI / 180;
                    double latitudeOne = double.Parse(ship.MTypeOne.Latitude) * Math.PI / 180;
                    heading = (double.Parse(ship.MTypeOne.Heading) * Math.PI / 180);

                    //Multiply the speed by 1.852 to convert it from knots to km/h
                    double speed = double.Parse(ship.MTypeOne.Speed) * 1.852;

                    int broadcastDelay = ship.MTypeOne.BroadcastDelay;

                    //Calculate the distance that the ship should travel between each 
                    //broadcast given the speed (km/h) and broadcast delay (seconds)
                    distance = speed * broadcastDelay / 60 / 60;

                    //Calculate the new latitude and longitude values that the ship will have once its travel is complete
                    latitudeTwo = Math.Asin((Math.Sin(latitudeOne) * Math.Cos(distance / R)) +
                              (Math.Cos(latitudeOne) * Math.Sin(distance / R) * Math.Cos(heading)));

                    longitudeTwo = longitudeOne + Math.Atan2(Math.Sin(heading) * Math.Sin(distance / R) * Math.Cos(latitudeOne),
                                   Math.Cos(distance / R) - (Math.Sin(latitudeOne) * Math.Sin(latitudeTwo)));

                    Console.WriteLine("{0}, {1}", (latitudeTwo * 180 / Math.PI), (longitudeTwo * 180 / Math.PI));

                    //Update the ship's longitude and latitude values with the previously calculated values
                    ship.MTypeOne.Latitude = Convert.ToString(latitudeTwo * 180 / Math.PI);
                    ship.MTypeOne.Longitude = Convert.ToString(longitudeTwo * 180 / Math.PI);
                    ship.MTypeOne.Heading = Convert.ToString(double.Parse(ship.MTypeOne.Heading) + double.Parse(ship.MTypeOne.Turn) * broadcastDelay / 60 / 60);

                }
            }
        }
    }
}
