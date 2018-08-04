using System;
using System.Collections.Generic;
using System.Text;

namespace Fluentley.QueryBuilder.Models
{
    public interface IPagedResult<T>
    {
        int PageIndex { get; }
        int PageSize { get; }
        T Data { get; }
    }
}
