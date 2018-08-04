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

        #region Base Methods

        public static T[] QueryOn<T>(this T[] queryable, Action<IQueryOption<T>> queryAction)
        {
            return QueryOn(queryable.AsQueryable(), queryAction).ToArray();
        }

        public static IQueryable<T> QueryOn<T>(this IQueryable<T> queryable, Action<IQueryOption<T>> queryAction)
        {
            return OptionProcessor.Process<IQueryOption<T>, QueryOption<T>, T>(queryAction, queryable)
                .Query;
        }

        public static IEnumerable<T> QueryOn<T>(this IEnumerable<T> queryable, Action<IQueryOption<T>> queryAction)
        {
            return QueryOn(queryable.AsQueryable(), queryAction);
        }


        public static IEnumerable<T> QueryOn<T>(this Func<IEnumerable<T>> function,
            Action<IQueryOption<T>> queryAction)
        {
            return function().QueryOn(queryAction);
        }

        public static T[] QueryOn<T>(this Func<T[]> function, Action<IQueryOption<T>> queryAction)
        {
            return function().QueryOn(queryAction);
        }

        #endregion

        #region Paged Methods

        public static IPagedResult<IQueryable<T>> QueryPagedOn<T>(this T[] queryable,
            Action<IQueryOption<T>> queryAction)
        {
            return QueryPagedOn(queryable.AsQueryable(), queryAction);
        }

        public static IPagedResult<IQueryable<T>> QueryPagedOn<T>(this IQueryable<T> queryable,
            Action<IQueryOption<T>> queryAction)
        {
            var processed = OptionProcessor.Process<IQueryOption<T>, QueryOption<T>, T>(queryAction, queryable);
            return ResultProcessor.Process(processed);
        }

        public static IPagedResult<IQueryable<T>> QueryPagedOn<T>(this IEnumerable<T> queryable,
            Action<IQueryOption<T>> queryAction)
        {
            return QueryPagedOn(queryable.AsQueryable(), queryAction);
        }


        public static IPagedResult<IQueryable<T>> QueryPagedOn<T>(this Func<IEnumerable<T>> function,
            Action<IQueryOption<T>> queryAction)
        {
            return function().QueryPagedOn(queryAction);
        }

        public static IPagedResult<IQueryable<T>> QueryPagedOn<T>(this Func<T[]> function,
            Action<IQueryOption<T>> queryAction)
        {
            return function().QueryPagedOn(queryAction);
        }

        #endregion
    }
}