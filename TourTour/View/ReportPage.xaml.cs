using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TourTour.Utilities;

namespace TourTour.View
{
    /// <summary>
    /// Interaction logic for ReportPage.xaml
    /// </summary>
    public partial class ReportPage : Page
    {
        public ReportPage()
        {
            InitializeComponent();
            FillGrid();
        }

        private void FillGrid()
        {
            using (DBContext db=new DBContext())
            {
                DataGridReport.ItemsSource = null;
                DataGridReport.ItemsSource = db.Tours.Include("Voucher").Include("Hotel.City.Country").ToList();
            }
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDlg = new PrintDialog();
            printDlg.PrintVisual(DataGridReport, "ToursReport");
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenu());
        }
    }
}
