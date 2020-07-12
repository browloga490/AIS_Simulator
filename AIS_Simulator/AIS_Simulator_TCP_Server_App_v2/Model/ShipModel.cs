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

        public string MMSI
        {
            get => MTypeOne.MMSI;
            set
            {
                MTypeOne.MMSI = value;
                MTypeFive.MMSI = value;
                OnPropertyChanged("MMSI");
            }
        }

        private MessageTypeOneModel _mTypeOne;
        public MessageTypeOneModel MTypeOne
        {
            get => _mTypeOne;
            set
            {
                _mTypeOne = value;
                _mTypeOne.generateSentence();
                OnPropertyChanged("MTypeOne");
            }
        }

        private MessageTypeFiveModel _mTypeFive;
        public MessageTypeFiveModel MTypeFive
        {
            get => _mTypeFive;
            set
            {
                _mTypeFive = value;
                OnPropertyChanged("MTypeFive");
            }
        }

        public ShipModel()
        {
            this.MTypeOne = new MessageTypeOneModel();
            this.MTypeFive = new MessageTypeFiveModel();
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
