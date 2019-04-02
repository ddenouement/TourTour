using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace TourTour.View
{
    /// <summary>
    /// Логика взаимодействия для AddHotelPage.xaml
    /// </summary>
    public partial class AddHotelPage : Page
    {
        string sqlstring = "";
        private Dictionary<string, int> adds;
        public AddHotelPage()
        {
            this.adds = new Dictionary<string, int>();
            InitializeComponent();
            fillItems();
        }
       
        private void fillItems()
        {
           /* SqlConnection con = new SqlConnection(sqlstring);
            con.Open();
            string sqlquery = "select * from table_hotels";
            SqlCommand cmd = new SqlCommand(sqlquery, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
           
            //так же заполнить comboboxes с названиями с бд сервисов additional services

            con.Close();*/
        }

        private void ButtonAddServiceToHotel_Click(object s, RoutedEventArgs e)
        {
            int pr;
            if (int.TryParse(TextBoxServicePrice.Text, out pr))
            {
                adds.Add(ComboBoxServices.Text, pr);
            }
            else TextBoxServicePrice.Text = "";

        }

        private void UpdateEntity()
        {

        }
        private void AddEntity()
        {
            SqlConnection con = new SqlConnection(sqlstring);
            con.Open();
            string sqlquery = "insert into dsh_wpfCRUD (name,address,gender,country,state,city,contactno) values (@name,@address,@gender,@country,@state,@city,@contactno)";
            SqlCommand cmd = new SqlCommand(sqlquery, con);

            //надо привести к int 
            cmd.Parameters.AddWithValue("@hotel_name",TextBoxHotelName.Text);
            cmd.Parameters.AddWithValue("@hotel_price", TextBoxHotelPrice.Text);
            cmd.Parameters.AddWithValue("@hotel_stars", TextBoxHotelStars.Text);
            //+добавить город и сервисы
            cmd.ExecuteNonQuery();

        }

        private void ButtonNewCity_Click(object s, RoutedEventArgs a)
        {

        }

        private void ButtonNewService_Click(object s, RoutedEventArgs a)
        {

        }

        private void ButtonSubmit_Click(object a, RoutedEventArgs d)
        {
            string s = TextBoxHotelName.Text;
            string sqlquery = "select COUNT(*)  from table_Hotels where NameHotel='" + s + "'"; //exists in DB with this name
            SqlConnection con = new SqlConnection(sqlstring);
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlquery, con);
            int userCount = (int)cmd.ExecuteScalar();
            con.Close();
            bool ex = (userCount != 0);
            if (ex)
            {
                UpdateEntity();
            }
            else
            {
                AddEntity();
                // clearform();

            }
        }

        private void ButtonCancel_Click(object s, RoutedEventArgs a)
        {
            this.NavigationService.Navigate(new HotelsPage());
        }
    }
}