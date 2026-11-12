using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using System.IO;

namespace pr5_DataBinding_Collections
{
    /// <summary>
    /// Логика взаимодействия для RoutePage.xaml
    /// </summary>
    public partial class RoutePage : Page
    {
        static string FilePath = "routes.json";

        public ObservableCollection<Route> Routes { get; set; }
        public Route? SelectedRoute { get; set; }

        public RoutePage()
        {
            Routes = Load();
            InitializeComponent();
            DataContext = this;
        }

        static ObservableCollection<Route> Load()
        {
            if (!File.Exists(FilePath))
                return new ObservableCollection<Route>();

            try
            {
                var list = JsonSerializer.Deserialize<List<Route>>(File.ReadAllText(FilePath));
                return list != null ? new ObservableCollection<Route>(list) : new ObservableCollection<Route>();
            }
            catch
            {
                return new ObservableCollection<Route>();
            }
        }

        public static void Save(ObservableCollection<Route> list)
        {
            File.WriteAllText(FilePath, JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true }));
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RouteFormPage(Routes, null));
        }

        private void EditItem_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedRoute == null)
            {
                MessageBox.Show("Пользователь не выбран");
                return;
            }
            NavigationService.Navigate(new RouteFormPage(Routes, SelectedRoute));
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedRoute == null)
            {
                MessageBox.Show("Пользователь не выбран");
                return;
            }
            bool confirm = MessageBox.Show("Вы действительно хотите удалить запись?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes;
            if (confirm)
            {
                Routes.Remove(SelectedRoute);
                Save(Routes);
            }

        }

    }
}
