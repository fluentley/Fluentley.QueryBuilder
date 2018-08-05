using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Fluentley.QueryBuilder.Models
{
    internal class QueryResult<T> : IQueryResult<T>
    {
        public QueryPaging Paging { get; internal set; }
        public IQueryable<T> Data { get; internal set; }
        public List<Expression<Func<T, object>>> EagerLoads { get; set; }
    }
}