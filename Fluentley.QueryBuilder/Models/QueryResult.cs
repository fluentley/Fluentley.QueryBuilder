namespace Fluentley.QueryBuilder.Models
{
    internal class QueryResult<T> : IQueryResult<T>
    {
        public QueryPaging Paging { get; internal set; }
        public T Data { get; internal set; }
    }
}