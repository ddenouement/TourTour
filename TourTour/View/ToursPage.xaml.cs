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
    /// Логика взаимодействия для ToursPage.xaml
    /// </summary>
    public partial class ToursPage : Page
    //колонки country i city берем с таблицы Отель соответсвенной для этого Тура(дублируем просто чтобы клиент видел)
    {
        ToursViewModel tvm;

        public ToursPage()
        {
            InitializeComponent();
            FillGrid();
            if (!Adapter.AdminMode())
            {
                ButtonNewTour.Visibility = Visibility.Hidden;

                ColumnDelete.Visibility = Visibility.Collapsed;
                ColumnEdit.Visibility = Visibility.Collapsed;
            }
            else
            {
                ColumnAddToCart.Visibility = Visibility.Collapsed;
            }
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
            tvm = new ToursViewModel();
            DataGridTours.ItemsSource = tvm.items.ToBindingList();
        }

        private void ButtonEdit_Click(object sender, RoutedEventArgs e)
        {
            int id = GetCurrentID(sender);
            Adapter.CurrentId = id;

            this.NavigationService.Navigate(new AddTourPage());
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = GetCurrentID(sender);

            if (MessageBox.Show("Delete selected tour?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    using (DBContext db = new DBContext())
                    {
                        db.Tours.Remove(db.Tours.FirstOrDefault(x => x.tour_id == id));
                        db.SaveChanges();
                    }
                    MessageBox.Show("Tour deleted successfully");
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

        private void ButtonNewTour_Click(object sender, RoutedEventArgs e)
        {
            Adapter.CurrentId = null;
            this.NavigationService.Navigate(new AddTourPage());
        }

        private void ButtonAddToCart_Click(object sender, RoutedEventArgs a)
        {

        }


        private int GetCurrentID(object sender)
        {
            object obj = ((FrameworkElement)sender).DataContext as object;
            System.Reflection.PropertyInfo pi = obj.GetType().GetProperty("ID");
            int id = (int)(pi.GetValue(obj, null));

            return id;
        }
    }
}
