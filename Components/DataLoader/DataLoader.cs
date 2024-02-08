using LumberLogistics.Components.QueryManager;
using LumberLogistics.Pages;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace LumberLogistics.Pages
{
    public class DataLoader : QueryManager
    {
        private QueryManager queryManager;

        public DataLoader(QueryManager queryManager)
        {
            this.queryManager = queryManager;
        }

        public void LoadData(User user, DataGrid dataGrid, string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                MessageBox.Show("Błąd: Zapytanie jest puste.");
                return;
            }

            using (SqlConnection connection = new SqlConnection(user.GetConnString()))
            {
                try
                {
                    connection.Open();
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
    }
}
