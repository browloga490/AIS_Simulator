﻿using AIS_Simulator_TCP_Server_App_v2.View;
using AIS_Simulator_TCP_Server_App_v2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AIS_Simulator_TCP_Server_App_v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ShipViewModel VM = new ShipViewModel();
        public MainWindow()
        {
            DataContext = VM;
            InitializeComponent();
        }
    }
}
