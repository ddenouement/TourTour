using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TourTour.Utilities;
using TourTour.ViewModel;

namespace TourTour.View
{
    /// <summary>
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        ClientsViewModel cvm;

        public ClientsPage()
        {

            InitializeComponent();
            FillGrid();
        }
        private void FillGrid()
        {
            cvm = new ClientsViewModel();
            DataGridClients.ItemsSource = null;
            DataGridClients.ItemsSource = cvm.Clients;
        }
        private void ButtonClientInfo_Click(object sender, RoutedEventArgs e)
        {
            int id = GetCurrentID(sender);

            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    row.DetailsVisibility = row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    break;
                }
        }
       
        private void ButtonVouchers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(cvm.Clients.ElementAt(0).Paychecks.ElementAt(0).Voucher.voucher_id+"");
        }

        private void ButtonPayed_Click(object sender, RoutedEventArgs e)
        {
            int id = GetCurrentID(sender);
            try
            {
                using (DBContext db = new DBContext())
                {
                    db.Paychecks.FirstOrDefault(x => x.voucher_id == id).payed = true;
                    db.Paychecks.FirstOrDefault(x => x.voucher_id == id).pay_date = DateTime.Today;
                    db.SaveChanges();
                }
                FillGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            int id = GetCurrentID(sender);
            
            Adapter.CurrentId = id;
            this.NavigationService.Navigate(new AddClientPage());
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = GetCurrentID(sender);

            if (MessageBox.Show("Delete selected client?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    using (DBContext db = new DBContext())
                    {
                        db.Clients.Remove(db.Clients.FirstOrDefault(x => x.client_id == id));
                        db.SaveChanges();
                    }
                    MessageBox.Show("Client deleted successfully");
                    FillGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Adapter.CurrentId = null;
            this.NavigationService.Navigate(new MainMenu());
        }

        private int GetCurrentID(object sender)
        {
            object obj = ((FrameworkElement)sender).DataContext as object;
            System.Reflection.PropertyInfo pi = obj.GetType().GetProperty("client_id");
            int id = (int)(pi.GetValue(obj, null));

            return id;
        }

        
    }
}
