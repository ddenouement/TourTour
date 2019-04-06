using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourTour
{
    public class Hotel
    {
        public Hotel()
        {
            this.Hotel_service = new HashSet<Hotel_service>();
        }
        [Key]
        public int hotel_id { get; set; }
        public string hotel_name { get; set; }
        public int stars { get; set; }
        public decimal hotel_price { get; set; }

        public int city_id { get; set; }
        [ForeignKey("city_id")]
        public virtual City City { get; set; }
        public virtual ICollection<Hotel_service> Hotel_service { get; set; }
    }
    public class City
    {
        [Key]
        public int city_id { get; set; }
        public string city_name { get; set; }
        public int country_id { get; set; }
        [ForeignKey("country_id")]
        public virtual Country Country { get; set; }
        // public virtual Hotel Hotel { get; set; }
    }
    public class Client
    {
        public Client()
        {
            this.Phone_number = new HashSet<Phone_number>();
            this.Paychecks = new HashSet<Paycheck>();
        }
        [Key]
        public int client_id { get; set; }
        public string client_name { get; set; }
        public string client_surname { get; set; }
        public string client_patronym { get; set; }
        public System.DateTime birth_date { get; set; }
        public string country { get; set; }
        public string region { get; set; }
        public string city { get; set; }
        public string area { get; set; }
        public string street { get; set; }
        public int house { get; set; }
        public string block { get; set; }
        public int apt { get; set; }
        public string email { get; set; }


        public virtual ICollection<Paycheck> Paychecks { get; set; }
         
        public virtual ICollection<Phone_number> Phone_number { get; set; }
    }
    public partial class Country
    {
        public Country()
        {
            this.City = new HashSet<City>();
        }
        [Key]
        public int country_id { get; set; }
        public string country_name { get; set; }
        public Nullable<decimal> visa_cost { get; set; }

        public virtual ICollection<City> City { get; set; }
    }
    public partial class Hotel_service
    {
        [Key]
        public int Hotel_service_id { get; set; }

        public int hotel_id { get; set; }

        public int service_id { get; set; }
        public decimal service_price { get; set; }
        [ForeignKey("hotel_id")]
        public virtual Hotel Hotel { get; set; }
        [ForeignKey("service_id")]
        public virtual Service Service { get; set; }
    }
    public partial class Paycheck
    {
        [Key]
        public int check_id { get; set; }
        [ForeignKey("client_id")]
        public virtual Client Client { get; set; }
        public int client_id { get; set; }
        public int ppl_count { get; set; }
        public bool payed { get; set; }
        public Nullable<System.DateTime> pay_date { get; set; }
        [ForeignKey("voucher_id")]
        public virtual Voucher Voucher { get; set; }

        public int voucher_id { get; set; }
    }
    public partial class Phone_number
    {

        [ForeignKey("client_id")]
        public virtual Client Client { get; set; }

        public int client_id { get; set; }
        [Key]
        public string phone_num { get; set; }
    }
    public partial class Service
    {
        public Service()
        {
            this.Hotel_service = new HashSet<Hotel_service>();
        }
        [Key]
        public int service_id { get; set; }
        public string service_name { get; set; }

        public virtual ICollection<Hotel_service> Hotel_service { get; set; }
    }

    public partial class User
    {
        [Key]
        public int id { get; set; }
        public string login { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool is_admin { get; set; }


        //   public int ? client_id { get; set; }
        //    [ForeignKey("client_id")]
        //   public virtual Client   client { get; set; }
    }
    public partial class Voucher
    {
        [Key]
        public int voucher_id { get; set; }
        public System.DateTime start_date { get; set; }
        public System.DateTime end_date { get; set; }
        public decimal insurance_cost { get; set; }
        public string feedback { get; set; }
        [ForeignKey("tour_id")]


        public virtual Tour Tour { get; set; }
        public int tour_id { get; set; }
    }

    public partial class Tour
    {
        public Tour()
        {
            this.Voucher = new HashSet<Voucher>();
        }
        [Key]
        public int tour_id { get; set; }
        public string tour_name { get; set; }
        public int mileage { get; set; }
        public decimal transfer { get; set; }
        public bool availability { get; set; }
        public string description { get; set; }
        public decimal avia_cost { get; set; }

        public int hotel_id { get; set; }
        [ForeignKey("hotel_id")]
        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<Voucher> Voucher { get; set; }
    }

}
