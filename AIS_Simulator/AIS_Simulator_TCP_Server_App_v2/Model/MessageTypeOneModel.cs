﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace AIS_Simulator_TCP_Server_App_v2.Model
{
    public class MessageTypeOneModel : INotifyPropertyChanged
    {
        private readonly bool initialized;

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
                    GenerateSentence();
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
                    GenerateSentence();
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
                    GenerateSentence();
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
                    GenerateSentence();
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
                    GenerateSentence();
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
                    GenerateSentence();
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
                    GenerateSentence();
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
                    GenerateSentence();
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
                OnPropertyChanged("Latitude");
                if (initialized)
                {
                    GenerateSentence();
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
                    GenerateSentence();
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
                    GenerateSentence();
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
                    GenerateSentence();
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
                    GenerateSentence();
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
                    GenerateSentence();
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
                    GenerateSentence();
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
                    GenerateSentence();
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
                    GenerateSentence();
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
                    GenerateSentence();
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
                    GenerateSentence();
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
                    GenerateSentence();
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
                    GenerateSentence();
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
                    GenerateSentence();
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

        //Ship Motion values

        private int _movementType;
        public int MovementType
        {
            get => _movementType;
            set
            {
                _movementType = value;
                OnPropertyChanged("MovementType");
            }
        }

        //Circular Motion Values

        private string _radius;
        public string Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                OnPropertyChanged("Radius");
            }

        }

        private string _centrePointLongitude;
        public string CentrePointLongitude
        {
            get => _centrePointLongitude;
            set
            {
                _centrePointLongitude = value;
                OnPropertyChanged("CentrePointLongitude");
            }
        }

        private string _centrePointLatitude;
        public string CentrePointLatitude
        {
            get => _centrePointLatitude;
            set
            {
                _centrePointLatitude = value;
                OnPropertyChanged("CentrePointLatitude");
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

            this.MovementType = 0;
            this.Radius = "5";
            this.CentrePointLongitude = "180.0000";
            this.CentrePointLatitude = "90.0000";

            this.initialized = true;
        }

        public MessageTypeOneModel(MessageTypeOneModel templateMTypeOne)
        {
            this.initialized = false;

            //Initializing message values from a given template

            this.MessageType = templateMTypeOne.MessageType;
            this.Repeat = templateMTypeOne.Repeat;
            this.MMSI = templateMTypeOne.MMSI;
            this.NavStatus = templateMTypeOne.NavStatus;
            this.Turn = templateMTypeOne.Turn;
            this.Speed = templateMTypeOne.Speed;
            this.Accuracy = templateMTypeOne.Accuracy;
            this.Longitude = templateMTypeOne.Longitude;
            this.Latitude = templateMTypeOne.Latitude;
            this.Course = templateMTypeOne.Course;
            this.Heading = templateMTypeOne.Heading;
            this.Timestamp = templateMTypeOne.Timestamp;
            this.Maneuver = templateMTypeOne.Maneuver;
            this.Spare = templateMTypeOne.Spare;
            this.RAIM = templateMTypeOne.RAIM;
            this.Radio = templateMTypeOne.Radio;

            this.Sentence = templateMTypeOne.Sentence;
            this.BroadcastDelay = templateMTypeOne.BroadcastDelay;

            this.PacketID = templateMTypeOne.PacketID;
            this.FragCount = templateMTypeOne.FragCount;
            this.FragNum = templateMTypeOne.FragNum;
            this.SequentialMessageID = templateMTypeOne.SequentialMessageID;
            this.RadChanCode = templateMTypeOne.RadChanCode;
            this.Payload = templateMTypeOne.Payload;
            this.FillBitNum = templateMTypeOne.FillBitNum;
            this.CheckSum = templateMTypeOne.CheckSum;

            this.MovementType = templateMTypeOne.MovementType;
            this.Radius = templateMTypeOne.Radius;
            this.CentrePointLongitude = templateMTypeOne.CentrePointLongitude;
            this.CentrePointLatitude = templateMTypeOne.CentrePointLatitude;

            this.initialized = true;
        }

        public void GenerateSentence()
        {
            string[] binaryArray = new string[16];
            string binaryMessage;
            List<byte> asciiValList = new List<byte>();

            //Update the values of the binary array with the new converted input values
            binaryArray[0] = ConvertIntegerToBinary(this.MessageType.ToString(), 6);
            binaryArray[1] = ConvertIntegerToBinary(this.Repeat, 2);
            binaryArray[2] = ConvertIntegerToBinary(this.MMSI, 30);
            binaryArray[3] = ConvertIntegerToBinary(this.NavStatus, 4);
            binaryArray[4] = ConvertIntegerToBinary((4.733 * Math.Sqrt(double.Parse(this.Turn))).ToString(), 8, 1);
            binaryArray[5] = ConvertIntegerToBinary(this.Speed, 10);
            binaryArray[6] = ConvertIntegerToBinary(this.Accuracy, 1);
            binaryArray[7] = ConvertIntegerToBinary(this.Longitude, 28, 600000);
            binaryArray[8] = ConvertIntegerToBinary(this.Latitude, 27, 600000);
            binaryArray[9] = ConvertIntegerToBinary(this.Course, 12, 10);
            binaryArray[10] = ConvertIntegerToBinary(this.Heading, 9);
            binaryArray[11] = ConvertIntegerToBinary(this.Timestamp, 6);
            binaryArray[12] = ConvertIntegerToBinary(this.Maneuver, 2);
            binaryArray[13] = ConvertIntegerToBinary(this.Spare, 3);
            binaryArray[14] = ConvertIntegerToBinary(this.RAIM, 1);
            binaryArray[15] = ConvertIntegerToBinary(this.Radio, 19);

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

        public string ConvertIntegerToBinary(string num, int padding, int multiplier = 1)
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
