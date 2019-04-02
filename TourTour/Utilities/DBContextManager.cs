using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;

namespace TourTour.Utilities
{
    static class DBContextManager
    {
        private static DBContext dbContext;

        static DBContextManager()
        {
            dbContext = new DBContext();
        }

        public static DBContext GetDBContext()
        {
            return dbContext;
        }

        public static bool CreateUser(string mlogin, string memail, string mpassword)
        {
            bool res = true;
            try
            {
                if ((dbContext.Users.FirstOrDefault(x => x.email == memail) != null || dbContext.Users.FirstOrDefault(x => x.login == mlogin) != null)) return false;

                User user1 = new User { login = mlogin, email = memail, password = mpassword };
                dbContext.Users.Add(user1);
                dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                res = false;
                MessageBox.Show(ex.ToString());
            }

            return res;
        }

        public static User TryLogin(string mlogin, string mpassword)
        {
            User u = null;
            try
            {
                u = (dbContext.Users.FirstOrDefault(x => x.login == mlogin && x.password == mpassword));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return u;
        }

        public static DbSet<Tour> GetTours()
        {
            return dbContext.Tours;
        }
    }
}
