using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TourTour.Utilities;

namespace TourTour.View
{
    /// <summary>
    /// Логика взаимодействия для SignUpPage.xaml
    /// </summary>
    public partial class SignUpPage : Page
    {
        public SignUpPage()
        {
            InitializeComponent();
            this.ShowsNavigationUI = false;

        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService ns = NavigationService.GetNavigationService(this);
            LoginPage page = new LoginPage();

            ns.Navigate(page);
        }

        private void ButtonSignUp_Click(object sender, RoutedEventArgs e)
        {

            //TODO: form validation!!
            //   if(true)

            if (Adapter.Signup(TextBoxUsername.Text, TextBoxEmail.Text, PasswordBoxPassword.Password))
            {
                NavigationService ns = NavigationService.GetNavigationService(this);
                MainMenu page = new MainMenu();

                ns.Navigate(page);
            }
            else
                MessageBox.Show("Something went wrong");

        }

    }
}
