using System.Linq.Expressions;
using System.Reflection;
using InterviewProject.Data.Filters;
using InterviewProject.Data.Interfaces;

namespace InterviewProject.Services.Utils
{
    public static class SortService<TEntity, TKey> // : IFilterSortService<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public static IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, ISorter command)
        {
            if (string.IsNullOrEmpty(command.SortField)) return query;
            if (command.SortField.EndsWith("_original"))
                command.SortField = command.SortField.Remove(command.SortField.Length - 9);

            if (!CheckPropertyChain(command.SortField))
            {
                return query;
            }

            var param = Expression.Parameter(typeof(TEntity), "item");
            var body = command.SortField.Split('.').Aggregate<string, Expression>(param, Expression.Property);
            var lambda = Expression.Lambda(body, param);

            var method = typeof(Queryable).GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(a => a.Name == $"OrderBy{(command.IsAsc ? string.Empty : "Descending")}")
                .Single(a => a.GetParameters().Length == 2);
            method = method.MakeGenericMethod(typeof(TEntity), body.Type);
            return (IQueryable<TEntity>)method.Invoke(method, new object[] { query, lambda });
        }
        
        private static bool CheckPropertyChain(string propertyChain)
        {
            var propertyName = propertyChain?.Split('.') ?? new string[0];
            PropertyInfo entityPropertyInfo = null;
            var type = typeof(TEntity);

            foreach (var part in propertyName)
            {
                entityPropertyInfo = type.GetProperty(part,
                    BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                if (entityPropertyInfo != null)
                {
                    type = entityPropertyInfo.PropertyType;
                }
                else
                {
                    break;
                }
            }

            if (entityPropertyInfo is null)
            {
                return false;
            }

            return true;
        }
    }
}

