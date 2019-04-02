using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TourTour.Utilities;

namespace TourTour.View
{
    /// <summary>
    /// Логика взаимодействия для ToursPage.xaml
    /// </summary>
    public partial class ToursPage : Page
    //колонки country i city берем с таблицы Отель соответсвенной для этого Тура(дублируем просто чтобы клиент видел)
    {
        static int PK_ID;

        public ToursPage()
        {
            InitializeComponent();
            FillGrid();
            if (!Adapter.AdminMode())
            {
                gridAddTour.Visibility = Visibility.Collapsed;
                ButtonNewTour.Visibility = Visibility.Hidden;
                //datagridTours.Columns[10].Visibility = Visibility.Collapsed;
                //datagridTours.Columns[11].Visibility = Visibility.Collapsed;
            }
            else
            {
                //datagridTours.Columns[12].Visibility = Visibility.Collapsed;//админ не может добавить тур
            }
        }
        private void btn_ShowHideDetails_hotel(object sender, RoutedEventArgs e)
        {
            for (var vis = sender as Visual; vis != null; vis = VisualTreeHelper.GetParent(vis) as Visual)
                if (vis is DataGridRow)
                {
                    var row = (DataGridRow)vis;
                    row.DetailsVisibility =
                    row.DetailsVisibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                    break;
                }
        }
        private void FillGrid()
        {
            //var db = DBContextManager.GetDBContext();
            //var tours = db.Tours.Join(db.Hotels,
            // t => t.hotel_id,
            // h => h.hotel_id,
            // (t, h) => new { t, h }).Join(db.Cities,
            // th => th.h.city_id,
            // c => c.city_id,
            // (th, c) => new { th, c }).Join(db.Countries,
            // thc => thc.c.country_id,
            // co => co.country_id,
            // (thc, co) => new {
            //     ID = thc.th.t.tour_id,
            //     Name = thc.th.t.tour_name,
            //     Mileage = thc.th.t.mileage,
            //     Transfer = thc.th.t.transfer,
            //     AviaCost = thc.th.t.avia_cost,
            //     Available = thc.th.t.availability,
            //     Description = thc.th.t.description,
            //     Country = co.country_name,
            //     City = thc.c.city_name
            // });

            var tours = from tour in DBContextManager.GetDBContext().Tours
                        select tour;

            tours.Load();
            DataGridTours.ItemsSource = DBContextManager.GetDBContext().Tours.Local;




        }
        private void ClearForm()
        {

        }
        private void btn_clearform_click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }
        private void btn_submit_new_tour_click(object sender, RoutedEventArgs e)
        {//update або add (update якщо нажали до цього на кнопку edit. Add - по замовчуванню

           
        }
        private void btn_edit(object sender, RoutedEventArgs e)
        {

            
        }
         
        private void btn_delete(object sender, RoutedEventArgs e)
        {
            
        }
        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new MainMenu());

        }
        private void ButtonNewTour_Click(object sender, RoutedEventArgs e)
        {
            bool a = gridAddTour.Visibility.Equals(Visibility.Collapsed);
            if (a)
            {
                //ButtonCreateHotel.Content = "hide";
                gridAddTour.Visibility = Visibility.Visible;
                //datagridTours.Margin = new Thickness(0, 284, 0, -0.4);//down
            }
            else
            {
                //ButtonCreateHotel.Content = "add";
                gridAddTour.Visibility = Visibility.Collapsed;
                //datagridTours.Margin = new Thickness(0, 66, 0, -0.4);//up
            }
            //надо будет новую кнопку создать и спрятать те что слева сверху
        }
        private void ButtonCreateHotel_Click(object sender, RoutedEventArgs a)
        {

            this.NavigationService.Navigate(new  AddHotelPage());
        }
        private void ButtonSubmit_Click(object sender, RoutedEventArgs a)
        {

        }
        private void ButtonAddToCart_Click(object sender, RoutedEventArgs a)
        {

        }

    }
}
