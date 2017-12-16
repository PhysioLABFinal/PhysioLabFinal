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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PhysioProject2
{
    /// <summary>
    /// Interaction logic for Clients.xaml
    /// </summary>
    public partial class Clients : Page
    {
        public Clients()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void SearchTxt_GotFocus(object sender, RoutedEventArgs e)
        {
            SearchTxt.Text = "";
        }

       

        private void ClientdataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ClientInfo info = new ClientInfo();
            info.Show();
        }

        private void addclieck(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
