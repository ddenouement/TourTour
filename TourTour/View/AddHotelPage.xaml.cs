using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TourTour.Utilities;

namespace TourTour.View
{
    /// <summary>
    /// Логика взаимодействия для AddHotelPage.xaml
    /// </summary>
    public partial class AddHotelPage : Page
    {
        int hotelid = -1;
        Hotel currenthotel = new Hotel();
        DBContext db = new DBContext();
        
        ObservableCollection<object> hotelservicesview = new ObservableCollection<object>();

        public AddHotelPage()
        {
            InitializeComponent();
            if (Adapter.CurrentId != null)
            {
                hotelid = (int)Adapter.CurrentId;
            }

            db.Hotels.Load();
            db.Countries.Load();
            db.Cities.Load();
            db.Services.Load();

            FillCountries();
            FillServices();
            FillGrid();

            //if (hotelid > -1)
            //    hotelservices = db.Hotels.FirstOrDefault(x => x.hotel_id == hotelid).Hotel_service;
        }
       
        private void FillGrid()
        {
            DbSet<Hotel_service> temp111 = db.Hotel_services;
            var query = from hotelservice in db.Hotel_services
                        join service in db.Services on hotelservice.service_id equals service.service_id
                        where hotelservice.hotel_id == hotelid
                        select new
                        {
                            ServiceID = service.service_id,
                            ServiceName = service.service_name,
                            ServiceHotelPrice = hotelservice.service_price
                        };
            
            hotelservicesview = new ObservableCollection<object>(query);
            DataGridHotelServices.ItemsSource = hotelservicesview.ToBindingList();
        }

        private void FillCities()
        {
            var countryid = ComboBoxCountry.SelectedValue;
            ObservableCollection<City> items = new ObservableCollection<City>(db.Countries.FirstOrDefault(x => x.country_id == (int)countryid).City);

            ComboBoxCity.ItemsSource = items.ToBindingList();

            if (items.Count > 0)
                ComboBoxCity.SelectedIndex = 0;
            else
                ComboBoxCity.SelectedIndex = -1;
        }

        private void FillServices()
        {
            ComboBoxServices.ItemsSource = db.Services.Local.ToBindingList();
        }

        private void FillCountries()
        {
            ComboBoxCountry.ItemsSource = db.Countries.Local.ToBindingList();
        }

        private void ButtonAddHotelService_Click(object s, RoutedEventArgs e)
        {
            var hservice = ComboBoxServices.SelectedValue;
            string hserviceprice = TextBoxServicePrice.Text;

            if (ComboBoxServices.SelectedIndex<=-1) MessageBox.Show("Service name value mustn't be empty");
            else
            if (hserviceprice.Length==0) MessageBox.Show("Service price value mustn't be empty");
            else
            if (!Decimal.TryParse(hserviceprice, out decimal temp) || temp < 0) MessageBox.Show("Service price value must be positive decimal");
            else
            {
                try
                {
                    db.Hotel_services.Add(new Hotel_service { hotel_id = hotelid, service_id = (int)hservice, service_price = temp });

                    DbSet<Hotel_service> temp111 = db.Hotel_services;

                    FillGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ButtonDeleteHotelService_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void ButtonNewCity_Click(object s, RoutedEventArgs a)
        {

        }

        private void ButtonNewService_Click(object s, RoutedEventArgs a)
        {
            if (MessageBox.Show("Cancel? The current progress will not be saved", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Adapter.CurrentId = null;
                this.NavigationService.Navigate(new HotelsPage());
            }
        }

        private void ButtonSubmit_Click(object a, RoutedEventArgs d)
        {
            string hname = TextBoxHotelName.Text;
            string hstars = TextBoxHotelStars.Text;
            string hprice = TextBoxHotelPrice.Text;
            var hcountry = ComboBoxCountry.SelectedValue;
            var hcity = ComboBoxCity.SelectedValue;

            if (hname.Length == 0) MessageBox.Show("Hotel name value mustn't be empty");
            else
            if (hstars.Length == 0) MessageBox.Show("Hotel stars value mustn't be empty");
            else
            if (!Int32.TryParse(hstars, out int temp) || temp < 1 || temp>5) MessageBox.Show("Tour stars value must be positive integer from 1 to 5");
            else
            if (hprice.Length == 0) MessageBox.Show("Hotel price value mustn't be empty");
            else
            if (!Decimal.TryParse(hprice, out decimal temp1) || temp1 < 0) MessageBox.Show("Hotel price value must be positive decimal");
            else
            if (ComboBoxCountry.SelectedIndex <= -1) MessageBox.Show("Country value mustn't be empty");
            else
            if (ComboBoxCity.SelectedIndex <= -1) MessageBox.Show("City value mustn't be empty");
            else
            {
                try
                {
                    currenthotel.hotel_name = hname;
                    currenthotel.stars = temp;
                    currenthotel.hotel_price = temp1;
                    currenthotel.city_id = (int)hcity;

                    if (hotelid == -1) {
                        foreach (var item in db.Hotel_services.Where(x => x.hotel_id == hotelid))
                        {
                            item.hotel_id = currenthotel.hotel_id;
                        }
                    }

                    db.SaveChanges();

                    MessageBox.Show(hname + " hotel created successfully");
                    this.NavigationService.Navigate(new HotelsPage());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ButtonCancel_Click(object s, RoutedEventArgs a)
        {
            if (MessageBox.Show("Cancel? The current progress will not be saved", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Adapter.CurrentId = null;
                this.NavigationService.Navigate(new HotelsPage());
            }
            
        }

        private void ComboBoxCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillCities();
        }

        
    }
}