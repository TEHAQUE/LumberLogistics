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
using LumberLogistics.Components.QueryManager;

namespace LumberLogistics.Pages
{
    public partial class Magazyn : Page
    {
        private User user;
        private QueryManager queryManager;
        private DataLoader dataLoader;
        public Magazyn(User user)
        {
            InitializeComponent();
            this.user = user;
            this.queryManager = new QueryManager();
            this.dataLoader = new DataLoader(queryManager);
            UstawDaneUzytkownika();
            dataLoader.LoadData(user, statMag, queryManager.Query4.GetQuery());
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
