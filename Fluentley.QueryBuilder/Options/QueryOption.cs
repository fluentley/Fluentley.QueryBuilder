using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace Fluentley.QueryBuilder.Options
{
    internal class QueryOption<T> : IQueryOption<T>
    {
        public QueryOption(IQueryable<T> query)
        {
            EagerLoads = new List<Expression<Func<T, object>>>();
            IsPaged = false;
            Query = query;
            QueryWithNoPaging = query;
        }

        internal List<Expression<Func<T, object>>> EagerLoads { get; set; }

        public IQueryable<T> Query { get; set; }
        public IQueryable<T> QueryWithNoPaging { get; set; }

        //Paging Properties
        public bool IsPaged { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }


        public IQueryOption<T> DynamicSort(string propertyName, string direction = "asc")
        {
            if (string.IsNullOrWhiteSpace(direction))
                direction = "asc";

            if (string.IsNullOrWhiteSpace(propertyName))
                return this;


            var property = typeof(T).GetProperties().FirstOrDefault(x => x.Name.ToLower() == propertyName.ToLower());
            if (property == null)
                return this;


            var sortByProperty = PropertySelector(property.Name);

            switch (direction.ToLower() == "asc")
            {
                default:
                    Query = Query.OrderBy(sortByProperty);
                    break;
                case false:
                    Query = Query.OrderByDescending(sortByProperty);
                    break;
            }

            return this;
        }


        public IQueryOption<T> DynamicWhere(string filter)
        {
            filter = filter.Replace("\\", string.Empty);
            Query = Query.Where(filter);
            QueryWithNoPaging = QueryWithNoPaging.Where(filter);
            return this;
        }

        public IQueryOption<T> Paging(int pageIndex, int pageSize)
        {
            if (pageSize == 0)
            {
                IsPaged = false;
                return this;
            }

            IsPaged = true;
            PageIndex = pageIndex;
            PageSize = pageSize;


            Query = Query.Skip(pageIndex * pageSize).Take(pageSize);
            return this;
        }


        public IQueryOption<T> DynamicContains(string propertyName, string value)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                return this;


            var property = typeof(T).GetProperties().FirstOrDefault(x => x.Name.ToLower() == propertyName.ToLower());
            if (property == null)
                return this;

            var selectedProperty = PropertySelector(property.Name);
            Query = Query.Where(
                x => selectedProperty.Compile().Invoke(x).ToString().ToLower().Contains(value.ToLower()));
            QueryWithNoPaging = QueryWithNoPaging.Where(x =>
                selectedProperty.Compile().Invoke(x).ToString().ToLower().Contains(value.ToLower()));
            return this;
        }

        public IQueryOption<T> QueryBy(IQueryable<T> query)
        {
            Query = query;
            QueryWithNoPaging = query;
            return this;
        }


        public IQueryOption<T> QueryBy(Func<IQueryable<T>, IQueryable<T>> query)
        {
            Query = query(Query);
            QueryWithNoPaging = query(QueryWithNoPaging);
            return this;
        }

        public IQueryOption<T> EagerLoad(params Expression<Func<T, object>>[] eagerLoads)
        {
            if (eagerLoads.Any())
                EagerLoads.AddRange(eagerLoads);
            return this;
        }


        private static Expression<Func<T, object>> PropertySelector(string propertyName)
        {
            var argument = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(argument, propertyName);
            //return the property as object
            var unaryExpression = Expression.Convert(property, typeof(object));
            var expression = Expression.Lambda<Func<T, object>>(unaryExpression, argument);
            return expression;
        }
    }
}