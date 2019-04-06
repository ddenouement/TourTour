using System;
using System.Linq;
using System.Windows;

namespace TourTour.Utilities
{
    static class DBContextManager
    {

        public static bool CreateUser(string mlogin, string memail, string mpassword)
        {
            bool res = false;
            try
            {
                using (DBContext db = new DBContext())
                {
                    if ((db.Users.FirstOrDefault(x => x.email == memail) != null || db.Users.FirstOrDefault(x => x.login == mlogin) != null)) return false;

                    User user1 = new User { login = mlogin, email = memail, password = mpassword };
                    db.Users.Add(user1);
                    db.SaveChanges();
                    res = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            return res;
        }

        public static User TryLogin(string mlogin, string mpassword)
        {
            User u = null;
            try
            {
                using (DBContext db = new DBContext())
                    u = (db.Users.FirstOrDefault(x => x.login == mlogin && x.password == mpassword));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return u;
        }

        //public static bool CreateTour(Tour tour)
        //{
        //    try
        //    {
        //        using (DBContext db = new DBContext())
        //        {
                    
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.ToString());
        //        return false;
        //    }
        //}
    }
}
