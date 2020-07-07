using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace AIS_Simulator_TCP_Server_App_v2.Model
{
    class ShipModel : INotifyPropertyChanged
    {
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

        public ShipModel()
        {
            this.PosRepClassA = new PositionReportClassA();
            this.StatVoyData = new StaticAndVoyageRelatedData();
            //this.StatVoyData.VesselName = "[ADD NEW SHIP]";
            this._broadcastStatus = "OFF";
            this.IsNewShip = true;
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

    public class PositionReportClassA : INotifyPropertyChanged
    {
        private bool initialized;

        private string _sentence;
        public string Sentence
        {
            get => _sentence;
            set
            {
                _sentence = value;
                OnPropertyChanged("Sentence");
            }
        }

        private string _broadcastDelay;
        public int BroadcastDelay
        {
            get => int.Parse(_broadcastDelay);
            set
            {
                _broadcastDelay = Convert.ToString(value);
                OnPropertyChanged("BroadcastDelay");
            }
        }

        //Payload values

        private int _messageType;
        public int MessageType 
        { 
            get => _messageType;
            set
            {
                _messageType = value;
                OnPropertyChanged("MessageType");
                if (initialized)
                {
                    generateSentence();
                }
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
                if (initialized)
                {
                    generateSentence();
                }
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
                if (initialized)
                {
                    generateSentence();
                }
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
                if (initialized)
                {
                    generateSentence();
                }
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
                if (initialized)
                {
                    generateSentence();
                }
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
                if (initialized)
                {
                    generateSentence();
                }
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
                if (initialized)
                {
                    generateSentence();
                }
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
                if (initialized)
                {
                    generateSentence();
                }
            } 
        }

        private string _latitude;
        public string Latitude 
        { 
            get => _latitude;
            set
            {
                _latitude = value;
                OnPropertyChanged("IsNewShip");
                if (initialized)
                {
                    generateSentence();
                }
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
                if (initialized)
                {
                    generateSentence();
                }
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
                if (initialized)
                {
                    generateSentence();
                }
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
                if (initialized)
                {
                    generateSentence();
                }
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
                if (initialized)
                {
                    generateSentence();
                }
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
                if (initialized)
                {
                    generateSentence();
                }
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
                if (initialized)
                {
                    generateSentence();
                }
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
                if (initialized)
                {
                    generateSentence();
                }
            } 
        }

        //General sentence values

        private string _packetID;
        public string PacketID
        {
            get => _packetID;
            set
            {
                _packetID = value;
                OnPropertyChanged("PacketID");
                if (initialized)
                {
                    generateSentence();
                }
            }
        }

        private string _fragCount;
        public string FragCount
        {
            get => _fragCount;
            set
            {
                _fragCount = value;
                OnPropertyChanged("FragCount");
                if (initialized)
                {
                    generateSentence();
                }
            }
        }

        private string _fragNum;
        public string FragNum
        {
            get => _fragNum;
            set
            {
                _fragNum = value;
                OnPropertyChanged("FragNum");
                if (initialized)
                {
                    generateSentence();
                }
            }
        }

        private string _sequentialMessageID;
        public string SequentialMessageID
        {
            get => _sequentialMessageID;
            set
            {
                _sequentialMessageID = value;
                OnPropertyChanged("SequentialMessageID");
                if (initialized)
                {
                    generateSentence();
                }
            }
        }

        private string _radChanCode;
        public string RadChanCode
        {
            get => _radChanCode;
            set
            {
                _radChanCode = value;
                OnPropertyChanged("RadChanCode");
                if (initialized)
                {
                    generateSentence();
                }
            }
        }

        private string _payload;
        public string Payload
        {
            get => _payload;
            set
            {
                _payload = value;
                OnPropertyChanged("Payload");
            }
        }

        private string _fillBitNum;
        public string FillBitNum
        {
            get => _fillBitNum;
            set
            {
                _fillBitNum = value;
                OnPropertyChanged("FillBitNum");
                if (initialized)
                {
                    generateSentence();
                }
            }
        }

        private string _checkSum;
        public string CheckSum
        {
            get => _checkSum;
            set
            {
                _checkSum = value;
                OnPropertyChanged("CheckSum");
            }
        }

        public string[] binaryArray { get; set; }


        public PositionReportClassA()
        {
            this.initialized = false;

            //Initializing message values with default values

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
                "0000000000000000000"
            };

            this.Sentence = "!AIVDM,1,1,,A,13HOI:0P0000VOHLCnHQKwvL05Ip,0*23";
            this.BroadcastDelay = 4;

            this.PacketID = "!AIVDM";
            this.FragCount = "1";
            this.FragNum = "1";
            this.SequentialMessageID = "";
            this.RadChanCode = "A";
            this.Payload = "";
            this.FillBitNum = "0";
            this.CheckSum = "";

            this.initialized = true;
            generateSentence();
    }

        public void generateSentence()
        {
            string binaryMessage;
            List<byte> asciiValList = new List<byte>();

            //Update the values of the binary array with the new converted input values
            this.binaryArray[0] = convertIntegerToBinary(this.MessageType.ToString(), 6);
            this.binaryArray[1] = convertIntegerToBinary(this.Repeat, 2);
            this.binaryArray[2] = convertIntegerToBinary(this.MMSI, 30);
            this.binaryArray[3] = convertIntegerToBinary(this.NavStatus, 4);
            this.binaryArray[4] = convertIntegerToBinary((4.733*Math.Sqrt(double.Parse(this.Turn))).ToString(), 8, 1);
            this.binaryArray[5] = convertIntegerToBinary(this.Speed, 10);
            this.binaryArray[6] = convertIntegerToBinary(this.Accuracy, 1);
            this.binaryArray[7] = convertIntegerToBinary(this.Longitude, 28, 600000);
            this.binaryArray[8] = convertIntegerToBinary(this.Latitude, 27, 600000);
            this.binaryArray[9] = convertIntegerToBinary(this.Course, 12, 10);
            this.binaryArray[10] = convertIntegerToBinary(this.Heading, 9);
            this.binaryArray[11] = convertIntegerToBinary(this.Timestamp, 6);
            this.binaryArray[12] = convertIntegerToBinary(this.Maneuver, 2);
            this.binaryArray[13] = convertIntegerToBinary(this.Spare, 3);
            this.binaryArray[14] = convertIntegerToBinary(this.RAIM, 1);
            this.binaryArray[15] = convertIntegerToBinary(this.Radio, 19);

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

            this.Payload = Encoding.ASCII.GetString(asciiValList.ToArray());

            this.Sentence = String.Format("{0},{1},{2},{3},{4},{5},{6}", 
                this.PacketID, 
                this.FragCount, 
                this.FragNum, 
                this.SequentialMessageID, 
                this.RadChanCode, 
                this.Payload, 
                this.FillBitNum);

            //Convert the sentence to bytes and store in an array
            byte[] tempBytes = Encoding.ASCII.GetBytes(this.Sentence.Substring(1));
            byte tempCheckSum = 0;

            //Apply XOR to each byte in the array (storing the result in tempCheckSum variable)
            for (int i=0; i<tempBytes.Length; i++)
                tempCheckSum ^= tempBytes[i];

            //Convert the tempCheckSum value to hexadecimal
            this.CheckSum = Convert.ToString(tempCheckSum, 16);

            //Add the checksum to the end of the sentence
            this.Sentence += "*" + this.CheckSum;
        }

        public string convertIntegerToBinary(string num, int padding, int multiplier=1)
        {
            ///Given a number in the form of a string (num), this method will
            ///return the binary representation of the number. If the number
            ///contains a fractional component, then the method will split it
            ///at the decimal point and compute the binary representaions of
            ///the integral and fractional parts seperately. These two values
            ///will them be combined and returned.

            string finalNum;

            if (num.Contains('.'))
            {
                int temp = (int) (float.Parse(num) * multiplier);
                finalNum = Convert.ToString(temp, 2).PadLeft(padding, '0');
            }
            else
            {
                finalNum = Convert.ToString(int.Parse(num)*multiplier, 2).PadLeft(padding, '0');
            }

            if (finalNum.Length > padding)
            {
                finalNum = finalNum.Remove(0, finalNum.Length - padding);
            }

            return finalNum;
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

    public class StaticAndVoyageRelatedData : INotifyPropertyChanged
    {
        public const string messageType = "5";

        private bool initialized;

        private string _sentenceOne;
        public string SentenceOne
        {
            get => _sentenceOne;
            set
            {
                _sentenceOne = value;
                OnPropertyChanged("SentenceOne");
            }
        }

        private string _sentenceTwo;
        public string SentenceTwo
        {
            get => _sentenceTwo;
            set
            {
                _sentenceTwo = value;
                OnPropertyChanged("SentenceTwo");
            }
        }

        private string _broadcastDelay;
        public int BroadcastDelay
        {
            get => int.Parse(_broadcastDelay);
            set
            {
                _broadcastDelay = Convert.ToString(value);
                OnPropertyChanged("BroadcastDelay");
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
                if (initialized)
                {
                    generateSentence();
                }
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
                if (initialized)
                {
                    generateSentence();
                }
            } 
        }

        private string _aisVersion;
        public string AISVersion 
        { 
            get => _aisVersion;
            set
            {
                _aisVersion = value;
                OnPropertyChanged("AISVersion");
                if (initialized)
                {
                    generateSentence();
                }
            } 
        }

        private string _imo;
        public string IMO 
        { 
            get => _imo;
            set
            {
                _imo = value;
                OnPropertyChanged("IMO");
                if (initialized)
                {
                    generateSentence();
                }
            } 
        }

        private string _callSign;
        public string CallSign 
        { 
            get => _callSign;
            set
            {
                _callSign = value;
                OnPropertyChanged("CallSign");
                if (initialized)
                {
                    generateSentence();
                }
            } 
        }

        private string _vesselName;
        public string VesselName 
        { 
            get => _vesselName;
            set
            {
                _vesselName = value;
                OnPropertyChanged("VesselName");
                if (initialized)
                {
                    generateSentence();
                }
            } 
        }

        private string _shipType;
        public string ShipType 
        { 
            get => _shipType;
            set
            {
                _shipType = value;
                OnPropertyChanged("ShipType");
                if (initialized)
                {
                    generateSentence();
                }
            } 
        }

        private string _dimensionToBow;
        public string DimensionToBow 
        { 
            get => _dimensionToBow;
            set
            {
                _dimensionToBow = value;
                OnPropertyChanged("DimensionToBow");
                if (initialized)
                {
                    generateSentence();
                }
            } 
        }

        private string _dimensionToStern;
        public string DimensionToStern 
        { 
            get => _dimensionToStern;
            set
            {
                _dimensionToStern = value;
                OnPropertyChanged("DimensionToStern");
                if (initialized)
                {
                    generateSentence();
                }
            }
        }

        private string _dimensionToPort;
        public string DimensionToPort 
        { 
            get => _dimensionToPort; 
            set
            {
                _dimensionToPort = value;
                OnPropertyChanged("DimensionToPort");
                if (initialized)
                {
                    generateSentence();
                }
            }
        }

        private string _dimensionToStarboard;
        public string DimensionToStarboard 
        { 
            get => _dimensionToStarboard;
            set
            {
                _dimensionToStarboard = value;
                OnPropertyChanged("DimensionToStarboard");
                if (initialized)
                {
                    generateSentence();
                }
            }
        }

        private string _epfd;
        public string EPFD 
        { 
            get => _epfd; 
            set
            {
                _epfd = value;
                OnPropertyChanged("EPFD");
                if (initialized)
                {
                    generateSentence();
                }
            }
        }

        private string _month;
        public string Month 
        { 
            get => _month;
            set
            {
                _month = value;
                OnPropertyChanged("Month");
                if (initialized)
                {
                    generateSentence();
                }
            } 
        }

        private string _day;
        public string Day 
        { 
            get => _day; 
            set
            {
                _day = value;
                OnPropertyChanged("Day");
                if (initialized)
                {
                    generateSentence();
                }
            }
        }

        private string _hour;
        public string Hour 
        { 
            get => _hour;
            set
            {
                _hour = value;
                OnPropertyChanged("Hour");
                if (initialized)
                {
                    generateSentence();
                }
            } 
        }

        private string _minute;
        public string Minute 
        { 
            get => _minute;
            set
            {
                _minute = value;
                OnPropertyChanged("Minute");
                if (initialized)
                {
                    generateSentence();
                }
            } 
        }

        private string _draught;
        public string Draught 
        { 
            get => _draught; 
            set
            {
                _draught = value;
                OnPropertyChanged("Draught");
                if (initialized)
                {
                    generateSentence();
                }
            }
        }

        private string _destination;
        public string Destination 
        { 
            get => _destination;
            set
            {
                _destination = value;
                OnPropertyChanged("Destination");
                if (initialized)
                {
                    generateSentence();
                }
            } 
        }

        private string _dte;
        public string DTE 
        { 
            get => _dte;
            set
            {
                _dte = value;
                OnPropertyChanged("DTE");
                if (initialized)
                {
                    generateSentence();
                }
            }
        }

        public string spare { get; set; }

        //General sentence values

        private string _packetID;
        public string PacketID
        {
            get => _packetID;
            set
            {
                _packetID = value;
                OnPropertyChanged("PacketID");
                if (initialized)
                {
                    generateSentence();
                }
            }
        }

        private string _fragCount;
        public string FragCount
        {
            get => _fragCount;
            set
            {
                _fragCount = value;
                OnPropertyChanged("FragCount");
                if (initialized)
                {
                    generateSentence();
                }
            }
        }

        private string _fragNum;
        public string FragNum
        {
            get => _fragNum;
            set
            {
                _fragNum = value;
                OnPropertyChanged("FragNum");
                if (initialized)
                {
                    generateSentence();
                }
            }
        }

        private string _sequentialMessageID;
        public string SequentialMessageID
        {
            get => _sequentialMessageID;
            set
            {
                _sequentialMessageID = value;
                OnPropertyChanged("SequentialMessageID");
                if (initialized)
                {
                    generateSentence();
                }
            }
        }

        private string _radChanCode;
        public string RadChanCode
        {
            get => _radChanCode;
            set
            {
                _radChanCode = value;
                OnPropertyChanged("RadChanCode");
                if (initialized)
                {
                    generateSentence();
                }
            }
        }

        private string _payloadOne;
        public string PayloadOne
        {
            get => _payloadOne;
            set
            {
                _payloadOne = value;
                //OnPropertyChanged("PayloadOne");
            }
        }

        private string _payloadTwo;
        public string PayloadTwo
        {
            get => _payloadTwo;
            set
            {
                _payloadTwo = value;
                //OnPropertyChanged("PayloadTwo");
            }
        }

        private string _fillBitNum;
        public string FillBitNum
        {
            get => _fillBitNum;
            set
            {
                _fillBitNum = value;
                OnPropertyChanged("FillBitNum");
            }
        }

        private string _checkSum;
        public string CheckSum
        {
            get => _checkSum;
            set
            {
                _checkSum = value;
                OnPropertyChanged("CheckSum");
            }
        }

        public StaticAndVoyageRelatedData()
        {
            this.initialized = false;

            //Initializing with default values

            this.Repeat = "0";
            this.MMSI = "000000000"; //Can change
            this.AISVersion = "0"; //Can change
            this.IMO = "000000000"; //Can change
            this.CallSign = "ABCDEFG"; //Can change
            this.VesselName = "[ADD NEW SHIP]"; //Can change
            this.ShipType = "0";
            this.DimensionToBow = "0";
            this.DimensionToStern = "0";
            this.DimensionToPort = "0";
            this.DimensionToStarboard = "0";
            this.EPFD = "0";
            this.Month = "0";
            this.Day = "0";
            this.Hour = "24";
            this.Minute = "60";
            this.Draught = "0"; //Multiply by 10 when converting from int to binary
            this.Destination = "FINALDESTINATION";
            this.DTE = "1";
            this.spare = "0";

            this.SentenceOne = "!AIVDM,1,1,,A,13HOI:0P0000VOHLCnHQKwvL05Ip,0*23";
            this.SentenceTwo = "!AIVDM,1,1,,A,13HOI:0P0000VOHLCnHQKwvL05Ip,0*23";
            this.BroadcastDelay = 4;

            this.PacketID = "!AIVDM";
            this.RadChanCode = "A";
            this.PayloadOne = "";
            this.PayloadTwo = "";
            this.FillBitNum = "0";

            this.initialized = true;
        }

        public void generateSentence()
        {
            string[] binaryArray = new string[21];
            string binaryMessage;
            List<byte> asciiValList = new List<byte>();

            

            //Update the values of the binary array with the new converted input values
            binaryArray[0] = convertIntegerToBinary(messageType.ToString(), 6);
            binaryArray[1] = convertIntegerToBinary(this.Repeat, 2);
            binaryArray[2] = convertIntegerToBinary(this.MMSI, 30);
            binaryArray[3] = convertIntegerToBinary(this.AISVersion, 2);
            binaryArray[4] = convertIntegerToBinary(this.IMO, 30);
            binaryArray[5] = convertStringToBinary(this.CallSign, 42);
            binaryArray[6] = convertStringToBinary(this.VesselName, 120);
            binaryArray[7] = convertIntegerToBinary(this.ShipType, 8);
            binaryArray[8] = convertIntegerToBinary(this.DimensionToBow, 9);
            binaryArray[9] = convertIntegerToBinary(this.DimensionToStern, 9);
            binaryArray[10] = convertIntegerToBinary(this.DimensionToPort, 6);
            binaryArray[11] = convertIntegerToBinary(this.DimensionToStarboard, 6);
            binaryArray[12] = convertIntegerToBinary(this.EPFD, 4);
            binaryArray[13] = convertIntegerToBinary(this.Month, 4);
            binaryArray[14] = convertIntegerToBinary(this.Day, 5);
            binaryArray[15] = convertIntegerToBinary(this.Hour, 5);
            binaryArray[16] = convertIntegerToBinary(this.Minute, 6);
            binaryArray[17] = convertIntegerToBinary(this.Draught, 8, 10);
            binaryArray[18] = convertStringToBinary(this.Destination, 120);
            binaryArray[19] = convertIntegerToBinary(this.DTE, 1);
            binaryArray[20] = convertIntegerToBinary(this.spare, 1);

            
            //Concatenate all of the string values in the binaryArray into one string
            binaryMessage = string.Join("", binaryArray);

            string tempStr;
            int tempInt;

            Console.WriteLine("Binary message: {0}", binaryMessage);
            Console.WriteLine("Length of binary message: {0}", binaryMessage.Length);

            //Separate the binaryMessage string into 6-char substrings and store them all in a list
            for (int i = 0; i < 424; i += 6)
            {
                if (binaryMessage.Length - i >= 6)
                    tempStr = binaryMessage.Substring(i, 6);
                else
                {
                    tempStr = binaryMessage.Substring(i).PadLeft(6, '0');
                    this.FillBitNum = Convert.ToString(binaryMessage.Length - i);
                }

                //Add 48 to decimal value
                tempInt = Convert.ToInt32(tempStr, 2) + 48;

                //Add 8 if result is greater than 87
                if (tempInt > 87)
                    tempInt += 8;
                
                asciiValList.Add(Convert.ToByte(tempInt));
            }

            //Convert binary values to decimal values and perform operations on them

            string tempPayload = Encoding.ASCII.GetString(asciiValList.ToArray());

            Console.WriteLine("Temp Payload Val : {0}", tempPayload);
            Console.WriteLine("Temp Payload Length : {0}", tempPayload.Length);

            this.PayloadOne = tempPayload.Substring(0, 60);
            this.PayloadTwo = tempPayload.Remove(0, 60);

            this.SentenceOne = String.Format("{0},{1},{2},{3},{4},{5},{6}",
                this.PacketID,
                "2",
                "1",
                "3",
                this.RadChanCode,
                this.PayloadOne,
                "0");

            this.SentenceTwo = String.Format("{0},{1},{2},{3},{4},{5},{6}",
                this.PacketID,
                "2",
                "2",
                "3",
                this.RadChanCode,
                this.PayloadTwo,
                this.FillBitNum);

            //Convert the sentences to bytes and store in an array
            byte[] tempBytesOne = Encoding.ASCII.GetBytes(this.SentenceOne.Substring(1));
            byte[] tempBytesTwo = Encoding.ASCII.GetBytes(this.SentenceTwo.Substring(1));
            byte checkSumOne = 0;
            byte checkSumTwo = 0;

            //Apply XOR to each byte in the arrays (storing the result in the checkSumOne and checkSumTwo variables)
            for (int i = 0; i < tempBytesOne.Length; i++)
                checkSumOne ^= tempBytesOne[i];

            for (int i = 0; i < tempBytesTwo.Length; i++)
                checkSumTwo ^= tempBytesTwo[i];

            //Convert the checksum values to hexadecimal and add them to the end of their respective sentences
            this.SentenceOne += "*" + Convert.ToString(checkSumOne, 16);
            this.SentenceTwo += "*" + Convert.ToString(checkSumTwo, 16);

            Console.WriteLine("Sentence 1 : {0}", this.SentenceOne);
            Console.WriteLine("Sentence 2 : {0}", this.SentenceTwo);
        }

        public string convertIntegerToBinary(string num, int padding, int multiplier = 1)
        {
            ///Given a number in the form of a string (num), this method will
            ///return the binary representation of the number. If the number
            ///contains a fractional component, then the method will split it
            ///at the decimal point and compute the binary representaions of
            ///the integral and fractional parts seperately. These two values
            ///will them be combined and returned.

            string finalNum;

            if (num.Contains('.'))
            {
                int temp = (int)(float.Parse(num) * multiplier);
                finalNum = Convert.ToString(temp, 2).PadLeft(padding, '0');
            }
            else
            {
                finalNum = Convert.ToString(int.Parse(num) * multiplier, 2).PadLeft(padding, '0');
            }

            if (finalNum.Length > padding)
            {
                finalNum = finalNum.Remove(0, finalNum.Length - padding);
            }
            

            return finalNum;
        }

        public string convertStringToBinary(string strVal, int padding)
        {
            string result = "";
            string tempStr;
            int padVal = 6; //Each char in the string strVal will be converted to a 6-bit representation

            foreach (char c in strVal)
            {
                tempStr = Convert.ToString(c, 2);

                if (tempStr.Length > padVal)
                {
                    result += tempStr.Remove(0, tempStr.Length - padVal);
                }
                else if (tempStr.Length < padVal)
                {
                    result += tempStr.PadLeft(padVal);
                }
            }

            Console.WriteLine("strVal length : {0} == {1}", strVal, result.Length);

            while (result.Length < padding)
            {
                result += "000000";
            }

            Console.WriteLine("strVal length after padding : {0} == {1}", strVal, result.Length);

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
}
