using System.Data.Entity;

namespace TourTour.Utilities
{
    static class Adapter
    {
        static public int? CurrentId=null;

         static public bool Login(string login, string password)
        {
            return CurrentUser.TryLogin(login, password);
        } 

        static public void Logout()
        {
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
