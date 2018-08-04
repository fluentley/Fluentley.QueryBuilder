using System;
using System.Linq;
using System.Linq.Expressions;

namespace Fluentley.QueryBuilder.Options
{
    public interface IQueryOption<T>
    {
        IQueryable<T> Query { get; }
        IQueryable<T> QueryWithNoPaging { get; }
        IQueryOption<T> DynamicSort(string propertyName, string direction = "asc");
        IQueryOption<T> DynamicWhere(string filter);
        IQueryOption<T> Paging(int pageIndex, int pageSize);
        IQueryOption<T> DynamicContains(string propertyName, string value);
        IQueryOption<T> QueryBy(IQueryable<T> query);
        IQueryOption<T> QueryBy(Func<IQueryable<T>, IQueryable<T>> query);
        IQueryOption<T> EagerLoad(params Expression<Func<T, object>>[] eagerLoads);
    }
}