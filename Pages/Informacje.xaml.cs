using LumberLogistics.Components.QueryManager;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
using LumberLogistics;

namespace LumberLogistics.Pages
{
    public partial class Informacje : Page
    {
        private User user;
        private QueryManager queryManager;
        private DataLoader dataLoader;
        public Informacje(User user)
        {
            InitializeComponent();
            this.queryManager = new QueryManager();
            this.user = user;
            this.dataLoader = new DataLoader(queryManager);
            UstawDaneUzytkownika();
            dataLoader.LoadData(user, stat1,queryManager.Query5.GetQuery());
            dataLoader.LoadData(user, stat2, queryManager.Query6.GetQuery());
            dataLoader.LoadData(user, stat3, queryManager.Query7.GetQuery());
            dataLoader.LoadData(user, stat4, queryManager.Query8.GetQuery());
            dataLoader.LoadData(user, stat5, queryManager.Query9.GetQuery());
        }

        private void UstawDaneUzytkownika()
        {
            ImieNazwiskoTextBox.Text = $"{user.Imie} {user.Nazwisko}";
            StanowiskoTextBox.Text = $"Stanowisko: {user.Stanowisko}, id: {user.Id}";
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
        private void RefreshBtn(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
        }
    }
}