using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace pr5_DataBinding_Collections
{
    /// <summary>
    /// Логика взаимодействия для RouteFormPage.xaml
    /// </summary>
    public partial class RouteFormPage : Page
    {
        private ObservableCollection<Route> _routes;
        private Route _route;
        private Route? _original;

        public RouteFormPage(ObservableCollection<Route> routes, Route? route = null)
        {
            _routes = routes;
            _original = route;

            _route = route == null ? new Route() : new Route
            {
                Id = route.Id,
                Name = route.Name,
                StartPoint = route.StartPoint,
                EndPoint = route.EndPoint
            };

            if (route != null)
            {
                foreach (var s in route.Sections)
                {
                    _route.Sections.Add(new RouteSection
                    {
                        StartPoint = s.StartPoint,
                        EndPoint = s.EndPoint,
                        DistanceKm = s.DistanceKm
                    });
                }
            }

            InitializeComponent();
            DataContext = _route;
        }
        private void AddSection_Click(object sender, RoutedEventArgs e)
        {
            if (!double.TryParse(SectionKm.Text, out double km))
            {
                MessageBox.Show("Введите корректное расстояние");
                return;
            }

            _route.Sections.Add(new RouteSection
            {
                StartPoint = SectionFrom.Text,
                EndPoint = SectionTo.Text,
                DistanceKm = km
            });

            SectionFrom.Text = "";
            SectionTo.Text = "";
            SectionKm.Text = "";
        }
        private void DeleteSection_Click(object sender, RoutedEventArgs e)
        {
            if (SectionsList.SelectedItem is RouteSection s)
                _route.Sections.Remove(s);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (_original == null)
            {
                _routes.Add(_route);
                RoutePage.Save(_routes);
                BackButton_Click(sender, e);
                return;
            }

            _original.Id = _route.Id;
            _original.Name = _route.Name;
            _original.StartPoint = _route.StartPoint;
            _original.EndPoint = _route.EndPoint;

            _original.Sections.Clear();
            foreach (var s in _route.Sections)
            {
                _original.Sections.Add(new RouteSection
                {
                    StartPoint = s.StartPoint,
                    EndPoint = s.EndPoint,
                    DistanceKm = s.DistanceKm
                });
            }

            RoutePage.Save(_routes);
            BackButton_Click(sender, e);
        }
    }
}
