﻿using System;
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
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using AprioriTID.DAO;

namespace AprioriTID.View
{
    /// <summary>
    /// Interaction logic for Process.xaml
    /// </summary>
    public partial class Process : MetroWindow
    {
        public Process()
        {
            InitializeComponent();
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.MainView.Content = new ProcessSupportPage();
            Constant.parentFrame = this.MainView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
