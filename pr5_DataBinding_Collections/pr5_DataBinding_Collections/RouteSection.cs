using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pr5_DataBinding_Collections
{
    public class RouteSection : INotifyPropertyChanged
    {
        private string _startPoint = "";
        private string _endPoint = "";
        private double _distanceKm;

        public string StartPoint
        {
            get { return _startPoint; }
            set
            {
                _startPoint = value;
                OnPropertyChanged();
                
            }
        }

        public string EndPoint
        {
            get { return _endPoint; }
            set
            {
                _endPoint = value;
                OnPropertyChanged();
            }
        }

        public double DistanceKm
        {
            get { return _distanceKm; }
            set
            {
                _distanceKm = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
    }
}
