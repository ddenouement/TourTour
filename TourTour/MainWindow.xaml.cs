using System.Windows;
using System.Windows.Navigation;
using TourTour.View;

namespace TourTour
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
            mainFrm.Content = new LoginPage();
            mainFrm.NavigationUIVisibility = NavigationUIVisibility.Hidden;            

        }
        
    }
}
