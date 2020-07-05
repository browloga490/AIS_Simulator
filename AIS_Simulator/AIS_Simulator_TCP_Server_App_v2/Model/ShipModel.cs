using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Simulator_TCP_Server_App_v2.Model
{
    class ShipModel : INotifyPropertyChanged
    {
        private bool _isNewShip;
        public bool IsNewShip
        {
            get => _isNewShip;
            set
            {
                _isNewShip = value;
                OnPropertyChanged("IsNewShip");
            }
        }

        private string _broadcastStatus;
        public string BroadcastStatus
        {
            get => _broadcastStatus;
            set
            {
                _broadcastStatus = value;
                OnPropertyChanged("BroadcastStatus");
            }
        }

        private PositionReportClassA _posRepClassA;
        public PositionReportClassA PosRepClassA
        {
            get => _posRepClassA;
            set
            {
                //DANGER ZONE!!
                _posRepClassA = value;
                Console.WriteLine("MMSI VALUE CHANGED :: {0}", _posRepClassA.MMSI);
                _posRepClassA.generateSentence();
                OnPropertyChanged("PosRepClassA");
            }
        }

        //All of the general variables needed to construct a message of any type



        //All of the variables needed to construct the Position Report message

        private int _messageType;
        public int MessageType 
        { 
            get => _messageType;
            set
            {
                _messageType = value;
                OnPropertyChanged("MessageType");
            } 
        }

        private string _repeat;
        public string Repeat 
        { 
            get => _repeat;
            set
            {
                _repeat = value;
                OnPropertyChanged("Repeat");
            } 
        }

        private string _mmsi;
        public string MMSI
        {
            get => _mmsi;
            set
            {
                _mmsi = value;
                OnPropertyChanged("MMSI");
            }
        }

        private string _navStatus;
        public string NavStatus 
        { 
            get => _navStatus;
            set
            {
                _navStatus = value;
                OnPropertyChanged("NavStatus");
            } 
        }

        private string _turn;
        public string Turn 
        {
            get => _turn;
            set
            {
                _turn = value;
                OnPropertyChanged("Turn");
            }
        }

        private string _speed;
        public string Speed 
        { 
            get => _speed;
            set
            {
                _speed = value;
                OnPropertyChanged("Speed");
            } 
        }

        private string _accuracy;
        public string Accuracy 
        {
            get => _accuracy;
            set
            {
                _accuracy = value;
                OnPropertyChanged("Accuracy");
            } 
        }

        private string _longitude;
        public string Longitude 
        { 
            get => _longitude; 
            set
            {
                _longitude = value;
                OnPropertyChanged("Longitude");
            }
        }

        private string _latitude;
        public string Latitude 
        { 
            get => _latitude;
            set
            {
                _latitude = value;
                OnPropertyChanged("Latitude");
            } 
        }

        private string _course;
        public string Course 
        { 
            get => _course;
            set
            {
                _course = value;
                OnPropertyChanged("Course");
            } 
        }

        private string _heading;
        public string Heading 
        { 
            get => _heading;
            set
            {
                _heading = value;
                OnPropertyChanged("Heading");
            }
        }

        private string _timestamp;
        public string Timestamp 
        { 
            get => _timestamp;
            set
            {
                _timestamp = value;
                OnPropertyChanged("Timestamp");
            } 
        }

        private string _maneuver;
        public string Maneuver 
        { 
            get => _maneuver;
            set
            {
                _maneuver = value;
                OnPropertyChanged("Maneuver");
            } 
        }

        private string _spare;
        public string Spare 
        { 
            get => _spare;
            set
            {
                _spare = value;
                OnPropertyChanged("Spare");
            } 
        }

        private string _raim;
        public string RAIM 
        { 
            get => _raim;
            set
            {
                _raim = value;
                OnPropertyChanged("RAIM");
            } 
        }

        private string _radio;
        public string Radio 
        { 
            get => _radio; 
            set
            {
                _radio = value;
                OnPropertyChanged("Radio");
            }
        }

        private StaticAndVoyageRelatedData _statVoyData;
        public StaticAndVoyageRelatedData StatVoyData
        {
            get => _statVoyData;
            set
            {
                _statVoyData = value;
                OnPropertyChanged("StatVoyData");
            }
        }

        public ShipModel()
        {
            this.PosRepClassA = new PositionReportClassA();
            this.StatVoyData = new StaticAndVoyageRelatedData();
            this.StatVoyData.vesselName = "[ADD NEW SHIP]";
            this._broadcastStatus = "OFF";
            this.IsNewShip = true;

            this.MessageType = 0; //Value initially set to 0 (default)
            this.Repeat = "0";
            this.MMSI = "000000000"; //Value initially set to 0 (default)
            this.NavStatus = "15";
            this.Turn = "701.234";
            this.Speed = "111.1";
            this.Accuracy = "0";
            this.Longitude = "181"; //Value initially set to 181 (default)
            this.Latitude = "91"; //Value initially set to 91 (default)
            this.Course = "360";
            this.Heading = "511";
            this.Timestamp = "60"; //Value initially set to 60 (default)
            this.Maneuver = "0";
            this.Spare = "0";
            this.RAIM = "0";
            this.Radio = "0";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class PositionReportClassA
    {
        public string[] binaryArray { get; set; }

        public string sentence { get; set; }
        public int broadcastDelay;

        public string packetID;
        public string fragCount;
        public string fragNum;
        public string sequentialMessageID;
        public string radChanCode;
        public string payload;
        public string fillBitNum;
        public string checkSum;


        public PositionReportClassA()
        {
            //Initializing with default values

            this.binaryArray = new string[16]
            {   "000000",
                "00",
                "000000000000000000000000000000",
                "1111",
                "10000000",
                "1111111111",
                "0",
                "10110101",
                "1011011",
                "111000010000",
                "111111111",
                "111100",
                "00",
                "000",
                "0",
                "0000000000000000000"};

            this.sentence = "!AIVDM,1,1,,A,13HOI:0P0000VOHLCnHQKwvL05Ip,0*23";
            this.broadcastDelay = 4;

            //Choose these values each time you generate a sentence (they should be the parameters of the method)
            this.packetID = "!AIVDM";
            this.fragCount = "1";
            this.fragNum = "1";
            this.sequentialMessageID = "";
            this.radChanCode = "A";
            this.payload = "";
            this.fillBitNum = "0";
            this.checkSum = "";
    }

        public void generateSentence()
        {
            string binaryMessage;
            List<byte> asciiValList = new List<byte>();

            //Update the values of the binary array with the new converted input values
            this.binaryArray[0] = convertIntegerToBinary(this.messageType.ToString(), 6);
            this.binaryArray[1] = convertIntegerToBinary(this.repeat, 2);
            this.binaryArray[2] = convertIntegerToBinary(this.mmsi, 30);
            this.binaryArray[3] = convertIntegerToBinary(this.navStatus, 4);
            this.binaryArray[4] = convertIntegerToBinary((4.733*Math.Sqrt(double.Parse(this.turn))).ToString(), 8, 1, true);
            this.binaryArray[5] = convertIntegerToBinary(this.speed, 10);
            this.binaryArray[6] = convertIntegerToBinary(this.accuracy, 1);
            this.binaryArray[7] = convertIntegerToBinary(this.longitude, 28, 60000);
            this.binaryArray[8] = convertIntegerToBinary(this.latitude, 27, 60000);
            this.binaryArray[9] = convertIntegerToBinary(this.course, 12, 10);
            this.binaryArray[10] = convertIntegerToBinary(this.heading, 9);
            this.binaryArray[11] = convertIntegerToBinary(this.timestamp, 6);
            this.binaryArray[12] = convertIntegerToBinary(this.maneuver, 2);
            this.binaryArray[13] = convertIntegerToBinary(this.spare, 3);
            this.binaryArray[14] = convertIntegerToBinary(this.raim, 1);
            this.binaryArray[15] = convertIntegerToBinary(this.radio, 19);

            binaryMessage = string.Join("", this.binaryArray);

            string tempStr;
            int tempInt;

            //Console.WriteLine("Binary message: {0}", binaryMessage);
            //Console.WriteLine("Length of binary message: {0}", binaryMessage.Length);

            for (int i=0; i<168; i+=6)
            {
                tempStr = binaryMessage.Substring(i, 6);
                tempInt = Convert.ToInt32(tempStr, 2) + 48;

                // add 48 to decimal value
                // add 8 if result is greater than 87
                if (tempInt > 87)
                {
                    tempInt += 8;
                }

                //Console.WriteLine("Conversion :: {0}", tempInt);
                asciiValList.Add(Convert.ToByte(tempInt));
            }

            //Convert binary values to decimal values and perform operations on them

            //Console.WriteLine("ASCII FORM :: {0}", Encoding.ASCII.GetString(asciiValList.ToArray()));

            this.payload = Encoding.ASCII.GetString(asciiValList.ToArray());

            this.sentence = String.Format("{0},{1},{2},{3},{4},{5},{6}", 
                this.packetID, 
                this.fragCount, 
                this.fragNum, 
                this.sequentialMessageID, 
                this.radChanCode, 
                this.payload, 
                this.fillBitNum);

            //Convert the sentence to bytes and store in an array
            byte[] tempBytes = Encoding.ASCII.GetBytes(this.sentence.Substring(1));
            byte tempCheckSum = 0;

            //Apply XOR to each byte in the array (storing the result in tempCheckSum variable)
            for (int i=0; i<tempBytes.Length; i++)
                tempCheckSum ^= tempBytes[i];

            //Convert the tempCheckSum value to hexadecimal
            this.checkSum = Convert.ToString(tempCheckSum, 16);

            //Add the checksum to the end of the sentence
            this.sentence += "*" + this.checkSum;
        }

        public string convertIntegerToBinary(string num, int padding, int multiplier=1, bool signedInteger=false)
        {
            ///Given a number in the form of a string (num), this method will
            ///return the binary representation of the number. If the number
            ///contains a fractional component, then the method will split it
            ///at the decimal point and compute the binary representaions of
            ///the integral and fractional parts seperately. These two values
            ///will them be combined and returned.

            string signBit = "";
            string finalNum;

            if (signedInteger)
            {
                if (num.Contains('-'))
                {
                    signBit = "1";
                    num.Remove(0, 1);
                }
                else
                    signBit = "0";

                padding -= 1;
            }

            if (num.Contains('.'))
            {
                int temp = (int) (float.Parse(num) * multiplier);
                finalNum = Convert.ToString(temp, 2).PadLeft(padding, '0');
            }
            else
            {
                finalNum = Convert.ToString(int.Parse(num)*multiplier, 2).PadLeft(padding, '0');
            }

            return signBit + finalNum;
        }

        public string convertStringToBinary (string strVal)
        {
            string result = "";
            string tempStr;
            int padVal = 6;

            foreach (char c in strVal)
            {
                tempStr = Convert.ToString(c, 2);

                if (tempStr.Length > padVal)
                {
                    tempStr.Remove(0, tempStr.Length - padVal);
                }
                else if (tempStr.Length < padVal)
                {
                    tempStr.PadLeft(padVal);
                }

                result += tempStr;
            }

            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }

    public class StaticAndVoyageRelatedData
    {
        public const string messageType = "5";
        public string repeat { get; set; }
        public string mmsi { get; set; }
        public string aisVersion { get; set; }
        public string imo { get; set; }
        public string callSign { get; set; }
        public string vesselName { get; set; }
        public string shipType { get; set; }
        public string dimensionToBow { get; set; }
        public string dimensionToStern { get; set; }
        public string dimensionToPort { get; set; }
        public string dimensionToStarboard { get; set; }
        public string epfd { get; set; }
        public string month { get; set; }
        public string day { get; set; }
        public string hour { get; set; }
        public string minute { get; set; }
        public string draught { get; set; }
        public string destination { get; set; }
        public string dte { get; set; }
        public string spare { get; set; }

        public string[] binaryArray { get; set; }

        public StaticAndVoyageRelatedData()
        {
            //Initializing with default values

            this.repeat = "0";
            this.mmsi = "000000000"; //Can change
            this.aisVersion = "0"; //Can change
            this.imo = "000"; //Can change
            this.callSign = "1234567"; //Can change
            this.vesselName = "12345678901234567890"; //Can change
            this.shipType = "99";
            this.dimensionToBow = "0";
            this.dimensionToStern = "0";
            this.dimensionToPort = "0";
            this.dimensionToStarboard = "0";
            this.epfd = "0";
            this.month = "0";
            this.day = "0";
            this.hour = "0";
            this.minute = "0";
            this.draught = "0";
            this.destination = "12345678901234567890";
            this.dte = "1";
            this.spare = "N/A";

            this.binaryArray = new string[16]
            {   "000000",
                "00",
                "000000000000000000000000000000",
                "1111",
                "10000000",
                "1111111111",
                "0",
                "10110101",
                "1011011",
                "111000010000",
                "111111111",
                "111100",
                "00",
                "000",
                "0",
                "0000000000000000000"};
        }

    }
}
