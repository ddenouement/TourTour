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

namespace TourTour.View
{
    /// <summary>
    /// Логика взаимодействия для ZvitPage.xaml
    /// </summary>
    public partial class ZvitPage : Page
    {
        public ZvitPage()
        {
            InitializeComponent();
        }
        private void btn_back_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenu());

        }

    }
}
