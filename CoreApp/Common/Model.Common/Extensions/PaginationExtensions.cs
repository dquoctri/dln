using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Model.Common.Extensions
{
    public static class PaginationExtensions
    {
        public static IQueryable<T> PagingSort<T>(this IQueryable<T> source, string orderBy, bool isAscending)
        {
            ArgumentNullException.ThrowIfNull(orderBy);
            var type = typeof(T);
            var property = type.GetProperty(orderBy);
            ArgumentNullException.ThrowIfNull(property);
            var linqOrderMethodName = isAscending ? nameof(Queryable.OrderBy) : nameof(Queryable.OrderByDescending); // using for c# 6 and above
            var parameter = Expression.Parameter(type, string.Empty);
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var lambda = Expression.Lambda(propertyAccess, parameter);

            var resultExp = Expression.Call(
                typeof(Queryable),
                linqOrderMethodName,
                new[] { type, property.PropertyType },
                source.Expression,
                Expression.Quote(lambda));
            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}
