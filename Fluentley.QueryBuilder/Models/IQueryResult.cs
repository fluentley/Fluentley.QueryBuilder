namespace Fluentley.QueryBuilder.Models
{
    public interface IQueryResult<out T>
    {
        QueryPaging Paging { get; }
        T Data { get; }
    }
}