using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TourTour.Utilities;

namespace TourTour.View
{
    /// <summary>
    /// Interaction logic for AddServicePage.xaml
    /// </summary>
    public partial class AddServicePage : Page
    {
        DBContext db = new DBContext();
        public AddServicePage()
        {
            InitializeComponent();
            FillServices();
        }

        public void FillServices()
        {
            DataGridServices.ItemsSource = null;
            DataGridServices.ItemsSource = db.Services.ToList();
        }

        private void ButtonNewService_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxServiceName.Text.Length > 0)
            {
                db.Services.Add(new Service { service_name = TextBoxServiceName.Text });
                FillServices();
            }
        }

        private void ButtonChangeServiceName_Click(object sender, RoutedEventArgs e)
        {
            if (TextBoxChangeServiceName.Text.Length > 0)
            {
                int i = (int)DataGridServices.SelectedValue;
                db.Services.FirstOrDefault(x => x.service_id == i).service_name = TextBoxChangeServiceName.Text;
                FillServices();
            }
        }

        private void DataGridServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Service s = (Service)DataGridServices.SelectedItem;
            if (s == null) TextBoxChangeServiceName.Text = "";
            else
            TextBoxChangeServiceName.Text = s.service_name;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            db.SaveChanges();
            this.NavigationService.Navigate(new HotelsPage());
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
