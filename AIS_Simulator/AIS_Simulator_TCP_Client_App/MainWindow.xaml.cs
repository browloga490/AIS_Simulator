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

            Console.WriteLine("Decoding Type 1 Message: \n");
            ConvertTypeOnePayloadToString("31mg=5OO@54aSU`GMM5f43900000");

            Console.WriteLine("\nDecoding Type 5 Message: \n");
            ConvertTypeFivePayloadToString("51mg=5@MFeK<48<@DHM@E=@000000000000000000000000Ht01RCPC11Dm2", "CPE2CkP0002", 4);

            //ConvertTypeOnePayloadToString
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

        public void ConvertTypeOnePayloadToString(string payload)
        {
            
            string tempStr;
            int tempInt;
            string binaryString = "";
            string[] binaryArray = new string[16];

            //Recover the 6-bits from the payload
            foreach (char c in payload)
            {
                tempInt = Convert.ToInt32(c) - 48;

                if (tempInt > 40)
                    tempInt -= 8;

                tempStr = Convert.ToString(tempInt, 2).PadLeft(6, '0');

                if (tempStr.Length > 6)
                    tempStr = tempStr.Remove(0, tempStr.Length - 6);

                binaryString += tempStr;
            }

            binaryArray[0] = Convert.ToString(Convert.ToInt32(binaryString.Substring(0, 6), 2)); //message type
            binaryArray[1] = Convert.ToString(Convert.ToInt32(binaryString.Substring(6, 2), 2)); //repeat
            binaryArray[2] = Convert.ToString(Convert.ToInt32(binaryString.Substring(8, 30), 2)); //mmsi
            binaryArray[3] = Convert.ToString(Convert.ToInt32(binaryString.Substring(38, 4), 2)); //navigation status
            binaryArray[4] = Convert.ToString(Math.Pow((Convert.ToInt32(binaryString.Substring(42, 8), 2)/4.733), 2)); //turn
            binaryArray[5] = Convert.ToString(Convert.ToInt32(binaryString.Substring(50, 10), 2)); //speed
            binaryArray[6] = Convert.ToString(Convert.ToInt32(binaryString.Substring(60, 1), 2)); //accuracy
            binaryArray[7] = Convert.ToString(Convert.ToInt32(binaryString.Substring(61, 28), 2)/600000); //longitude
            binaryArray[8] = Convert.ToString(Convert.ToInt32(binaryString.Substring(89, 27), 2)/600000); //latitude
            binaryArray[9] = Convert.ToString(Convert.ToInt32(binaryString.Substring(116, 12), 2)/10); //course
            binaryArray[10] = Convert.ToString(Convert.ToInt32(binaryString.Substring(128, 9), 2)); //heading
            binaryArray[11] = Convert.ToString(Convert.ToInt32(binaryString.Substring(137, 6), 2)); //timestamp
            binaryArray[12] = Convert.ToString(Convert.ToInt32(binaryString.Substring(143, 2), 2)); //maneuver
            binaryArray[13] = Convert.ToString(Convert.ToInt32(binaryString.Substring(145, 3), 2)); //spare
            binaryArray[14] = Convert.ToString(Convert.ToInt32(binaryString.Substring(148, 1), 2)); //raim
            binaryArray[15] = Convert.ToString(Convert.ToInt32(binaryString.Substring(149, 19), 2)); //radio

            Console.WriteLine("Message Type: {0}", binaryArray[0]);
            Console.WriteLine("Repeat: {0}", binaryArray[1]);
            Console.WriteLine("MMSI: {0}", binaryArray[2]);
            Console.WriteLine("Navigation Status: {0}", binaryArray[3]);
            Console.WriteLine("Rate of Turn: {0}", binaryArray[4]);
            Console.WriteLine("Speed: {0}", binaryArray[5]);
            Console.WriteLine("Accuracy: {0}", binaryArray[6]);
            Console.WriteLine("Longitude: {0}", binaryArray[7]);
            Console.WriteLine("Latitude: {0}", binaryArray[8]);
            Console.WriteLine("Course: {0}", binaryArray[9]);
            Console.WriteLine("Heading: {0}", binaryArray[10]);
            Console.WriteLine("Timestamp: {0}", binaryArray[11]);
            Console.WriteLine("Maneuver: {0}", binaryArray[12]);
            Console.WriteLine("Spare: {0}", binaryArray[13]);
            Console.WriteLine("RAIM: {0}", binaryArray[14]);
            Console.WriteLine("Radio: {0}", binaryArray[15]);
        }

        public void ConvertTypeFivePayloadToString(string payloadOne, string payloadTwo, int fillBitNum)
        {

            string tempStr;
            int tempInt;
            string binaryString = "";
            string[] binaryArray = new string[21];

            string payload = payloadOne + payloadTwo;

            foreach (char c in payload)
            {
                tempInt = Convert.ToInt32(c) - 48;

                if (tempInt > 40)
                    tempInt -= 8;

                tempStr = Convert.ToString(tempInt, 2).PadLeft(6, '0');

                if (tempStr.Length > 6)
                    tempStr = tempStr.Remove(0, tempStr.Length - 6);

                binaryString += tempStr;
            }

            //Remove fill bits
            if (fillBitNum > 0)
            {
                tempStr = binaryString.Substring(binaryString.Length - 6);
                binaryString = binaryString.Remove(binaryString.Length - 6) + tempStr.Substring(fillBitNum-2);
            }

            binaryArray[0] = Convert.ToString(Convert.ToInt32(binaryString.Substring(0, 6), 2)); //message type
            binaryArray[1] = Convert.ToString(Convert.ToInt32(binaryString.Substring(6, 2), 2)); //repeat
            binaryArray[2] = Convert.ToString(Convert.ToInt32(binaryString.Substring(8, 30), 2)); //mmsi
            binaryArray[3] = Convert.ToString(Convert.ToInt32(binaryString.Substring(38, 2), 2)); //ais version
            binaryArray[4] = Convert.ToString(Convert.ToInt32(binaryString.Substring(40, 30), 2)); //imo
            binaryArray[5] = ConvertBinaryToString(binaryString.Substring(70, 42)); //call sign
            binaryArray[6] = ConvertBinaryToString(binaryString.Substring(112, 120)); //vessel name
            binaryArray[7] = Convert.ToString(Convert.ToInt32(binaryString.Substring(232, 8), 2)); //ship type
            binaryArray[8] = Convert.ToString(Convert.ToInt32(binaryString.Substring(240, 9), 2)); //DimensionToBow
            binaryArray[9] = Convert.ToString(Convert.ToInt32(binaryString.Substring(249, 9), 2)); //DimensionToStern
            binaryArray[10] = Convert.ToString(Convert.ToInt32(binaryString.Substring(258, 6), 2)); //DimensionToPort
            binaryArray[11] = Convert.ToString(Convert.ToInt32(binaryString.Substring(264, 6), 2)); //DimensionToStarboard
            binaryArray[12] = Convert.ToString(Convert.ToInt32(binaryString.Substring(270, 4), 2)); //EPFD
            binaryArray[13] = Convert.ToString(Convert.ToInt32(binaryString.Substring(274, 4), 2)); //Month
            binaryArray[14] = Convert.ToString(Convert.ToInt32(binaryString.Substring(278, 5), 2)); //Day
            binaryArray[15] = Convert.ToString(Convert.ToInt32(binaryString.Substring(283, 5), 2)); //hour
            binaryArray[16] = Convert.ToString(Convert.ToInt32(binaryString.Substring(288, 6), 2)); //minute
            binaryArray[17] = Convert.ToString(Convert.ToInt32(binaryString.Substring(294, 8), 2)); //Draught
            binaryArray[18] = ConvertBinaryToString(binaryString.Substring(302, 120)); //Destination
            binaryArray[19] = Convert.ToString(Convert.ToInt32(binaryString.Substring(422, 1), 2)); //dte
            binaryArray[20] = Convert.ToString(Convert.ToInt32(binaryString.Substring(423, 1), 2)); //spare

            Console.WriteLine("Message Type: {0}", binaryArray[0]);
            Console.WriteLine("Repeat: {0}", binaryArray[1]);
            Console.WriteLine("MMSI: {0}", binaryArray[2]);
            Console.WriteLine("AIS Version: {0}", binaryArray[3]);
            Console.WriteLine("IMO: {0}", binaryArray[4]);
            Console.WriteLine("Call Sign: {0}", binaryArray[5]);
            Console.WriteLine("Vessel Name: {0}", binaryArray[6]);
            Console.WriteLine("Ship Type: {0}", binaryArray[7]);
            Console.WriteLine("Dimensions To Bow: {0}", binaryArray[8]);
            Console.WriteLine("Dimensions To Stern: {0}", binaryArray[9]);
            Console.WriteLine("Dimensions To Port: {0}", binaryArray[10]);
            Console.WriteLine("Dimensions To Starboard: {0}", binaryArray[11]);
            Console.WriteLine("EPFD: {0}", binaryArray[12]);
            Console.WriteLine("Month: {0}", binaryArray[13]);
            Console.WriteLine("Day: {0}", binaryArray[14]);
            Console.WriteLine("Hour: {0}", binaryArray[15]);
            Console.WriteLine("Minute: {0}", binaryArray[16]);
            Console.WriteLine("Draught: {0}", binaryArray[17]);
            Console.WriteLine("Destination: {0}", binaryArray[18]);
            Console.WriteLine("DTE: {0}", binaryArray[19]);
            Console.WriteLine("Spare: {0}", binaryArray[20]);
        }

        public string ConvertBinaryToString(string strVal)
        {
            string tempStr;
            string finalStr = "";

            for (int i=0; i<strVal.Length; i += 6)
            {
                tempStr = strVal.Substring(i, 6);

                finalStr += Convert.ToChar(Convert.ToInt32(tempStr, 2) + 64);
            }

            return finalStr;
        }

    }
}
