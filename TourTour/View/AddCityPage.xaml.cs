using System;
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
using TourTour.Utilities;

namespace TourTour.View
{
    /// <summary>
    /// Interaction logic for AddCityPage.xaml
    /// </summary>
    public partial class AddCityPage : Page
    {
        DBContext db = new DBContext();

        public AddCityPage()
        {
            InitializeComponent();
            FillCountries();
        }

        private void FillCountries()
        {
            int id = DataGridCountries.SelectedIndex;
            DataGridCountries.ItemsSource = null;
            DataGridCountries.ItemsSource = db.Countries.ToList();
            DataGridCountries.SelectedIndex = id;
        }

        private void FillCities()
        {
            Country i = (Country)DataGridCountries.SelectedItem;
            DataGridCities.ItemsSource = null;
            //DataGridCities.ItemsSource = db.Countries.FirstOrDefault(x => x.country_id == i.country_id).City.ToList();
            if (i!=null)
            DataGridCities.ItemsSource = i.City.ToList();
        }

        private void ButtonNewCountry_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxCountryName.Text.Length > 0)
            {
                db.Countries.Add(new Country { country_name = TextBoxCountryName.Text });
                FillCountries();
            }
        }

        private void ButtonChangeCountryName_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxChangeCountryName.Text.Length > 0)
            {
                int i = (int)DataGridCountries.SelectedValue;
                db.Countries.FirstOrDefault(x => x.country_id == i).country_name = TextBoxChangeCountryName.Text;
                FillCountries();
            }
        }

        private void ButtonNewCity_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxCityName.Text.Length > 0)
            {
                int i = (int)DataGridCountries.SelectedValue;
                db.Countries.FirstOrDefault(x=>x.country_id==i).City.Add(new City { city_name = TextBoxCityName.Text });
                FillCities();
            }
        }

        private void ButtonChangeCityName_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxChangeCityName.Text.Length > 0)
            {
                int i = (int)DataGridCountries.SelectedValue;
                int temp = (int)DataGridCities.SelectedValue;
                db.Countries.FirstOrDefault(x => x.country_id == i).City.FirstOrDefault(x=>x.city_id==temp).city_name= TextBoxChangeCityName.Text;
                FillCities();
            }
        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
            this.NavigationService.Navigate(new HotelsPage());
        }

        private void DataGridCountries_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FillCities();
            Country temp = (Country)DataGridCountries.SelectedItem;
            TextBoxChangeCountryName.Text = temp.country_name;
        }

        private void DataGridCities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            City temp = (City)DataGridCities.SelectedItem;
            if (temp == null) TextBoxChangeCityName.Text = "";
            else
            TextBoxChangeCityName.Text = temp.city_name;
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Cancel? The current progress will not be saved", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Adapter.CurrentId = null;
                this.NavigationService.Navigate(new HotelsPage());
            }
        }

        
    }
}
