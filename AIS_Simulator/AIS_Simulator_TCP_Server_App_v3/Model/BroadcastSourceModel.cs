using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Simulator_TCP_Server_App_v3.Model
{
    class BroadcastSourceModel
    {
        private bool initialized;

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

        //Message Sentence Values

        private string _packetID;
        public string PacketID
        {
            get => _packetID;
            set
            {
                _packetID = value;
                OnPropertyChanged("PacketID");
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

        //Payload Values

        //Type 1, 2, and 3 message payload values

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
                OnPropertyChanged("IsNewShip");
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

        //Type 5 message payload values

        private string _aisVersion;
        public string AISVersion
        {
            get => _aisVersion;
            set
            {
                _aisVersion = value;
                OnPropertyChanged("AISVersion");
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
            }
        }

        public BroadcastSourceModel()
        {
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
}
