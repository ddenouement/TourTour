﻿using System;
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

        //public DbSet<Tour> _tours { get; set; }
        //public DbSet<Hotel> _hotels { get; set; }
        //public DbSet<City> _cities { get; set; }
        //public DbSet<Country> _countries { get; set; }

        public ObservableCollection<object> items;

        public ToursViewModel()
        {
            try
            {
                db.Tours.Load();
                db.Hotels.Load();
                db.Cities.Load();
                db.Countries.Load();
                db.Services.Load();
                db.Hotel_services.Load();

                var query = from tour in db.Tours
                            join hotel in db.Hotels on tour.hotel_id equals hotel.hotel_id
                            join city in db.Cities on hotel.city_id equals city.city_id
                            join country in db.Countries on city.country_id equals country.country_id
                            select new
                            {
                                ID = tour.tour_id,
                                Name = tour.tour_name,
                                Mileage = tour.mileage,
                                Transfer = tour.transfer,
                                AviaCost = tour.avia_cost,
                                Available = tour.availability,
                                Description = tour.description,
                                Country = country.country_name,
                                City = city.city_name,
                                Hotelid = hotel.hotel_id,
                                hotel_info = "Name: " + hotel.hotel_name +
                                               "\nPrice: " + hotel.hotel_price +
                                               "\nStars: " + hotel.stars +
                                               "\nAdditional services:"
                            };

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

            res += "Name: " + hotel.hotel_name +
                   "\nPrice: " + hotel.hotel_price +
                   "\nStars: " + hotel.stars +
                   "\nAdditional services:";
            foreach (Hotel_service hs in hotel.Hotel_service)
            {
                res += "\n" + hs.Service.service_name + " - " + hs.service_price;
            }

            return res;
        }
    }
}
