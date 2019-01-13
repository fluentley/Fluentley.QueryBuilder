using System;
using System.Linq;
using System.Linq.Expressions;
using Fluentley.QueryBuilder.Models;
using Fluentley.QueryBuilder.Options;

namespace Fluentley.QueryBuilder.Processors
{
    internal class ResultProcessor
    {
        public IQueryResult<T> Process<T>(QueryOption<T> processedQueryOption)
        {
            var queryResult = new QueryResult<T>();

            if (processedQueryOption.IsPaged)

                queryResult.Paging = new QueryPaging
                {
                    IsPaged = true,
                    CurrentPageIndex = processedQueryOption.PageIndex,
                    TotalPages =
                        (int) Math.Ceiling(processedQueryOption.QueryWithNoPaging.Count() /
                                           (double) processedQueryOption.PageSize),
                    TotalRecords = processedQueryOption.QueryWithNoPaging.Count()
                };

            queryResult.EagerLoads = processedQueryOption.EagerLoads;
            queryResult.Data = processedQueryOption.Query;
            return queryResult;
        }


        public IQueryResult<T,TSelect> Process<T,TSelect>(QueryOption<T> processedQueryOption, Expression<Func<T, TSelect>> selector)
        {
            var queryResult = new QueryResult<T, TSelect>();

            if (processedQueryOption.IsPaged)

                queryResult.Paging = new QueryPaging
                {
                    IsPaged = true,
                    CurrentPageIndex = processedQueryOption.PageIndex,
                    TotalPages =
                        (int)Math.Ceiling(processedQueryOption.QueryWithNoPaging.Count() /
                                          (double)processedQueryOption.PageSize),
                    TotalRecords = processedQueryOption.QueryWithNoPaging.Count()
                };

            queryResult.EagerLoads = processedQueryOption.EagerLoads;
            queryResult.Data = processedQueryOption.Query.Select(selector);
            return queryResult;
        }
    }
}