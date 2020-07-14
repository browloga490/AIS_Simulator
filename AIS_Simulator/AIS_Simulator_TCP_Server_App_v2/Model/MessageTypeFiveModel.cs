using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Simulator_TCP_Server_App_v2.Model
{
    class MessageTypeFiveModel
    {
        public const string messageType = "5";

        private readonly bool initialized;

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
            }
        }

        private string _payloadTwo;
        public string PayloadTwo
        {
            get => _payloadTwo;
            set
            {
                _payloadTwo = value;
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

        public MessageTypeFiveModel()
        {
            this.initialized = false;

            //Initializing with default values

            this.Repeat = "0";
            this.MMSI = "000000000";
            this.AISVersion = "0";
            this.IMO = "000000000";
            this.CallSign = "ABCDEFG";
            this.VesselName = "[ADD NEW SHIP]";
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
            this.Draught = "0";
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

            //Iterate through the binaryMessage string converting every group of 6-bits to its ascii decimal value
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
            this.SentenceOne += "*" + Convert.ToString(checkSumOne, 16).PadLeft(2, '0');
            this.SentenceTwo += "*" + Convert.ToString(checkSumTwo, 16).PadLeft(2, '0');
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
            ///Given a string (strVal), this method converts each char in the string 
            ///to its 6-bit representation. It returns a string of all of the concatonated
            ///6-bit representations, padding to the left to match the final length with the given
            ///padding value (padding).
            
            string result = "";
            string tempStr;
            int padVal = 6;

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

            while (result.Length < padding)
            {
                result += "000000";
            }

            return result;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
