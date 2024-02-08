
using System;
using System.Data.SqlClient;
using System.IO.Packaging;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LumberLogistics.Pages
{
    public partial class Login : Page
    {

        private int id;
        private string imie;
        private string nazwisko;
        private string stanowisko;
        private string connectionString;
        public Login()
        {
            InitializeComponent();
        }

        private void ZalogujButtonClick(object sender, RoutedEventArgs e)
        {
            string login = LoginTextBox.Text;
            string haslo = HasloBox.Password;

            if (SprawdzDaneLogowania(login, haslo))
            {
                MessageBox.Show("Logowanie udane!");
                PrzejdzNaNastepnaStrone();
            }
            else
            {
                MessageBox.Show("Logowanie nieudane.");
            }
        }

        private bool SprawdzDaneLogowania(string login, string haslo)
        {
            string connectionString = "Data Source=localhost\\MSSQLSERVER1;Initial Catalog=Tartak;Integrated Security=True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT PracownikID, Imie, Nazwisko, Specjalnosci.NazwaSpecjalnosci AS Stan FROM Pracownicy INNER JOIN Specjalnosci ON Pracownicy.SpecjalnoscID = Specjalnosci.SpecjalnoscID WHERE Login = @Login AND Haslo = @Haslo";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    command.Parameters.AddWithValue("@Haslo", haslo);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            id = reader.GetInt32(reader.GetOrdinal("PracownikID"));
                            imie = reader.GetString(reader.GetOrdinal("Imie"));
                            nazwisko = reader.GetString(reader.GetOrdinal("Nazwisko"));
                            stanowisko = reader.GetString(reader.GetOrdinal("Stan"));
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        private void PrzejdzNaNastepnaStrone()
        {
            User user = new User(id, imie, nazwisko, stanowisko, connectionString);
            NavigationService?.Navigate(new Landing(user));
        }
    }
}
