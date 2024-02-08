using LumberLogistics.Pages;

namespace LumberLogistics.Components.QueryManager
{
    public class QueryManager : Query
    {
        public Query Query1 { get; }
        public Query Query2 { get; }
        public Query Query3 { get; }
        public Query Query4 { get; }
        public Query Query5 { get; }
        public Query Query6 { get; }
        public Query Query7 { get; }
        public Query Query8 { get; }
        public Query Query9 { get; }

        public QueryManager()
        {
            Query1 = new Query();
            Query1.SetQuery("SELECT G.NazwaGatunku AS Gatunek, CAST(ROUND(COUNT(P.PartiaID) * 100.0 / (SELECT COUNT(PartiaID) FROM PartiaDrewna), 2) AS DECIMAL(5,2)) AS Procent FROM Gatunek G JOIN PartiaDrewna P ON G.GatunekID = P.GatunekID GROUP BY G.NazwaGatunku;");
            Query2 = new Query();
            Query2.SetQuery("SELECT PM.Nazwa AS ProcesMagazynowy, " +
                       $"CAST(ROUND(COUNT(M.ProcesMagazynowaniaID) * 100.0 / (SELECT COUNT(ProcesMagazynowaniaID) FROM Magazyn), 2) AS DECIMAL(5,2)) AS ProcentLiczbaPartii " +
                       $"FROM ProcesyMagazynowe PM JOIN Magazyn M ON PM.ProcMagID = M.ProcesMagazynowaniaID " +
                       $"GROUP BY PM.Nazwa;");
            Query3 = new Query();
            Query3.SetQuery("SELECT RO.Nazwa AS RodzajObrobki, " +
                            $"CAST(ROUND(COUNT(O.ObrobkaID) * 100.0 / (SELECT COUNT(ObrobkaID) FROM Obrabianie), 2) AS DECIMAL(5,2)) AS ProcentLiczbaPartii " +
                            $"FROM RodzajObrobki RO JOIN Obrabianie O ON RO.RodzajObrobkiID = O.RodzajObrobkiID " +
                            $"GROUP BY RO.Nazwa;");
            Query4 = new Query();
            Query4.SetQuery("select MagazynID, PartiaDrewnaID, ProcesyMagazynowe.Nazwa AS NazwaProcesuMagazynowego, Otrzymane,Wydane from magazyn inner join ProcesyMagazynowe on Magazyn.ProcesMagazynowaniaID=ProcesyMagazynowe.ProcMagID");
            Query5 = new Query();
            Query5.SetQuery("SELECT NazwaGatunku, Opis FROM Gatunek");
            Query6 = new Query();
            Query6.SetQuery("SELECT Nazwa AS NazwaObróbki, Opis FROM RodzajObrobki");
            Query7 = new Query();
            Query7.SetQuery("SELECT NazwaWycinki, L.Nazwa AS Lokalizacja, Obszar FROM Wycinka W JOIN Lokalizacja L ON W.LokalizacjaID = L.LokalizacjaID");
            Query8 = new Query();
            Query8.SetQuery("SELECT Nazwa AS NazwaStanuWycinki, Opis FROM StanWycinki");
            Query9 = new Query();
            Query9.SetQuery("SELECT Nazwa AS NazwaProcesuMagazynu, Opis FROM ProcesyMagazynowe");
        }
    }
}
