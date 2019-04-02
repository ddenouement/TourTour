using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        //пример
      //  string sqlstring = "Data Source=SQLDB;Initial Catalog=Demo;Persist Security Info=True;User ID=demoh;Password=Demo1@";

        static int PK_ID;
        public ClientsPage()
        {

            InitializeComponent();
            fillgrid();
        }
        private void fillgrid()
        {
            //обновляем инфу в таблице datagridClients
         //   SqlConnection con = new SqlConnection(sqlstring);
       ////     con.Open();
        //    string sqlquery = "select * from dsh_wpfCRUD";
         //   SqlCommand cmd = new SqlCommand(sqlquery, con);
           // SqlDataAdapter adp = new SqlDataAdapter(cmd);
            // DataTable dt = new DataTable();
            //adp.Fill(dt);
          //  datagridClients.ItemsSource = dt.DefaultView;
        //    con.Close();
        }
        private void clearForm()
        {
            txtname.Text = string.Empty;
            txtsurname.Text = string.Empty;
            txtfathername.Text = string.Empty;
            dateOBpicker.SelectedDate=DateTime.Now;
            txtemail.Text = string.Empty;
            txtcountry.Text = string.Empty;
            txtaddress.Text = string.Empty;
            txtcity.Text = string.Empty;
            txtcontactno.Text = string.Empty;
        }
        private void btn_clearform_click(object sender, RoutedEventArgs e)
        {
            clearForm();
        }
        private void btn_submit_new_client_click(object sender, RoutedEventArgs e)
        {//update або add (update якщо нажали до цього на кнопку edit. Add - по замовчуванню

            if (btnsubmit.Content.Equals("Update"))
            {
                //Code for updating data
                //  SqlConnection con = new SqlConnection(sqlstring);
                // con.Open();
                //string sqlquery = "update dsh_wpfCRUD set name=@name,address=@address,gender=@gender,country=@country,state=@state,city=@city,contactno=@contactno where id='" + PK_ID + "'";
                //SqlCommand cmd = new SqlCommand(sqlquery, con);
                //cmd.Parameters.AddWithValue("@name", txtname.Text);
                //cmd.Parameters.AddWithValue("@address", txtname.Text);
                //   cmd.Parameters.AddWithValue("@dateofbirth", dateOBpicker.toString());
                //cmd.Parameters.AddwithValue("@email, txtemail.toString());
                // cmd.Parameters.AddWithValue("@country", txtcountry.Text);
                //   cmd.Parameters.AddWithValue("@state", txtstate.Text);
                //   cmd.Parameters.AddWithValue("@city", txtcity.Text);
                //   cmd.Parameters.AddWithValue("@contactno", txtcontactno.Text);
                //   cmd.ExecuteNonQuery();
                fillgrid();
                clearForm();
            }
        
         else //("Add"))
            {


                //пример как додавати дані до БД
                /*   SqlConnection con = new SqlConnection(sqlstring);
                   con.Open();
                   string sqlquery = "insert into dsh_wpfCRUD (name,address,gender,country,state,city,contactno) values (@name,@address,@gender,@country,@state,@city,@contactno)";
                   SqlCommand cmd = new SqlCommand(sqlquery, con);
                   cmd.Parameters.AddWithValue("@name", txtname.Text);
                   cmd.Parameters.AddWithValue("@address", txtaddress.Text);
                    cmd.Parameters.AddWithValue("@country", txtcountry.Text);
                    cmd.Parameters.AddWithValue("@dateofbirth", dateOBpicker.toString());
                   cmd.Parameters.AddWithValue("@state", txtstate.Text);
                    cmd.Parameters.AddwithValue("@email, txtemail.toString());
                   cmd.Parameters.AddWithValue("@city", txtcity.Text);
                   cmd.Parameters.AddWithValue("@contactno", txtcontactno.Text);
                   cmd.ExecuteNonQuery();*/
                fillgrid();
                clearForm();
            }
        }
        private void btn_edit(object sender, RoutedEventArgs e)
        {

            var id1 = (DataRowView)datagridClients.SelectedItem; //get specific ID from   DataGrid after click on Edit button in DataGrid  
         //   PK_ID = Convert.ToInt32(id1.Row["id"].ToString());
         //   SqlConnection con = new SqlConnection(sqlstring);
         //   con.Open();
         //   string sqlquery = "select * from dsh_wpfCRUD where id='" + PK_ID + "' ";
          //  SqlCommand cmd = new SqlCommand(sqlquery, con);
         //   SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
          //  adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                txtname.Text = dt.Rows[0]["name"].ToString();
                txtfathername.Text= dt.Rows[0]["fathername"].ToString();
                txtsurname.Text = dt.Rows[0]["surname"].ToString();
                dateOBpicker.SelectedDate = DateTime.Parse( dt.Rows[0]["dateofbirth"].ToString());
                txtemail.Text = dt.Rows[0]["email"].ToString();
                txtaddress.Text = dt.Rows[0]["address"].ToString();               
                txtcountry.Text = dt.Rows[0]["country"].ToString();
                txtstate.Text = dt.Rows[0]["state"].ToString();
                txtcity.Text = dt.Rows[0]["city"].ToString();
                txtcontactno.Text = dt.Rows[0]["contactno"].ToString();
            }
            btnsubmit.Content = "Update";
        }
        private void btn_delete(object sender, RoutedEventArgs e)
        {
           var id1 = (DataRowView)datagridClients.SelectedItem;  //Get  ID From  DataGrid after click on Delete Button.

            PK_ID = Convert.ToInt32(id1.Row["id"].ToString());
         //   SqlConnection con = new SqlConnection(sqlstring);
         //   con.Open();
        //    string sqlquery = "delete from dsh_wpfCRUD where id='" + PK_ID + "' ";
         //   SqlCommand cmd = new SqlCommand(sqlquery, con);
         //   cmd.ExecuteNonQuery();
            fillgrid();
        }
        private void btn_back_click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new  MainMenu());

        }
    }
}
