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
    class ToursViewModel : INotifyPropertyChanged
    {
        DBContext db = new DBContext();

        private List<Tour> _tours;
        public List<Tour> Tours
        {
            get
            {
                return _tours;
            }
            set
            {
                _tours = value;
                NotifyPropertyChanged();
            }
        }

        public ToursViewModel()
        {
            FillTours();
        }

        private void FillTours()
        {
            try
            {
                var query = from tours in db.Tours
                            select tours;

                Tours = query.ToList();
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
