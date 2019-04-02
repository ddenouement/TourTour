using System.Data.Entity;

namespace TourTour.Utilities
{
    class DBContext : DbContext
    {
        public DBContext() : base("tourtourEntities"){
            Database.SetInitializer<DBContext>(new CreateDatabaseIfNotExists< DBContext>());

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Tour> Tours { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }
        public DbSet<Paycheck> Paychecks { get; set; }

    }
}
