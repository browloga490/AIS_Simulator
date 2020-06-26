using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Simulator_TCP_Server_App.Resource_Dictionaries
{
    public class ViewModel : ViewModelBase
    {
        private List<Ship> _shipList;
        public List<Ship> ShipList
        {
            get => _shipList;
            set => Set(ref _shipList, value, true);
        }

        public string _serverLog;
        public string ServerLog
        {
            get => _serverLog;
            set => Set(ref _serverLog, value, true);
        }

        public ViewModel()
        {
            this.ShipList = new List<Ship>();
            this.ServerLog = "";
        }
    }

    public class Ship
    {
        public string vesselName { get; set; }
        public string broadcastStatus { get; set; }

    }

    public class PositionReportClassA
    {
        public string messageType { get; set; }
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
        public string timeStamp { get; set; }
        public string maneuver { get; set; }
        public string spare { get; set; }
        public string raim { get; set; }
        public string radio { get; set; }

        public string[] binaryArray { get; set; }

        public PositionReportClassA()
        {
            //Initializing with default values

            this.messageType = "0"; //Value initially set to 0 (default)
            this.repeat = "0";
            this.mmsi = "000000000"; //Value initially set to 0 (default)
            this.navStatus = "15";
            this.turn = "128";
            this.speed = "123";
            this.accuracy = "0";
            this.longitude = "181"; //Value initially set to 181 (default)
            this.latitude = "91"; //Value initially set to 91 (default)
            this.course = "3600";
            this.heading = "111111111";
            this.timeStamp = "60"; //Value initially set to 60 (default)
            this.maneuver = "0";
            this.spare = "N/A";
            this.raim = "0";
            this.radio = "N/A";

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
