using AIS_Simulator_TCP_Server_App_v2.Model;
using AIS_Simulator_TCP_Server_App_v2.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Threading;
using System.Windows;

namespace AIS_Simulator_TCP_Server_App_v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ShipViewModel VM;

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
            catch (FileNotFoundException e)
            {
                VM = new ShipViewModel();
            }

            DataContext = VM;
            InitializeComponent();

            MainView.Visibility = Visibility.Visible;
            ConfigureView.Visibility = Visibility.Hidden;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            VM.startTCPServer();
            txtStatus.ScrollToEnd();
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            VM.stopTCPServer();
            txtStatus.ScrollToEnd();
        }

        private void BtnConfigueShip_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvShipList.SelectedValue == null))
            {
                MainView.Visibility = Visibility.Hidden;
                ConfigureView.Visibility = Visibility.Visible;
                svStatVoyData.Visibility = Visibility.Hidden;
                svShipMovement.Visibility = Visibility.Hidden;
                svPosRep.Visibility = Visibility.Visible;
            }
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            ConfigureView.Visibility = Visibility.Hidden;
            MainView.Visibility = Visibility.Visible;
            VM.saveConfiguration(txtVesselName.Text);
        }

        private void StatVoyDataMenuItem_Click(object sender, RoutedEventArgs e)
        {
            svShipMovement.Visibility = Visibility.Hidden;
            svPosRep.Visibility = Visibility.Hidden;
            svStatVoyData.Visibility = Visibility.Visible;
        }

        private void PosRepMenuItem_Click(object sender, RoutedEventArgs e)
        {
            svShipMovement.Visibility = Visibility.Hidden;
            svStatVoyData.Visibility = Visibility.Hidden;
            svPosRep.Visibility = Visibility.Visible;
        }

        private void ShipMotionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            svPosRep.Visibility = Visibility.Hidden;
            svStatVoyData.Visibility = Visibility.Hidden;
            svShipMovement.Visibility = Visibility.Visible;
        }

        private void btnStartBroadcasting_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvShipList.SelectedValue == null))
                VM.startBroadcast();
        }

        private void btnStopBroadcasting_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvShipList.SelectedValue == null))
                VM.stopBroadcast();
        }

        private void btnRemoveShip_Click(object sender, RoutedEventArgs e)
        {
            if (!(lvShipList.SelectedValue == null))
                VM.removeShip();
        }

        private void WindowClosing(object sender, CancelEventArgs e)
        {
            VM.Server = new TCPServerModel();
            string jsonData = JsonConvert.SerializeObject(VM);
            System.IO.File.WriteAllText(String.Format(@"{0}\ViewModelState.txt", System.AppDomain.CurrentDomain.BaseDirectory), jsonData);
        }
    }
}
