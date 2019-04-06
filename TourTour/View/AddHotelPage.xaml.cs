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
        
        List<Hotel_service> hotelservices = new List<Hotel_service>();

        public AddHotelPage()
        {
            InitializeComponent();
            db.Hotels.Load();
            db.Countries.Load();
            db.Cities.Load();
            db.Services.Load();


            if (Adapter.CurrentId != null)
            {
                hotelid = (int)Adapter.CurrentId;
                currenthotel = db.Hotels.SingleOrDefault(x => x.hotel_id == hotelid);

                TextBoxHotelName.Text = currenthotel.hotel_name;
                TextBoxHotelStars.Text = currenthotel.stars.ToString();
                TextBoxHotelPrice.Text = currenthotel.hotel_price.ToString();
            }
            

            FillCountries();
            FillServices();
            FillGrid();
        }
       
        private void FillGrid()
        {
            var query = from hotelservice in db.Hotel_services
                        where hotelservice.hotel_id==hotelid
                        select hotelservice;
            
            hotelservices = query.ToList();

            DataGridHotelServices.ItemsSource = null;
            DataGridHotelServices.ItemsSource = hotelservices;
        }

        private void FillCountries()
        {
            ComboBoxCountry.ItemsSource = db.Countries.Local.ToBindingList();

            if (hotelid > -1)
            {
                
            }
        }

        private void FillCities()
        {
                var countryid = ComboBoxCountry.SelectedValue;
                var query = db.Countries.FirstOrDefault(x => x.country_id == (int)countryid).City;
                List<City> items = query.ToList();

                ComboBoxCity.ItemsSource = items;

                if (items.Count > 0)
                    ComboBoxCity.SelectedIndex = 0;
                else
                    ComboBoxCity.SelectedIndex = -1;
        }

        private void FillServices()
        {
            ComboBoxServices.ItemsSource = db.Services.Local.ToBindingList();
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
                    Hotel_service hs = new Hotel_service { hotel_id = hotelid, service_id = (int)hservice, service_price = temp, Service=db.Services.FirstOrDefault(x => x.service_id==(int)hservice) };

                    hotelservices.Add(hs);

                    DataGridHotelServices.ItemsSource = null;
                    DataGridHotelServices.ItemsSource = hotelservices;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ButtonDeleteHotelService_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridHotelServices.SelectedIndex > -1)
            {
                if (MessageBox.Show("Delete selected service?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    hotelservices.RemoveAt(DataGridHotelServices.SelectedIndex);

                    DataGridHotelServices.ItemsSource = null;
                    DataGridHotelServices.ItemsSource = hotelservices;
                }
            }
        }

        private void ButtonNewCity_Click(object s, RoutedEventArgs a)
        {

        }

        private void ButtonNewService_Click(object s, RoutedEventArgs a)
        {
            if (MessageBox.Show("Go to service creating page? The current progress will not be saved", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
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
                    currenthotel.Hotel_service = hotelservices;

                    if (hotelid < 0)
                    {
                        db.Hotels.Add(currenthotel);
                    }
                    
                    //// adding new services
                    //foreach (Hotel_service item in hotelservices)
                    //{
                    //    if (db.Hotel_services.Count(x => x.Hotel_service_id == item.Hotel_service_id) == 0)
                    //    {
                    //        db.Hotel_services.Add(item);
                    //    }
                    //}

                    ////removing deleted services
                    //foreach (Hotel_service item in db.Hotel_services)
                    //{
                    //    if (hotelservices.Count(x=> x.Hotel_service_id == item.Hotel_service_id) == 0)
                    //    {
                    //        db.Hotel_services.Remove(db.Hotel_services.FirstOrDefault(x => x.Hotel_service_id == item.Hotel_service_id));
                    //    }
                    //}

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