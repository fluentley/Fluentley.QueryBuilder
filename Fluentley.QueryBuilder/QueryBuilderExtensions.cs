using System;
using System.Collections.Generic;
using System.Linq;
using Fluentley.QueryBuilder.Models;
using Fluentley.QueryBuilder.Options;
using Fluentley.QueryBuilder.Processors;

namespace Fluentley.QueryBuilder
{
    public static class QueryBuilderExtensions
    {
        private static readonly OptionProcessor OptionProcessor = new OptionProcessor();
        private static readonly ResultProcessor ResultProcessor = new ResultProcessor();

        #region Paged Methods

        public static IQueryResult<IQueryable<T>> QueryPagedOn<T>(this T[] queryable,
            Action<IQueryOption<T>> queryAction)
        {
            return QueryPagedOn(queryable.AsQueryable(), queryAction);
        }

        public static IQueryResult<IQueryable<T>> QueryPagedOn<T>(this IQueryable<T> queryable,
            Action<IQueryOption<T>> queryAction)
        {
            var processed = OptionProcessor.Process<IQueryOption<T>, QueryOption<T>, T>(queryAction, queryable);
            return ResultProcessor.Process(processed);
        }

        public static IQueryResult<IQueryable<T>> QueryPagedOn<T>(this IEnumerable<T> queryable,
            Action<IQueryOption<T>> queryAction)
        {
            return QueryPagedOn(queryable.AsQueryable(), queryAction);
        }


        public static IQueryResult<IQueryable<T>> QueryPagedOn<T>(this Func<IEnumerable<T>> function,
            Action<IQueryOption<T>> queryAction)
        {
            return function().QueryPagedOn(queryAction);
        }

        public static IQueryResult<IQueryable<T>> QueryPagedOn<T>(this Func<T[]> function,
            Action<IQueryOption<T>> queryAction)
        {
            return function().QueryPagedOn(queryAction);
        }

        #endregion
    }
}