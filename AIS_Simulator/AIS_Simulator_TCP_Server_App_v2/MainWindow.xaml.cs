using AIS_Simulator_TCP_Server_App_v2.View;
using AIS_Simulator_TCP_Server_App_v2.ViewModel;
using System;
using System.Collections.Generic;
using System.Device.Location;
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

            /* Helpful URLs:
             * https://www.aggsoft.com/ais-decoder.htm
             * https://nmeachecksum.eqth.net/
             * https://www.onlinegdb.com/online_csharp_compiler
             */



            //String integralNum = num.Substring(0, num.IndexOf('.'));
            //String fractionalNum = num.Substring(num.IndexOf('.') + 1, num.Length - (num.IndexOf('.') + 1));
            //int intnum = (int) (float.Parse("120.2345") * 60000);
            //string finalNum = Convert.ToString(intnum, 2);

            //Console.WriteLine("TEST :: {0}",finalNum);
            //Console.WriteLine("LENGTH :: {0}", finalNum.Length);

            /*
            Convert.ToString((int) -78189720, 2);

            //Console.WriteLine("Binary Value: {0}", Convert.ToInt32(doubleVal));
            Console.WriteLine("Signed Binary Value: {0}", Convert.ToString((int)-78189720, 2));

            GeoCoordinate coordOne = new GeoCoordinate();
            GeoCoordinate coordTwo = new GeoCoordinate();

            coordOne.Latitude = 14.1424;
            coordOne.Longitude = 83.2421;

            double latDiff = coordTwo.Latitude - coordOne.Latitude;
            double longDiff = coordTwo.Longitude - coordOne.Longitude;
            
             */

            //Estimation: 1 degree == 111 km, 0.1 degree == 11.1 km
            //Speed: 1 knot == 1.852 km/h

            double S_Long = 87.3453; //Long: 87.3453
            double S_Lat = 23.4521; //Lat: 23.4521
            double speed = 100.1 * 1.852; //Speed: 100.1 knots == 185.3852 km/h
            double E_Long = 102.5343; //Destination: Long = 102.5343, Lat = 53.2345
            double E_Lat = 53.2345;
            int broadcastDelay = 8;//Broadcast Delay: 8 seconds

            double delayHours = broadcastDelay / 60 / 60; // delayHours = broadcastDelay/60/60;
            double distanceStep = speed * delayHours; // distanceStep = speed*delayHours
            double degreeStep = distanceStep * 0.1 / 11.1; // degreeStep = distanceStep*0.1/11.1

            double LongStep = degreeStep * (E_Long - S_Long); // LongStep = degreeStep*(E_Long - S_Long)
            double LatStep = degreeStep * (E_Lat - S_Lat); // LatStep = degreeStep*(E_Lat - S_Lat)

            double newLong;
            double newLat;

            /*
            while (S_Long < E_Long && S_Lat < E_Lat)
            {
                newLong = S_Long + LongStep; // newLong = S_Long + LongStep
                newLat = S_Lat + LatStep; // newLat = S_Lat + LatStep

                Console.WriteLine("{0},{1}", newLong, newLat);

                S_Long = newLong;
                S_Lat = newLat;
            }*/
            

            



            //coordTwo.Latitude = 15.1424;
            //coordTwo.Longitude = 83.2421;

            //coordOne.GetDistanceTo(coordTwo);

            //Console.WriteLine("DISTANCE: {0}", coordOne.GetDistanceTo(coordTwo));

            //coord.GetDistanceTo()

            double longitudeOne = 0.927611367234957; //45.55;
            double latitudeOne = 1.09815990651231; //59.667;
            double bearing = 0.785398;
            double distance = 543.0;
            const double R = 6371;

            double latitudeTwo = Math.Asin((Math.Sin(latitudeOne) * Math.Cos(distance / R)) +
                      (Math.Cos(latitudeOne) * Math.Sin(distance / R) * Math.Cos(bearing)));

            double longitudeTwo = longitudeOne + Math.Atan2(Math.Sin(bearing) * Math.Sin(distance / R) * Math.Cos(latitudeOne),
                           Math.Cos(distance / R) - (Math.Sin(latitudeOne) * Math.Sin(latitudeTwo)));

            Console.WriteLine("\nLatitude : From {0} to {1}", latitudeOne, latitudeTwo);
            Console.WriteLine("Longitude : From {0} to {1}\n", longitudeOne, longitudeTwo);
            


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
            svShipMovement.Visibility = Visibility.Hidden;
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
            svShipMovement.Visibility = Visibility.Hidden;
            svPosRep.Visibility = Visibility.Hidden;
            svStatVoyData.Visibility = Visibility.Visible;
        }

        private void PosRepMenuItem_Click(object sender, RoutedEventArgs e)
        {
            svShipMovement.Visibility = Visibility.Hidden;
            svStatVoyData.Visibility = Visibility.Hidden;
            svPosRep.Visibility = Visibility.Visible;
        }

        private void ShipMotionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            svPosRep.Visibility = Visibility.Hidden;
            svStatVoyData.Visibility = Visibility.Hidden;
            svShipMovement.Visibility = Visibility.Visible;
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
