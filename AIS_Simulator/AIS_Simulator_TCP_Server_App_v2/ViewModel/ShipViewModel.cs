using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using AIS_Simulator_TCP_Server_App_v2.Model;

namespace AIS_Simulator_TCP_Server_App_v2.ViewModel
{
    class ShipViewModel
    {
        private ObservableCollection<ShipModel> _shipList;
        private string _serverStatus;

        public ShipViewModel()
        {
            _shipList = new ObservableCollection<ShipModel>()
            {
                new ShipModel("[ADD NEW SHIP]")
            };

            //if (_shipList[_shipList.Count -1].VesselName == "[ADD NEW SHIP]")
            //{
            //}
            //
            //UpdateCommand.Execute(true);
        }

        public ObservableCollection<ShipModel> ShipList
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

        public void saveConfiguration(string shipName)
        {
            if (!(_shipList[_shipList.Count-1].VesselName.Equals("[ADD NEW SHIP]")))
            {
                _shipList.Add(new ShipModel("[ADD NEW SHIP]"));
            }
        }

        private static readonly KeyValuePair<int, string>[] messageTypePosRepList = {
            new KeyValuePair<int, string>(0, "Position Report Class A"),
            new KeyValuePair<int, string>(1, "Position Report Class A (Assigned schedule)"),
            new KeyValuePair<int, string>(2, "Position Report Class A (Response to interrogation)"),
        };

        public KeyValuePair<int, string>[] MessageTypePosRepList
        {
            get => messageTypePosRepList;
        }
    }
}
