using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pr5_DataBinding_Collections
{
    public class Route : INotifyPropertyChanged
    {
        private int _id = 0;
        private string _name = "";
        private string _startPoint = "";
        private string _endPoint = "";

        private ObservableCollection<RouteSection> _sections = new();

        public int Id
        {
            get { return _id; }
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string StartPoint
        {
            get { return _startPoint; }
            set
            {
                if (_startPoint != value)
                {
                    _startPoint = value;
                    OnPropertyChanged();
                }
            }
        }

        public string EndPoint
        {
            get { return _endPoint; }
            set
            {
                if (_endPoint != value)
                {
                    _endPoint = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<RouteSection> Sections
        {
            get { return _sections; }
            set 
            {
                if (_sections == value) return;

                if (_sections != null)
                    _sections.CollectionChanged -= OnSectionsChanged;

                _sections = value;

                if (_sections != null)
                    _sections.CollectionChanged += OnSectionsChanged;

                OnPropertyChanged();
                OnPropertyChanged(nameof(TotalDistanceKm));
            }
        }
        public double TotalDistanceKm
        {
            get
            {
                double total = 0;
                foreach (var s in Sections)
                    total += s.DistanceKm;
                return total;
            }
        }
        
        public Route()
        {
            _sections.CollectionChanged += OnSectionsChanged;
        }

        private void OnSectionsChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(TotalDistanceKm));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string? propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}
