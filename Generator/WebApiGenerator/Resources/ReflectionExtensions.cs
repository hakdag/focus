using #projectname#.Common.Attributes;
using #projectname#.Common.Models;
using System;
using System.Linq;
using System.Linq.Dynamic;
using System.Reflection;

namespace #projectname#.Helpers
{
    public static class ReflectionExtensions
    {
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool desc)
        {
            var type = typeof(TEntity);

            var property = type.GetProperty(orderByProperty);
            if (property != null && property.PropertyType.BaseType == typeof(BaseModel))
            {
                var sp = property.PropertyType.GetProperties().FirstOrDefault(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(SearchPropertyAttribute)));
                orderByProperty += "." + sp.Name;
            }

            string command = desc ? orderByProperty + " descending" : orderByProperty;
            return source.OrderBy(command);
        }

        public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> source, string filterProperty, string filterQuery)
        {
            var type = typeof(TEntity);
            var property = type.GetProperty(filterProperty);
            if (property != null && property.PropertyType.BaseType == typeof(BaseModel))
            {
                var sp = property.PropertyType.GetProperties().FirstOrDefault(pi => pi.CustomAttributes.Any(ca => ca.AttributeType == typeof(SearchPropertyAttribute)));
                filterProperty += "." + sp.Name;
                return filter<TEntity>(source, sp, filterProperty, filterQuery);
            }
            return filter(source, property, filterProperty, filterQuery);
        }

        private static IQueryable<TEntity> filter<TEntity>(IQueryable<TEntity> source, PropertyInfo property, string filterProperty, string filterQuery)
        {
            if (property.PropertyType == typeof(int))
            {
                var filterValue = Convert.ToInt32(filterQuery);
                return source.Where("(" + filterProperty + " == @0)", filterValue);
            }

            return source.Where("(" + filterProperty + ".Contains(@0))", filterQuery);
        }

        private static PropertyInfo getPropertyRecursive(Type type, string[] parts, int index = 0)
        {
            var pName = parts[index];
            var property = type.GetProperty(pName);

            index++;
            if (index < parts.Length)
                return getPropertyRecursive(property.PropertyType, parts, index);

            return property;
        }
    }
}