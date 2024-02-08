using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace LumberLogistics.Pages
{
    public partial class MainSpace : Page
    {
        private User user;
        public MainSpace(User user)
        {
            InitializeComponent();
            this.user = user;
            UstawDaneUzytkownika();
            LoadData();

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
        private void LoadData()
        {

            using (SqlConnection connection = new SqlConnection(user.GetConnString()))
            {
                try
                {
                    connection.Open();

                    string query = $"SELECT PD.PartiaID,PD.MagazynID,P.Nazwa AS ProcesMagazynu, G.NazwaGatunku, W.NazwaWycinki, R.Nazwa AS ProcesObróbki, PD.Waga, PD.Klasa, PD.Stan, PD.Opis FROM PartiaDrewna PD JOIN Gatunek G ON PD.GatunekID = G.GatunekID JOIN Wycinka W ON PD.WycinkaID = W.WycinkaID JOIN Obrabianie O ON PD.ObrobkaID = O.ObrobkaID JOIN RodzajObrobki R ON O.RodzajObrobkiID = R.RodzajObrobkiID JOIN Magazyn M ON PD.MagazynID = M.PartiaDrewnaID JOIN ProcesyMagazynowe P ON M.ProcesMagazynowaniaID = P.ProcMagID WHERE PracownikID = {user.Id}";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    dataGrid.ItemsSource = dataTable.DefaultView;
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
        private void AlterBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(AlterId.Text, out int alterId) && int.TryParse(AlteredId.Text, out int alteredId))
                {
                    string updateQuery = "UPDATE PartiaDrewna SET MagazynID = @AlteredId WHERE PartiaID = @AlterId";

                    using (SqlConnection connection = new SqlConnection(user.GetConnString()))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@AlterId", alterId);
                            command.Parameters.AddWithValue("@AlteredId", alteredId);

                            int affectedRows = command.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Aktualizacja zakończona sukcesem.");
                            }
                            else
                            {
                                MessageBox.Show("Nie znaleziono pasującego rekordu do aktualizacji.");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Wprowadź poprawne liczby całkowite w TextBox'ach.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }
        private void DropBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(DropedId.Text, out int dropedId))
                {
                    string updateQuery = "DELETE FROM PartiaDrewna WHERE PartiaID = @dropedId";

                    using (SqlConnection connection = new SqlConnection(user.GetConnString()))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@dropedId", dropedId);

                            int affectedRows = command.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Aktualizacja zakończona sukcesem.");
                            }
                            else
                            {
                                MessageBox.Show("Nie znaleziono pasującego rekordu do aktualizacji.");
                            }
                        }
                        connection.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Wprowadź poprawne liczby całkowite w TextBox'ach.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }
        private void InsertBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                if (int.TryParse(PartiaId.Text, out int partiaId) && int.TryParse(GatunekId.Text, out int gatunekId)
                    && int.TryParse(MagazynId.Text, out int magazynId) && int.TryParse(WycinkaId.Text, out int wycinkaId)
                    && int.TryParse(ObrobkId.Text, out int obrobkiId)
                    )
                {
                    string updateQuery = "INSERT INTO PartiaDrewna (PartiaID, GatunekID, MagazynID, WycinkaID, ObrobkaID, Waga, Klasa, Stan, Opis, PracownikID) VALUES (@PartiaId, @GatunekId, @MagazynId, @WycinkaId, @ObrobkId, @Waga, @Klasa, @Stan, @Opis, @PracownikId)";

                    using (SqlConnection connection = new SqlConnection(user.GetConnString()))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand(updateQuery, connection))
                        {
                            command.Parameters.AddWithValue("@PartiaId", partiaId);
                            command.Parameters.AddWithValue("@GatunekId", gatunekId);
                            command.Parameters.AddWithValue("@MagazynId", magazynId);
                            command.Parameters.AddWithValue("@WycinkaId", wycinkaId);
                            command.Parameters.AddWithValue("@ObrobkId", obrobkiId);
                            command.Parameters.AddWithValue("@Waga", Convert.ToSingle(Waga.Text));
                            command.Parameters.AddWithValue("@Klasa", Klasa.Text);
                            command.Parameters.AddWithValue("@Stan", Stan.Text);
                            command.Parameters.AddWithValue("@Opis", Opis.Text);
                            command.Parameters.AddWithValue("@PracownikId", user.Id);

                            int affectedRows = command.ExecuteNonQuery();

                            if (affectedRows > 0)
                            {
                                MessageBox.Show("Aktualizacja zakończona sukcesem.");
                            }
                            else
                            {
                                MessageBox.Show("Nie znaleziono pasującego rekordu do aktualizacji.");
                            }
                            connection.Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Wprowadź poprawne liczby całkowite w TextBox'ach.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd: {ex.Message}");
            }
        }

        private void RefreshBtn(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
        }
    }
}
