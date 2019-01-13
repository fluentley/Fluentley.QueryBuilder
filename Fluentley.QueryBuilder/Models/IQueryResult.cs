using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fluentley.QueryBuilder.Models
{
    public interface IQueryResult<T>
    {
        QueryPaging Paging { get; }
        IQueryable<T> Data { get; }
        List<Expression<Func<T, object>>> EagerLoads { get; set; }
    }

    public interface IQueryResult<T, TSelect>
    {
        QueryPaging Paging { get; }
        IQueryable<TSelect> Data { get; }
        List<Expression<Func<T, object>>> EagerLoads { get; set; }
    }
}