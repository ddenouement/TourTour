using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TourTour.Utilities;
using TourTour.ViewModel;

namespace TourTour.View
{
    /// <summary>
    /// Interaction logic for CartPage.xaml
    /// </summary>
    public partial class CartPage : Page
    {
        List<Tour> cart;

        public CartPage()
        {
            InitializeComponent();
            FillGrid();
        }

        private void FillGrid()
        {
            cart = new List<Tour>();
            using (DBContext db=new DBContext())
            {
                foreach (int t in Adapter.Cart)
                {
                    Tour to = db.Tours.Include("Hotel.Hotel_service.Service").Include("Hotel.City.Country").Single(x => x.tour_id == t);
                    cart.Add(to);//db.Tours.FirstOrDefault(x=>x.tour_id==t).);
                }
            }

            DataGridCart.ItemsSource = null;
            DataGridCart.ItemsSource = cart;
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

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
        {
            int id = GetCurrentID(sender);

            using (DBContext db = new DBContext())
            {
                if (!db.Tours.FirstOrDefault(x => x.tour_id == id).availability) MessageBox.Show("Sorry, the tour is currently unavailable");
                else
                {
                    Adapter.CurrentCartId = id;
                    this.NavigationService.Navigate(new OrderPage());
                }
            }
        }

        private void ButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            int id = GetCurrentID(sender);

            if (MessageBox.Show("Remove selected tour?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Adapter.Cart.Remove(Adapter.Cart.FirstOrDefault(x => x == id));
                FillGrid();
            }
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Adapter.CurrentCartId = null;
            this.NavigationService.Navigate(new MainMenu());
        }

        private int GetCurrentID(object sender)
        {
            object obj = ((FrameworkElement)sender).DataContext as object;
            System.Reflection.PropertyInfo pi = obj.GetType().GetProperty("tour_id");
            int id = (int)(pi.GetValue(obj, null));

            return id;
        }
    }
}
