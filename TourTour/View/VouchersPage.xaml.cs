using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TourTour.Utilities;

namespace TourTour.View
{
    /// <summary>
    /// Interaction logic for VouchersPage.xaml
    /// </summary>
    public partial class VouchersPage : Page
    {
        
        public VouchersPage()
        {
            InitializeComponent();
            FillGrid();
        }

        private void FillGrid()
        {
            using (DBContext db = new DBContext())
            {
                
                DataGridPaychecks.ItemsSource = null;
                  DataGridPaychecks.ItemsSource = db.Paychecks.Include("Client").Include("Voucher").Where(x => x.payed == false).ToList();
                
            
            }
        }

        private void ButtonPayed_Click(object sender, RoutedEventArgs e)
        {
            int id = GetCurrentID(sender);
            try
            {
                using (DBContext db = new DBContext())
                {
                    db.Paychecks.FirstOrDefault(x => x.check_id == id).payed = true;
                    db.Paychecks.FirstOrDefault(x => x.check_id == id).pay_date = DateTime.Today;
                    db.SaveChanges();
                }
                FillGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int GetCurrentID(object sender)
        {
            object obj = ((FrameworkElement)sender).DataContext as object;
            System.Reflection.PropertyInfo pi = obj.GetType().GetProperty("client_id");
            int id = (int)(pi.GetValue(obj, null));

            return id;
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenu());
        }

        
    }
}
