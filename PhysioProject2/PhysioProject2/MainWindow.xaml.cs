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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void client_button(object sender, RoutedEventArgs e)
        {
            Main.Content = new Clients();
        }

        private void exit(object sender, RoutedEventArgs e)
        {
            this.Close();

        }

        private void products_button(object sender, RoutedEventArgs e)
        {
            Main.Content = new ();
        }

        private void appointm_button(object sender, RoutedEventArgs e)
        {
            Main.Content = new Appointments();
        }

        

        private void payments_button(object sender, RoutedEventArgs e)
        {
            Main.Content = new Payments();
        }
    }
}
