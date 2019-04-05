using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TourTour.Utilities;

namespace TourTour.View
{
    /// <summary>
    /// Interaction logic for AddTourPage.xaml
    /// </summary>
    public partial class AddTourPage : Page
    {
        int tourid = -1;
        Tour currenttour = new Tour();
        DBContext db = new DBContext();

        public AddTourPage()
        {
            InitializeComponent();

            db.Tours.Load();
            db.Hotels.Load();
            ComboBoxHotel.ItemsSource = db.Hotels.Local.ToBindingList();

            if (Adapter.CurrentId != null)
            {
                tourid = (int)Adapter.CurrentId;
                currenttour = db.Tours.SingleOrDefault(x => x.tour_id == tourid);

                TextBoxTourName.Text = currenttour.tour_name;
                TextBoxMileage.Text = currenttour.mileage.ToString();
                TextBoxTransferCost.Text = currenttour.transfer.ToString();
                TextBoxDescription.Text = currenttour.description;
                TextBoxAviaCost.Text = currenttour.avia_cost.ToString();
                CheckBoxAvailable.IsChecked = currenttour.availability;
                ComboBoxHotel.SelectedValue = currenttour.hotel_id;
            }
        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            string tname = TextBoxTourName.Text;
            string tmileage = TextBoxMileage.Text;
            string ttransfer = TextBoxTransferCost.Text;
            string taviacost = TextBoxAviaCost.Text;
            string tdescr = TextBoxDescription.Text;
            var thotel = ComboBoxHotel.SelectedValue;


            if (tname.Length == 0) MessageBox.Show("Tour name value mustn't be empty");
            else
            if (tmileage.Length == 0) MessageBox.Show("Tour mileage value mustn't be empty");
            else
            if (!Int32.TryParse(tmileage, out int temp) || temp < 0) MessageBox.Show("Tour mileage value must be positive integer");
            else
            if (ttransfer.Length == 0) MessageBox.Show("Transfer cost value mustn't be empty");
            else
            if (!Decimal.TryParse(ttransfer, out decimal temp1) || temp1 < 0) MessageBox.Show("Transfer cost value must be positive decimal");
            else
            if (taviacost.Length > 0 && !Decimal.TryParse(TextBoxAviaCost.Text, out temp1) || temp < 0) MessageBox.Show("Avia cost value must be positive decimal");
            else
            if (tdescr.Length == 0) MessageBox.Show("Tour description value mustn't be empty");
            else
            if (ComboBoxHotel.SelectedIndex <= -1) MessageBox.Show("Hotel value mustn't be empty");
            else
            {
                try
                {
                    currenttour.tour_name = tname;
                    currenttour.mileage = Int32.Parse(tmileage);
                    currenttour.transfer = Decimal.Parse(ttransfer);
                    currenttour.availability = CheckBoxAvailable.IsChecked.Value;
                    currenttour.description = tdescr;
                    if (taviacost.Length > 0) currenttour.avia_cost = Decimal.Parse(taviacost);
                    currenttour.hotel_id = (int)thotel;

                    db.SaveChanges();


                    MessageBox.Show(tname + " tour created successfully");
                    this.NavigationService.Navigate(new ToursPage());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ButtonCreateHotel_Click(object sender, RoutedEventArgs a)
        {
            if (MessageBox.Show("Go to hotel creation page? The current progress will not be saved", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Adapter.CurrentId = null;
                this.NavigationService.Navigate(new AddHotelPage());
            }
        }

        private void ButtonCancel_Click(object s, RoutedEventArgs a)
        {
            if (MessageBox.Show("Cancel? The current progress will not be saved", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Adapter.CurrentId = null;
                this.NavigationService.Navigate(new ToursPage());
            }
        }
    }
}
