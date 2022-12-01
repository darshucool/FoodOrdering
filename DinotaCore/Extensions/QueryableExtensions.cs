using System;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;

namespace Dinota.Core.Extensions
{
    public static class QueryableExtensions
    {
        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, 
            Expression<Func<TSource, TKey>> keySelector, ListSortDirection sortDirection)
        {
            if (sortDirection == ListSortDirection.Ascending)
                return source.OrderBy(keySelector);
            else
                return source.OrderByDescending(keySelector);
        }
    }
}
