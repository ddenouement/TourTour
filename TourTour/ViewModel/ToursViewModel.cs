using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using TourTour.Utilities;

namespace TourTour.ViewModel
{
    class ToursViewModel
    {
        DBContext db = new DBContext();

        public DbSet<Tour> _tours { get; set; }
        public DbSet<Hotel> _hotels { get; set; }
        public DbSet<City> _cities { get; set; }
        public DbSet<Country> _countries { get; set; }

        public ObservableCollection<object> items;

        public ToursViewModel()
        {
            try
            {
                db.Tours.Load();
                db.Hotels.Load();
                db.Cities.Load();

                _tours = db.Tours;
                _hotels = db.Hotels;
                _cities = db.Cities;
                _countries = db.Countries;

                var query = _tours.Join(_hotels,
                 t => t.hotel_id,
                 h => h.hotel_id,
                 (t, h) => new { t, h }).Join(_cities,
                 th => th.h.city_id,
                 c => c.city_id,
                 (th, c) => new { th, c }).Join(_countries,
                 thc => thc.c.country_id,
                 co => co.country_id,
                 (thc, co) => new
                 {
                     ID = thc.th.t.tour_id,
                     Name = thc.th.t.tour_name,
                     Mileage = thc.th.t.mileage,
                     Transfer = thc.th.t.transfer,
                     AviaCost = thc.th.t.avia_cost,
                     Available = thc.th.t.availability,
                     Description = thc.th.t.description,
                     Country = co.country_name,
                     City = thc.c.city_name
                 });

                items = new ObservableCollection<object>(query);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
