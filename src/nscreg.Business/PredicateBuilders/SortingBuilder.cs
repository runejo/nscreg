using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using nscreg.Utilities.Enums;

namespace nscreg.Business.PredicateBuilders
{
    /// <summary>
    /// Sort predicate builder, works as extension of IQueryable
    /// </summary>
    public static class SortingBuilder
    {
        /// <summary>
        /// OrderBy extension
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="source">Source collection</param>
        /// <param name="sortField">Sorting field</param>
        /// <returns>Sorted iqueryable</returns>
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, (SortFields, OrderRule) sortField)
        {
            var type = typeof(T);
            var property = type.GetProperty(sortField.Item1.ToString());
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var resultExp = Expression.Call(typeof(Queryable),
                sortField.Item2 == OrderRule.Asc ? "OrderBy" : "OrderByDescending", new[] { type, property.PropertyType },
                source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }

        /// <summary>
        /// ThenBy extension
        /// </summary>
        /// <typeparam name="T">Entity type</typeparam>
        /// <param name="source">Source collection</param>
        /// <param name="sortField">Sorting field</param>
        /// <returns>Sorted iqueryable</returns>
        public static IQueryable<T> ThenBy<T>(this IQueryable<T> source, (SortFields, OrderRule) sortField)
        {
            var type = typeof(T);
            var property = type.GetProperty(sortField.Item1.ToString());
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            var resultExp = Expression.Call(typeof(Queryable),
                sortField.Item2 == OrderRule.Asc ? "ThenBy" : "ThenByDescending", new[] { type, property.PropertyType },
                source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}