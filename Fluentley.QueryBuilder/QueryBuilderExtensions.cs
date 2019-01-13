using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public static IQueryResult<T> QueryOn<T>(this T[] queryable,
            Action<IQueryOption<T>> queryAction)
        {
            return QueryOn(queryable.AsQueryable(), queryAction);
        }

        public static IQueryResult<T> QueryOn<T>(this IQueryable<T> queryable,
            Action<IQueryOption<T>> queryAction)
        {
            var processed = OptionProcessor.Process<IQueryOption<T>, QueryOption<T>, T>(queryAction, queryable);
            return ResultProcessor.Process(processed);
        }

        public static IQueryResult<T, TSelect> QueryOn<T, TSelect>(this IQueryable<T> queryable,
            Action<IQueryOption<T>> queryAction, Expression<Func<T, TSelect>> selector)
        {
            var processed = OptionProcessor.Process<IQueryOption<T>, QueryOption<T>, T>(queryAction, queryable);
            return ResultProcessor.Process(processed, selector);

        }

        public static IQueryResult<T> QueryOn<T>(this IEnumerable<T> queryable,
            Action<IQueryOption<T>> queryAction)
        {
            return QueryOn(queryable.AsQueryable(), queryAction);
        }

        public static IQueryResult<T> QueryOn<T>(this Func<IEnumerable<T>> function,
            Action<IQueryOption<T>> queryAction)
        {
            return function().QueryOn(queryAction);
        }

        public static IQueryResult<T> QueryOn<T>(this Func<T[]> function,
            Action<IQueryOption<T>> queryAction)
        {
            return function().QueryOn(queryAction);
        }

        #endregion
    }
}