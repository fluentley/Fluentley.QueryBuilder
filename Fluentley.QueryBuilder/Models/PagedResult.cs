namespace Fluentley.QueryBuilder.Models
{
    internal class PagedResult<T> : IPagedResult<T>
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public T Data { get; set; }
        public bool IsPaged { get; set; }
    }
}