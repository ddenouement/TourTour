namespace TourTour.Utilities
{
    static class CurrentUser
    {
        private static User _currentUser;
        

        static CurrentUser()
        {
            Logout();
        }

        public static bool TryLogin(string login, string password)
        {
            User u = DBContextManager.TryLogin(login, password);

            if (u != null)
            {
                _currentUser = u;
                return true;
            }
            else return false;
        }

        public static bool TrySignup(string login, string email, string password)
        {
            if (DBContextManager.CreateUser(login, email, password))
            {
                TryLogin(login, password);
                return true;
            }
            else return false;
        }

        public static void Logout()
        {
            _currentUser = null;
        }

        public static bool IsAdmin()
        {
            return _currentUser.is_admin;
        }

        public static bool IsLoggedIn()
        {
            return (_currentUser!=null);
        }
    }
}
