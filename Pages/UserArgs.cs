namespace LumberLogistics.Pages
{
    public class User
    {
        public int Id { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
        public string Stanowisko { get; set; }
        public string ConnectionString { get; }
        public User(int id, string imie, string nazwisko, string stanowisko, string connectionString)
        {
            Id = id;
            Imie = imie;
            Nazwisko = nazwisko;
            Stanowisko = stanowisko;
            ConnectionString = "Data Source=localhost\\MSSQLSERVER1;Initial Catalog=Tartak;Integrated Security=True";
        }
        public string GetConnString()
        {
            return ConnectionString;
        }
    }
}
