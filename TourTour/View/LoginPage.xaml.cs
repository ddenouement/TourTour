using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using TourTour.Utilities;

namespace TourTour.View
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            this.ShowsNavigationUI = false;
        }

        private void ButtonSignUp_Click(object sender, RoutedEventArgs e)
        {
         
           NavigationService ns = NavigationService.GetNavigationService(this);
            SignUpPage page = new SignUpPage();

          ns.Navigate(page);

        }

        private void ButtonLogIn_Click(object sender, RoutedEventArgs e)
        {
           if (Adapter.Login(TextBoxUsername.Text, PasswordBoxPassword.Password)) { 
          // if(true){
                NavigationService ns = NavigationService.GetNavigationService(this);
                MainMenu page = new MainMenu();

                ns.Navigate(page);
            }
            else
                MessageBox.Show("Login or password is incorrect");

        }

    }
}
