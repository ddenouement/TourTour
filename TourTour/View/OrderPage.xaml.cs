using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TourTour.Utilities;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace TourTour.View
{
    /// <summary>
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        public int tourid;
        public decimal defaultinsurance = 60;
        public Tour currenttour = new Tour();

        public OrderPage()
        {
            InitializeComponent();

            using (DBContext db = new DBContext())
            {
                db.Hotels.Load();
                db.Countries.Load();
                db.Cities.Load();
                db.Services.Load();

                tourid = (int)Adapter.CurrentCartId;
                currenttour = db.Tours.Include("Hotel.Hotel_service.Service").Include("Hotel.City.Country").Single(x => x.tour_id == tourid);

                TextBoxPplCount.Text = "1"; //default
                LabelTourName.Content = currenttour.tour_name;
            }
        }

        private void ButtonOrder_Click(object a, RoutedEventArgs d)
        {
            ReloadPrices();
            DateTime st = DatePickerStart.SelectedDate.Value;
            DateTime en = DatePickerEnd.SelectedDate.Value;
            string pplamt = TextBoxPplCount.Text;

            if (!Int32.TryParse(TextBoxPplCount.Text, out int count)) MessageBox.Show("Please, write a valid amount of people");
            else
            if (st == null || en == null) MessageBox.Show("Please, choose the dates of your tour");
            else
            if (st < DateTime.Today || en < DateTime.Today || st.Equals(en)) MessageBox.Show("Start and end dates must be in future");
            else
            {
                Paycheck p = new Paycheck();
                p.Voucher = new Voucher { start_date = (DateTime)st, end_date = (DateTime)en, insurance_cost = defaultinsurance };
                p.ppl_count = count;
                p.payed = false;

                Adapter.TemporaryPaycheck = p;

                this.NavigationService.Navigate(new AddClientPage());
            }
        }

        

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void DatePickerStart_SelectedDateChanged(object s, RoutedEventArgs a)
        {
            ReloadPrices();
        }

        private void DatePickerEnd_SelectedDateChanged(object s, RoutedEventArgs a)
        {
            ReloadPrices();
        }

        public void ReloadPrices()
        {
            LabelInsurance.Content = "Waiting for all data...";
            LabelTotal.Content = "Waiting for all data...";

            if (!Int32.TryParse(TextBoxPplCount.Text, out int count))
            {
                MessageBox.Show("Invalid amount of people");
                return;
            }
            DateTime? st = DatePickerStart.SelectedDate;
            DateTime? en = DatePickerEnd.SelectedDate;
            if (st == null || en == null || st < DateTime.Today || en < DateTime.Today || st.Equals(en)) return;
            DateTime st1 = (DateTime)st;
            DateTime en1 = (DateTime)en;
            int days = (int)(en1.Subtract(st1).TotalDays);

            decimal insurancetotal = defaultinsurance * days * count;
            LabelInsurance.Content = insurancetotal;

            decimal total=(currenttour.transfer+currenttour.avia_cost+currenttour.Hotel.hotel_price*days)*count + insurancetotal;
            LabelTotal.Content = total;
            
        }

        private void TextBoxPplCount_TextChanged(object sender, TextChangedEventArgs e)
        {
           
            ReloadPrices();
        }

        private void ButtonCancel_Click(object s, RoutedEventArgs a)
        {
            if (MessageBox.Show("Cancel? The current progress will not be saved", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Adapter.CurrentCartId = null;
                this.NavigationService.Navigate(new CartPage());
            }
        }
    }
}
