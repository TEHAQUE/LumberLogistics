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
    public partial class Landing : Page
    {
        private QueryManager queryManager;
        private User user;
        private DataLoader dataLoader;
        public Landing(User user)
        {
            InitializeComponent();
            this.queryManager = new QueryManager();
            this.user = user;
            this.dataLoader = new DataLoader(queryManager);
            dataLoader.LoadData(user, stat1, queryManager.Query1.GetQuery());
            dataLoader.LoadData(user, stat2, queryManager.Query2.GetQuery());
            dataLoader.LoadData(user, stat3, queryManager.Query3.GetQuery());
            UstawDaneUzytkownika();
            LoadData4();
        }

        private void UstawDaneUzytkownika()
        {
            StanowiskoTextBox.Text = $"Stanowisko: {user.Stanowisko}, id: {user.Id}";
            ImieNazwiskoTextBox.Text = $"{user.Imie} {user.Nazwisko}";
        }
        private void LoadData4()
        {
            using (SqlConnection connection = new SqlConnection(user.ConnectionString))
            {
                try
                {
                    connection.Open();

                    string query = $"select * from Zamowienia where PracownikID = {user.Id}";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);

                    DataTable dataTable = new DataTable();

                    adapter.Fill(dataTable);

                    stat4.ItemsSource = dataTable.DefaultView;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd: {ex.Message}");
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                        connection.Close();
                }
            }
        }

        private void LoadMain(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new MainSpace(user));
        }
        private void Magazyn(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Magazyn(user));
        }
        private void Informacje(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Informacje(user));
        }

        private void Wyloguj(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Login());
        }
    }
}
