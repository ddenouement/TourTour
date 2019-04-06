using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TourTour.Utilities;
using TourTour.ViewModel;

namespace TourTour.View
{
    /// <summary>
    /// Логика взаимодействия для HotelsPage.xaml
    /// </summary>
    public partial class HotelsPage : Page
    {
        HotelsViewModel hvm;
        
        public HotelsPage()
        {
            InitializeComponent();
            FillGrid();
        }
        
        private void ButtonHotelInfo_Click(object sender, RoutedEventArgs e)
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

        private void FillGrid()
        {
            hvm = new HotelsViewModel();
            DataGridHotels.ItemsSource = null;
            DataGridHotels.ItemsSource = hvm.Hotels;
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            int id = GetCurrentID(sender);

            Adapter.CurrentId = id;
            this.NavigationService.Navigate(new AddHotelPage());
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = GetCurrentID(sender);

            if (MessageBox.Show("Delete selected hotel?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    using (DBContext db = new DBContext())
                    {
                        db.Hotels.Remove(db.Hotels.FirstOrDefault(x => x.hotel_id == id));
                        db.SaveChanges();
                    }
                    MessageBox.Show("Hotel deleted successfully");
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

        private void ButtonNewHotel_Click(object sender, RoutedEventArgs e)
        {
            Adapter.CurrentId = null;
            this.NavigationService.Navigate(new AddHotelPage());
        }

        private int GetCurrentID(object sender)
        {
            object obj = ((FrameworkElement)sender).DataContext as object;
            System.Reflection.PropertyInfo pi = obj.GetType().GetProperty("hotel_id");
            int id = (int)(pi.GetValue(obj, null));

            return id;
        }
    }
}
