using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TourTour.Utilities;

namespace TourTour.ViewModel
{
    class ClientsViewModel : INotifyPropertyChanged
    {
        DBContext db = new DBContext();

        private List<Client> _clients;



        public List<Client> Clients
        {
            get
            {
                return _clients;
            }
            set
            {
                _clients = value;
            }
        }

        public ClientsViewModel()
        {
            try
            {
                var query = from hotel in db.Clients
                            select hotel;

                Clients = query.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
