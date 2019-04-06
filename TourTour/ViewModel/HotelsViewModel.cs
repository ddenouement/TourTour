using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using TourTour.Utilities;

namespace TourTour.ViewModel
{
    class HotelsViewModel : INotifyPropertyChanged
    {
        DBContext db = new DBContext();

        private List<Hotel> _hotels;

        

        public List<Hotel> Hotels
        {
            get
            {
                return _hotels;
            }
            set
            {
                _hotels = value;
            }
        }

        public HotelsViewModel()
        {
            try
            {
                var query = from hotel in db.Hotels
                            select hotel;
                
                Hotels = query.ToList();
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
