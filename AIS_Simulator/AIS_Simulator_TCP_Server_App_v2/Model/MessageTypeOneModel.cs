using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Simulator_TCP_Server_App_v2.Model
{
    public class MessageTypeOneModel : INotifyPropertyChanged
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

        //General AIVDM sentence values

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

        public MessageTypeOneModel()
        {
            this.initialized = false;

            //Initializing message values with default values

            this.MessageType = 0;
            this.Repeat = "0";
            this.MMSI = "000000000";
            this.NavStatus = "15";
            this.Turn = "701.234";
            this.Speed = "100.1";
            this.Accuracy = "0";
            this.Longitude = "181";
            this.Latitude = "91";
            this.Course = "360";
            this.Heading = "54";
            this.Timestamp = "60";
            this.Maneuver = "0";
            this.Spare = "0";
            this.RAIM = "0";
            this.Radio = "0";

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
            string[] binaryArray = new string[16];
            string binaryMessage;
            List<byte> asciiValList = new List<byte>();

            //Update the values of the binary array with the new converted input values
            binaryArray[0] = convertIntegerToBinary(this.MessageType.ToString(), 6);
            binaryArray[1] = convertIntegerToBinary(this.Repeat, 2);
            binaryArray[2] = convertIntegerToBinary(this.MMSI, 30);
            binaryArray[3] = convertIntegerToBinary(this.NavStatus, 4);
            binaryArray[4] = convertIntegerToBinary((4.733 * Math.Sqrt(double.Parse(this.Turn))).ToString(), 8, 1);
            binaryArray[5] = convertIntegerToBinary(this.Speed, 10);
            binaryArray[6] = convertIntegerToBinary(this.Accuracy, 1);
            binaryArray[7] = convertIntegerToBinary(this.Longitude, 28, 600000);
            binaryArray[8] = convertIntegerToBinary(this.Latitude, 27, 600000);
            binaryArray[9] = convertIntegerToBinary(this.Course, 12, 10);
            binaryArray[10] = convertIntegerToBinary(this.Heading, 9);
            binaryArray[11] = convertIntegerToBinary(this.Timestamp, 6);
            binaryArray[12] = convertIntegerToBinary(this.Maneuver, 2);
            binaryArray[13] = convertIntegerToBinary(this.Spare, 3);
            binaryArray[14] = convertIntegerToBinary(this.RAIM, 1);
            binaryArray[15] = convertIntegerToBinary(this.Radio, 19);

            binaryMessage = string.Join("", binaryArray);

            string tempStr;
            int tempInt;

            //Iterate through the binaryMessage string converting every group of 6-bits to its ascii decimal value
            for (int i = 0; i < 168; i += 6)
            {
                tempStr = binaryMessage.Substring(i, 6);

                // add 48 to decimal value
                tempInt = Convert.ToInt32(tempStr, 2) + 48;

                // add 8 if result is greater than 87
                if (tempInt > 87)
                {
                    tempInt += 8;
                }

                //Console.WriteLine("Conversion :: {0}", tempInt);
                asciiValList.Add(Convert.ToByte(tempInt));
            }

            //Convert binary values to decimal values and perform operations on them
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
            for (int i = 0; i < tempBytes.Length; i++)
                tempCheckSum ^= tempBytes[i];

            //Convert the tempCheckSum value to hexadecimal
            this.CheckSum = Convert.ToString(tempCheckSum, 16).PadLeft(2,'0');

            //Add the checksum to the end of the sentence
            this.Sentence += "*" + this.CheckSum;
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
