using System.Collections.Generic;

namespace TourTour.Utilities
{
    static class Adapter
    {
        static public int? CurrentId=null;
        static public int? CurrentCartId = null;
        static public List<int> Cart=new List<int>();
        static public Paycheck TemporaryPaycheck;

        static public bool Login(string login, string password)
        {
            Cart = new List<int>();
            return CurrentUser.TryLogin(login, password);
        } 

        static public void Logout()
        {
            Cart = new List<int>();
            CurrentUser.Logout();
        }

        static public bool Signup(string login, string email, string password)
        {
            return CurrentUser.TrySignup(login, email, password);
        }

        static public bool AdminMode()
        {
            return CurrentUser.IsAdmin();
        }

        static public bool LoggedIn()
        {
            return CurrentUser.IsLoggedIn();
        }

    }
}
