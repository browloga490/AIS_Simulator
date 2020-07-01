using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Simulator_TCP_Server_App_v2.Model
{
    class ShipModel : INotifyPropertyChanged
    {
        //private string _vesselName;
        public string VesselName
        {
            get => this.StatVoyData.vesselName;

            set
            {
                this.StatVoyData.vesselName = value;
                OnPropertyChanged("VesselName");
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
                _posRepClassA = value;
                //Console.WriteLine("Binary Conversion: '175.234' {0}",_posRepClassA.convertIntegerToBinary("175.234", 8, 20));
                //Console.WriteLine("Binary Conversion: '?' = {0}", _posRepClassA.convertStringToBinary("?"));
                //Console.WriteLine("Generated Payload: {0}", _posRepClassA.generateSentence());

                Console.WriteLine("Convert '170.1345' to binary: {0}", Convert.ToString(long.Parse("1345.01"), 2));

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

        public ShipModel(string name)
        {
            this.PosRepClassA = new PositionReportClassA();
            this.StatVoyData = new StaticAndVoyageRelatedData();
            this.StatVoyData.vesselName = name;
            this._broadcastStatus = "OFF";
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
        public int messageType { get; set; }
        public string repeat { get; set; }
        public string mmsi { get; set; }
        public string navStatus { get; set; }
        public string turn { get; set; }
        public string speed { get; set; }
        public string accuracy { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
        public string course { get; set; }
        public string heading { get; set; }
        public string timestamp { get; set; }
        public string maneuver { get; set; }
        public string spare { get; set; }
        public string raim { get; set; }
        public string radio { get; set; }

        public string[] binaryArray { get; set; }

        public PositionReportClassA()
        {
            //Initializing with default values

            this.messageType = 0; //Value initially set to 0 (default)
            this.repeat = "0";
            this.mmsi = "000000000"; //Value initially set to 0 (default)
            this.navStatus = "15";
            this.turn = "-125.001";
            this.speed = "111.1";
            this.accuracy = "0";
            this.longitude = "-170.1345"; //Value initially set to 181 (default)
            this.latitude = "-89.1234"; //Value initially set to 91 (default)
            this.course = "3600";
            this.heading = "511";
            this.timestamp = "60"; //Value initially set to 60 (default)
            this.maneuver = "0";
            this.spare = "0";
            this.raim = "0";
            this.radio = "0";

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

        public string generateSentence()
        {
            string binaryMessage;
            List<byte> sixBitList = new List<byte>();

            //Update the values of the binary array with the new converted input values
            this.binaryArray[0] = convertIntegerToBinary(this.messageType.ToString(), 6);
            this.binaryArray[1] = convertIntegerToBinary(this.repeat, 2);
            this.binaryArray[2] = convertIntegerToBinary(this.mmsi, 30);
            this.binaryArray[3] = convertIntegerToBinary(this.navStatus, 4);
            this.binaryArray[4] = convertIntegerToBinary(this.turn, 8, 3);
            this.binaryArray[5] = convertIntegerToBinary(this.speed, 7, 3);
            this.binaryArray[6] = convertIntegerToBinary(this.accuracy, 1);
            this.binaryArray[7] = convertIntegerToBinary(this.longitude, 8, 19);
            this.binaryArray[8] = convertIntegerToBinary(this.latitude, 7, 19);
            this.binaryArray[9] = convertIntegerToBinary(this.course, 12);
            this.binaryArray[10] = convertIntegerToBinary(this.heading, 9);
            this.binaryArray[11] = convertIntegerToBinary(this.timestamp, 6);
            this.binaryArray[12] = convertIntegerToBinary(this.maneuver, 2);
            this.binaryArray[13] = convertIntegerToBinary(this.spare, 3);
            this.binaryArray[14] = convertIntegerToBinary(this.raim, 1);
            this.binaryArray[15] = convertIntegerToBinary(this.radio, 19);

            binaryMessage = string.Join("", this.binaryArray);

            string temp;

            Console.WriteLine("Binary message: {0}", binaryMessage);
            Console.WriteLine("Length of binary message: {0}", binaryMessage.Length);

            for (int i=0; i<168; i+=6)
            {
                temp = binaryMessage.Substring(i, 6);
                //Console.WriteLine("i value is: {0}", i);
                //Console.WriteLine("Temp is: {0}",temp);
                sixBitList.Add(Convert.ToByte(temp, 2));
            }

            return Encoding.ASCII.GetString(sixBitList.ToArray());
        }

        public string convertIntegerToBinary(string num, int intPadding, int fractPadding=0, bool signedInteger=false)
        {
            ///Given a number in the form of a string (num), this method will
            ///return the binary representation of the number. If the number
            ///contains a fractional component, then the method will split it
            ///at the decimal point and compute the binary representaions of
            ///the integral and fractional parts seperately. These two values
            ///will them be combined and returned.

            string integralNum;
            string fractionalNum;
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
            }

            if (num.Contains('.'))
            {
                integralNum = num.Substring(0, num.IndexOf('.'));
                fractionalNum = num.Substring(num.IndexOf('.') + 1, num.Length - (num.IndexOf('.') + 1));

                finalNum = Convert.ToString(int.Parse(integralNum), 2).PadLeft(intPadding, '0');

                float tempFract = float.Parse("0."+fractionalNum);
                string tempString;

                string fractNew = "";

                for (int i=0; i<fractPadding; i++)
                {
                    if (tempFract == 0)
                        break;
                    else
                    {
                        tempString = (tempFract * 2).ToString();
                        Console.WriteLine("tempString: {0}", tempString);
                        fractNew += (tempString.Substring(0, tempString.IndexOf('.')));
                        tempFract = float.Parse("0" + tempString.Substring(tempString.IndexOf('.'), tempString.Length - (tempString.IndexOf('.'))));
                    }
                }
                finalNum += fractNew;
            }
            else
            {
                finalNum = Convert.ToString(int.Parse(num), 2).PadLeft(intPadding, '0');
            }

            Console.WriteLine("{0} => {1}  ::  Length: {2}", num, finalNum,finalNum.Length);
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
