namespace Fluentley.QueryBuilder.Models
{
    public class QueryPaging
    {
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public int CurrentPageIndex { get; set; }
        public bool IsPaged { get; set; }
    }
}