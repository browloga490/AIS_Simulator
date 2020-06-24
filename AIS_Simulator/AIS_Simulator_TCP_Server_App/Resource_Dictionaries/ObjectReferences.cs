using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AIS_Simulator_TCP_Server_App.Resource_Dictionaries
{
    class ObjectReferences
    {
    }

    public class Ship
    {
        public string vesselName { get; set; }
        public string broadcastStatus { get; set; }

    }

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

    public class PositionReportClassA
    {

    }
}
