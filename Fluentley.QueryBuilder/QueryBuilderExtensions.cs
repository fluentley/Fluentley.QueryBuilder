using System;
using System.Collections.Generic;
using System.Linq;
using Fluentley.QueryBuilder.Options;

namespace Fluentley.QueryBuilder
{
    public static class QueryBuilderExtensions
    {
        public static T[] QueryOn<T>(this T[] queryable, Action<IQueryOptions<T>> queryAction)
        {
            var queryOption = new QueryOptions<T>(queryable.AsQueryable());
            queryAction?.Invoke(queryOption);
            return queryOption.Query.AsQueryable().ToArray();
        }

        public static IEnumerable<T> QueryOn<T>(this IEnumerable<T> queryable, Action<IQueryOptions<T>> queryAction)
        {
            var queryOption = new QueryOptions<T>(queryable.AsQueryable());
            queryAction?.Invoke(queryOption);
            return queryOption.Query;
        }


        public static IEnumerable<T> QueryOn<T>(this Func<IEnumerable<T>> function,
            Action<IQueryOptions<T>> queryAction)
        {
            var queryOption = new QueryOptions<T>(function().AsQueryable());
            queryAction?.Invoke(queryOption);
            return queryOption.Query;
        }

        public static IEnumerable<T> QueryOn<T>(this Func<T[]> function, Action<IQueryOptions<T>> queryAction)
        {
            var queryOption = new QueryOptions<T>(function().AsQueryable());
            queryAction?.Invoke(queryOption);
            return queryOption.Query;
        }
    }
}