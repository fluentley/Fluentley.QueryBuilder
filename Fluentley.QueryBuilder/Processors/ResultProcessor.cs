using System;
using System.Linq;
using Fluentley.QueryBuilder.Models;
using Fluentley.QueryBuilder.Options;

namespace Fluentley.QueryBuilder.Processors
{
    internal class ResultProcessor
    {
        public IQueryResult<IQueryable<T>> Process<T>(QueryOption<T> processedQueryOption)
        {
            var queryResult = new QueryResult<IQueryable<T>>();


            if (processedQueryOption.IsPaged)

                queryResult.Paging = new QueryPaging
                {
                    IsPaged = true,
                    CurrentPageIndex = processedQueryOption.PageIndex,
                    TotalPages =
                        (int)Math.Ceiling(processedQueryOption.QueryWithNoPaging.ToList().Count /
                                           (double)processedQueryOption.PageSize),
                    TotalRecords = processedQueryOption.QueryWithNoPaging.ToList().Count
                };

            queryResult.Data = processedQueryOption.Query;
            return queryResult;
        }
    }
}