using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TourTour.Utilities;

namespace TourTour.ViewModel
{
    class CartViewModel : INotifyPropertyChanged
    {
        private List<Tour> _cart;
        public List<Tour> Cart
        {
            get
            {
                return _cart;
            }
            set
            {
                _cart = value;
            }
        }

        public CartViewModel()
        {
            //Cart = new List<Tour>(Adapter.Cart);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
