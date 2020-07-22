using AIS_Simulator_TCP_Server_App_v2.Model;
using AIS_Simulator_TCP_Server_App_v2.ViewModel;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace AIS_Simulator_TCP_Server_App_v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly ShipViewModel VM;

        public MainWindow()
        {
            //Retrieve a saved state of the ShipViewModel (if one exists)
            try
            {
                string json = System.IO.File.ReadAllText(String.Format(@"{0}\ViewModelState.txt", System.AppDomain.CurrentDomain.BaseDirectory));

                VM = JsonConvert.DeserializeObject<ShipViewModel>(json);
                VM.ShipList.Remove(VM.ShipList.First());
            }

            //Create a new instance of the ShipViewModel if a saved state does not exist
            catch (FileNotFoundException)
            {
                VM = new ShipViewModel();
            }

            DataContext = VM;
            InitializeComponent();

            MainView.Visibility = Visibility.Visible;
            ConfigureView.Visibility = Visibility.Hidden;

            Console.WriteLine("CONVERRSION: 00100001 == {0}", Convert.ToInt32("00100001",2));

            VM.ServerStatusBox = txtStatus;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            VM.StartTCPServer();
            txtStatus.ScrollToEnd();
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            VM.StopTCPServer();
            txtStatus.ScrollToEnd();
        }

        private void BtnConfigueShip_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvShipList.SelectedValue == null))
            {
                MainView.Visibility = Visibility.Hidden;
                ConfigureView.Visibility = Visibility.Visible;
                svStatVoyData.Visibility = Visibility.Hidden;
                grdShipMovement.Visibility = Visibility.Hidden;
                svPosRep.Visibility = Visibility.Visible;
                PosRepMenuItem.Focus();
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            ConfigureView.Visibility = Visibility.Hidden;
            MainView.Visibility = Visibility.Visible;
            VM.SaveConfiguration(txtVesselName.Text);
        }

        private void StatVoyDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            grdShipMovement.Visibility = Visibility.Hidden;
            svPosRep.Visibility = Visibility.Hidden;
            svStatVoyData.Visibility = Visibility.Visible;
        }

        private void PosRepMenuItem_Click(object sender, RoutedEventArgs e)
        {
            grdShipMovement.Visibility = Visibility.Hidden;
            svStatVoyData.Visibility = Visibility.Hidden;
            svPosRep.Visibility = Visibility.Visible;
        }

        private void ShipMotionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            svPosRep.Visibility = Visibility.Hidden;
            svStatVoyData.Visibility = Visibility.Hidden;
            grdShipMovement.Visibility = Visibility.Visible;
        }

        private void BtnStartBroadcasting_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvShipList.SelectedValue == null))
                VM.StartBroadcast();
        }

        private void BtnStopBroadcasting_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvShipList.SelectedValue == null))
                VM.StopBroadcast();
        }

        private void BtnRemoveShip_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvShipList.SelectedValue == null))
                VM.RemoveShip();
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            VM.Server = new TCPServerModel();
            VM.ServerStatusBox = null;

            string jsonData = JsonConvert.SerializeObject(VM);
            System.IO.File.WriteAllText(String.Format(@"{0}\ViewModelState.txt", System.AppDomain.CurrentDomain.BaseDirectory), jsonData);
        }
    }
}
