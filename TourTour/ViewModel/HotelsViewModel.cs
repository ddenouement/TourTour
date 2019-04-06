using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using TourTour.Utilities;

namespace TourTour.ViewModel
{
    class HotelsViewModel
    {
        DBContext db = new DBContext();

        public ObservableCollection<object> items;

        public HotelsViewModel()
        {
            try
            {
                db.Hotels.Load();
                db.Cities.Load();
                db.Countries.Load();
                db.Services.Load();
                db.Hotel_services.Load();

                var query = from hotel in db.Hotels
                            join city in db.Cities on hotel.city_id equals city.city_id
                            join country in db.Countries on city.country_id equals country.country_id
                            select new
                            {
                                ID = hotel.hotel_id,
                                Name = hotel.hotel_name,
                                Stars = hotel.stars,
                                Price = hotel.hotel_price,
                                Country = country.country_name,
                                City = city.city_name,
                                add_services = hotel.Hotel_service
                            };

                query.OrderBy(x => x.ID);
                items = new ObservableCollection<object>(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private string GetHotelInfo(Hotel hotel)
        {
            string res = "";
            foreach (Hotel_service hs in hotel.Hotel_service)
            {
                res += "\n" + hs.Service.service_name + " - " + hs.service_price;
            }

            return res;
        }
    }
}
