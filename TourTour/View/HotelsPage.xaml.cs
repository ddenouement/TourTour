using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes; 

namespace TourTour.View
{
    /// <summary>
    /// Логика взаимодействия для HotelsPage.xaml
    /// </summary>
    public partial class HotelsPage : Page
    {
        int PK_ID;
        public HotelsPage()
        {
            InitializeComponent();
        }

        private void fillgrid()
        { 

        }
        private void btn_back_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenu());

        }
        private void btn_edit(object s, RoutedEventArgs a)
        {
            var id1 = (DataRowView)datagridHotels.SelectedItem;  //Get  ID From  DataGrid after click on Delete Button.

            PK_ID = Convert.ToInt32(id1.Row["id"].ToString());
            //   SqlConnection con = new SqlConnection(sqlstring);
            //   con.Open();
            //    string sqlquery = "delete from t where id='" + PK_ID + "' ";
            //   SqlCommand cmd = new SqlCommand(sqlquery, con);
            //   cmd.ExecuteNonQuery();
            fillgrid();

        }
        private void btn_delete(object s, RoutedEventArgs a)
        {
        }
         
        private void btn_add_click(object s, RoutedEventArgs a) {
            this.NavigationService.Navigate(new AddHotelPage());
        }
    }
}
