namespace LumberLogistics.Pages
{
    public class Query
    {
        private string queryText;

        public string GetQuery()
        {
            return queryText;
        }

        public void SetQuery(string query)
        {
            queryText = query;
        }
    }
}
