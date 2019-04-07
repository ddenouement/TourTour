using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using TourTour.Utilities;

namespace TourTour.View
{
    /// <summary>
    /// Логика взаимодействия для AddClientPage.xaml
    /// </summary>
    public partial class AddClientPage : Page
    {
        int currentid = -1;

        public AddClientPage()
        {
            InitializeComponent();
            if (Adapter.CurrentId != null)
            {
                currentid = (int)Adapter.CurrentId;
            }
        }

        private void ButtonSubmit_Click(object sender, RoutedEventArgs e)
        {
            string cname = TextBoxName.Text;
            string csurname = TextBoxSurname.Text;
            string cpatronymic = TextBoxPatronymic.Text;
            string cemail = TextBoxEmail.Text;
            DateTime? cbday = DatePickerBirthDate.SelectedDate;
            string cphone = TextBoxPhone.Text;
            string ccountry = TextBoxCountry.Text;
            string cregion = TextBoxRegion.Text;
            string ccity = TextBoxCity.Text;
            string carea = TextBoxArea.Text;
            string cstreet = TextBoxStreet.Text;
            string chouse = TextBoxHouse.Text;
            string cblock = TextBoxBlock.Text;
            string cap = TextBoxApartment.Text;

            if (cname.Length == 0 || csurname.Length == 0 || cpatronymic.Length == 0 || cemail.Length == 0 || ccountry.Length == 0 ||
                ccity.Length == 0 || cstreet.Length == 0 || chouse.Length == 0 || cap.Length == 0) MessageBox.Show("Please, fill in all the mandatory fields (*)");
            else
            if (!(new EmailAddressAttribute().IsValid(cemail))) MessageBox.Show("Please, write a valid e-mail");
            else
            if (cbday == null || cbday > DateTime.Now) MessageBox.Show("Please, choose a valid date of birth");
            else
            if (cphone.Length>0 && !Regex.Match(cphone, @"(^\+\d{1,2})?((\(\d{3}\))|(\-?\d{3}\-)|(\d{3}))((\d{3}\-\d{4})|(\d{3}\-\d\d\  
-\d\d)|(\d{7})|(\d{3}\-\d\-\d{3}))").Success) MessageBox.Show("Please, write a valid phone number");
            else
            if (!Int32.TryParse(chouse, out int temp) || temp < 0) MessageBox.Show("House value must be positive integer");
            else
            if (!Int32.TryParse(cap, out int temp1) || temp1 < 0) MessageBox.Show("Apartment value must be positive integer");
            else
            {
                Client client = new Client
                {
                    client_name = cname,
                    client_surname = csurname,
                    client_patronym = cpatronymic,
                    email = cemail,
                    birth_date = (DateTime)cbday,
                    country = ccountry,
                    city = ccity,
                    street = cstreet,
                    house = temp,
                    apt = temp1
                };

                if (cregion.Length > 0) client.region = cregion;
                if (carea.Length > 0) client.area = carea;
                if (cblock.Length > 0) client.block = cblock;
                if (cphone.Length > 0) client.Phone_number.Add(new Phone_number { phone_num = cphone });

                using (DBContext db=new DBContext())
                {
                    //todo: add function to manager to check if email exists
                    Client tempclient = db.Clients.FirstOrDefault(x => x.email == cemail && x.client_name == cname && x.client_surname == csurname && x.client_patronym == cpatronymic);
                    

                    if (tempclient != null)
                    {
                        tempclient = client;
                        if (Adapter.CurrentCartId!=null)
                        {
                            tempclient.Paychecks.Add(Adapter.TemporaryPaycheck);
                        }
                    }
                    else
                    {
                        if (Adapter.CurrentCartId!=null)
                        {
                            client.Paychecks.Add(Adapter.TemporaryPaycheck);
                        }
                        db.Clients.Add(client);
                    }
                    
                    db.SaveChanges();
                }

                if (Adapter.CurrentCartId!=null)
                {
                    MessageBox.Show("Order created successfully");
                    Adapter.Cart.Remove((int)Adapter.CurrentCartId);
                    Adapter.CurrentCartId = null;
                    Adapter.CurrentId = null;
                    Adapter.TemporaryPaycheck = new Paycheck();
                    this.NavigationService.Navigate(new CartPage());
                }
                else
                if (currentid < 0)
                {
                    MessageBox.Show("Client created successfully");
                    this.NavigationService.Navigate(new MainMenu());
                }
                else
                {
                    MessageBox.Show("Client updated successfully");
                    this.NavigationService.Navigate(new ClientsPage());
                }
            }
        }

        private void ButtonCancel_Click(object s, RoutedEventArgs a)
        {
            if (MessageBox.Show("Cancel? The current progress will not be saved", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                Adapter.CurrentId = null;
                Adapter.CurrentCartId = null;
                Adapter.TemporaryPaycheck = new Paycheck();
                this.NavigationService.Navigate(new MainMenu());
            }

        }
    }
}
