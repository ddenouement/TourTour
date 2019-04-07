using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TourTour.Utilities;
using System.Data.Entity;

namespace TourTour.View
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public int total_price;
        public int tourid;
        public int price_per_person;
        public int ppls;
        DBContext db = new DBContext();
        public Tour curr_tour = new Tour();

        public OrderPage()
        {

            InitializeComponent();
            db.Hotels.Load();
            db.Countries.Load();
            db.Cities.Load();
            db.Services.Load();

            if (Adapter.CurrentId != null)
            {
                tourid = (int)Adapter.CurrentId;
                curr_tour =   db.Tours.Include("Hotel.Hotel_service.Service").Include("Hotel.City.Country").Single(x => x.tour_id == tourid);

                TextBoxPplCount.Text = "1";//default
                tour_name_label.Content = curr_tour.tour_name;
            }
             
            if ( ! CurrentUser.IsAdmin())
            {//нельзя выбрать другого клиента если ты не админ
                ComboBoxClient.Visibility = Visibility.Collapsed;
            }
            else
            {
                FillDataInCobmboboxClients();
            }
        }
        private void FillDataInCobmboboxClients()//clients
        {
            var query = db.Clients;

            List<Client> items = query.ToList();

            ComboBoxClient.ItemsSource = items;

            if (items.Count > 0)
                ComboBoxClient.SelectedIndex = 0;
            else
                ComboBoxClient.SelectedIndex = -1;
        }
        private void ComboBoxClient_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ButtonSubmit_Click(object a, RoutedEventArgs d)
        {
            int idclient = -1;
            ReloadPrices();
            if (CurrentUser.IsAdmin()) {   idclient = (int)ComboBoxClient.SelectedValue; MessageBox.Show(idclient+""); }
            else { }//TODO как определить клиента по юзеру?????????????????????????????????????????????????????????????????????????
            DateTime st =   PickStart.SelectedDate.Value  ;
            DateTime en = PickEnd.SelectedDate.Value;
            if (st == null || en == null) return;
           
        }
            private void ButtonCancel_Click(object s, RoutedEventArgs a)
        {
            if (MessageBox.Show("Cancel? The current progress will not be saved", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Adapter.CurrentId = null;
                this.NavigationService.Navigate(new CartPage());
            }

        }
        public void ReloadPrices()
        {
            int ppl_count = -1;
            int.TryParse(TextBoxPplCount.Text, out ppl_count);
            if (ppl_count == -1) { MessageBox.Show("Error in people count"); return; }
            ppls = ppl_count;
            price_per_person = (int)( curr_tour.Hotel.hotel_price + curr_tour.avia_cost);
            total_price = ppls * price_per_person;

            price_total_label.Content = total_price;
            price_one_person_label.Content = price_per_person;
            
        }

        private void TextBoxPplCount_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            ReloadPrices();
        }
    }
}
