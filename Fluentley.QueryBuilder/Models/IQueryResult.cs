namespace Fluentley.QueryBuilder.Models
{
    public interface IQueryResult<T>
    {
        QueryPaging Paging { get; }
        T Data { get; }
    }
}