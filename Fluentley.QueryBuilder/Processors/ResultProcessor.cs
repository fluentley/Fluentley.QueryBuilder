using System;
using System.Linq;
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
    }
}