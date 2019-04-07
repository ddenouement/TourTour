using System.Windows;
using System.Windows.Controls;
using TourTour.Utilities;

namespace TourTour.View
{
    /// <summary>
    /// Логика взаимодействия для MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Page
    {
        public MainMenu()
        {
            InitializeComponent();
            if (Adapter.AdminMode()) LabelRole.Content = "Admin";
            else LabelRole.Content = "User";
        }

        private void ButtonTours_Click(object sender, RoutedEventArgs a)
        {
            this.NavigationService.Navigate(new ToursPage());
        }
        private void ButtonClients_Click(object sender, RoutedEventArgs a)
        {
            if (Adapter.AdminMode()) this.NavigationService.Navigate(new ClientsPage());
            else MessageBox.Show("You are not admin");
        }
        private void ButtonHotels_Click(object sender, RoutedEventArgs a)
        {
            if (Adapter.AdminMode()) this.NavigationService.Navigate(new HotelsPage());
            else MessageBox.Show("You are not admin");
        }
        private void ButtonZvit_Click(object sender, RoutedEventArgs a)
        {
            if (Adapter.AdminMode()) this.NavigationService.Navigate(new ReportPage());
            else MessageBox.Show("You are not admin");
        }
        private void ButtonLogOut_Click(object sender, RoutedEventArgs a)
        {
            Adapter.Logout();
            this.NavigationService.Navigate(new LoginPage());
        }
        private void ButtonCart_Click(object sender, RoutedEventArgs a)
        {
            this.NavigationService.Navigate(new CartPage());
        }

        private void ButtonVouchers_Click(object sender, RoutedEventArgs e)
        {
            if (Adapter.AdminMode()) this.NavigationService.Navigate(new VouchersPage());
            else MessageBox.Show("You are not admin");
        }
    }
}
