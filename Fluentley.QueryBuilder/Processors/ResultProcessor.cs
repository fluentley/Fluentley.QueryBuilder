using System.Linq;
using Fluentley.QueryBuilder.Models;
using Fluentley.QueryBuilder.Options;

namespace Fluentley.QueryBuilder.Processors
{
    internal class ResultProcessor
    {

        public IPagedResult<IQueryable<T>> Process<T>(QueryOption<T> processedQueryOption)
        {
            var result = new PagedResult<IQueryable<T>>()
            {
                PageIndex = processedQueryOption.PageIndex,
                PageSize = processedQueryOption.PageSize,
                Data = processedQueryOption.Query,
                IsPaged = processedQueryOption.IsPaged
            };

            return result;
        }
    }
}