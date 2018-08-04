using System;
using System.Linq;
using System.Linq.Expressions;

namespace Fluentley.QueryBuilder.Options
{
    public interface IQueryOptions<T>
    {
        IQueryable<T> Query { get; }
        IQueryable<T> QueryWithNoPaging { get; }
        bool IsPaged { get; }
        int PageIndex { get;  }
        int PageSize { get; }
        IQueryOptions<T> DynamicSort(string propertyName, string direction = "asc");
        IQueryOptions<T> DynamicWhere(string filter);
        IQueryOptions<T> Paging(int pageIndex, int pageSize);
        IQueryOptions<T> DynamicContains(string propertyName, string value);
        IQueryOptions<T> QueryBy(IQueryable<T> query);
        IQueryOptions<T> QueryBy(Func<IQueryable<T>, IQueryable<T>> query);
        IQueryOptions<T> EagerLoad(params Expression<Func<T, object>>[] eagerLoads);
    }
}