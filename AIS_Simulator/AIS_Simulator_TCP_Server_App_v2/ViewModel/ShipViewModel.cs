using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AIS_Simulator_TCP_Server_App_v2.Model;

namespace AIS_Simulator_TCP_Server_App_v2.ViewModel
{
    class ShipViewModel
    {
        private IList<ShipModel> _shipList;
        private string _serverStatus;

        public ShipViewModel()
        {
            _shipList = new List<ShipModel>()
            {
                new ShipModel{VesselName="[ADD NEW SHIP]", BroadcastStatus="OFF", PosRepClassA= new PositionReportClassA()},
                new ShipModel{VesselName="[ADD NEW SHIP]", BroadcastStatus="OFF", PosRepClassA= new PositionReportClassA()}
            };

        }

        public IList<ShipModel> ShipList
        {
            get => _shipList;
            set => _shipList = value;
        }

        private ICommand mUpdater;
        public ICommand UpdateCommand
        {
            get
            {
                if (mUpdater == null)
                    mUpdater = new Updater();
                return mUpdater;
            }
            set => mUpdater = value;
        }

        private class Updater : ICommand
        {

            public bool CanExecute(object parameter) { return true; }

            public event EventHandler CanExecuteChanged;

            public void Execute(object parameter) { }
        }

        public void ConfigureShip()
        {
        }
    }
}
