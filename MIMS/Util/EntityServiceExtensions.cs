using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using MIMS.Models;
using Dinota.Core.Data;
using Dinota.Core.Specification;
using MvcContrib.Sorting;

namespace MIMS.Util
{
    public static class EntityServiceExtensions
    {
        public static Pagination<TEntity> GetPagination<TEntity, TRepository, TKey>(this EntityService<TEntity, TRepository> service, Expression<Func<TEntity, TKey>> sortExpression, ListSortDirection sortDirection, int pageSize, int? page)
            where TEntity : Entity
            where TRepository : IRepository<TEntity>
        {
            var pageIndex = (page ?? 1) - 1;
            var entities = service.GetCollection(sortExpression, sortDirection, pageSize, pageIndex);
            var count = service.Count(new Specification<TEntity>(e => true));

            return new Pagination<TEntity>(entities, pageIndex + 1, pageSize, count);
        }

        public static Pagination<TEntity> GetPagination<TEntity, TRepository, TKey>(this EntityService<TEntity, TRepository> service, ISpecification<TEntity> specification, Expression<Func<TEntity, TKey>> sortExpression, ListSortDirection sortDirection, int pageSize, int? page)
            where TEntity : Entity
            where TRepository : IRepository<TEntity>
        {
            var pageIndex = (page ?? 1) - 1;
            var entities = service.GetCollection(specification, sortExpression, sortDirection, pageSize, pageIndex);
            var count = service.Count(specification);

            return new Pagination<TEntity>(entities, pageIndex + 1, pageSize, count);
        }

        public static Pagination<TEntity> GetPagination<TEntity, TRepository>(this EntityService<TEntity, TRepository> service, ListModel listModel)
            where TEntity : Entity
            where TRepository : IRepository<TEntity>
        {
            var pageIndex = (listModel.Page ?? 1) - 1;
            var entities = GetCollection(service, listModel, pageIndex);
            var count = service.Count(new Specification<TEntity>(e => true));

            return new Pagination<TEntity>(entities, pageIndex + 1, listModel.PageSize, count);
        }

        public static Pagination<TEntity> GetPagination<TEntity, TRepository>(this EntityService<TEntity, TRepository> service, ISpecification<TEntity> specification, ListModel listModel)
            where TEntity : Entity
            where TRepository : IRepository<TEntity>
        {
            var pageIndex = (listModel.Page ?? 1) - 1;
            var entities = GetCollection(service, listModel, pageIndex, specification);
            var count = service.Count(specification);

            return new Pagination<TEntity>(entities, pageIndex + 1, listModel.PageSize, count);
        }

        public static Pagination<TEntity> GetPagination<TEntity, TRepository>(this EntityService<TEntity, TRepository> service, ISpecification<TEntity> specification, ListModel listModel, Func<TEntity, TEntity> includeEntites, string type)
            where TEntity : Entity
            where TRepository : IRepository<TEntity>
        {
            var pageIndex = (listModel.Page ?? 1) - 1;
            var entities = GetCollection(service, listModel, pageIndex, specification).ToList();
            foreach (var entity in entities)
            {
                if (entity.GetType().Name.Split('_')[0] == type)
                {
                    var en = includeEntites(entity);
                }
            }
            var count = service.Count(specification);

            return new Pagination<TEntity>(entities, pageIndex + 1, listModel.PageSize, count);
        }

        static IEnumerable<TEntity> GetCollection<TEntity, TRepository>(EntityService<TEntity, TRepository> service, ListModel listModel, int pageIndex, ISpecification<TEntity> specification = null)
            where TEntity : Entity
            where TRepository : IRepository<TEntity>
        {
            try
            {
                /* note: LINQ to entities rejects Expression<Func<TEntity, object>> type sort expressions when u want to sort by a value type (eg:int, DateTime).
                 * there are two workarounds. call the correct 'GetCollection' method through reflection or treat each case separately. both methods are ugly but
                 * settle with more readable version. however only frequently used types are implemented. other types have to be implemented as necessary
                 */
                if (listModel.Column == null)
                {
                    return service.GetCollection(entity => entity.LastModifiedDate, ListSortDirection.Descending, listModel.PageSize, pageIndex);
                }

                var propInfo = GetProperty<TEntity>(listModel.Column);

                if (propInfo != null)
                {
                    var propertyType = propInfo.PropertyType;

                    if (propertyType == typeof(string))
                    {
                        return GetSortedCollection<TEntity, TRepository, string>(service, listModel, pageIndex, specification);
                    }
                    if (propertyType == typeof(short))
                    {
                        return GetSortedCollection<TEntity, TRepository, short>(service, listModel, pageIndex, specification);
                    }
                    if (propertyType == typeof(int))
                    {
                        return GetSortedCollection<TEntity, TRepository, int>(service, listModel, pageIndex, specification);
                    }
                    if (propertyType == typeof(DateTime))
                    {
                        return GetSortedCollection<TEntity, TRepository, DateTime>(service, listModel, pageIndex, specification);
                    }
                    if (propertyType == typeof(decimal))
                    {
                        return GetSortedCollection<TEntity, TRepository, decimal>(service, listModel, pageIndex, specification);
                    }

                    //nullable primitives behave differently
                    if (propInfo.PropertyType.IsGenericType && propInfo.PropertyType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                    {
                        propertyType = Nullable.GetUnderlyingType(propInfo.PropertyType);

                        if (propertyType == typeof(short))
                        {
                            return GetSortedCollection<TEntity, TRepository, short?>(service, listModel, pageIndex, specification);
                        }
                        if (propertyType == typeof(int))
                        {
                            return GetSortedCollection<TEntity, TRepository, int?>(service, listModel, pageIndex, specification);
                        }
                        if (propertyType == typeof(DateTime))
                        {
                            return GetSortedCollection<TEntity, TRepository, DateTime?>(service, listModel, pageIndex, specification);
                        }
                        if (propertyType == typeof(decimal))
                        {
                            return GetSortedCollection<TEntity, TRepository, decimal?>(service, listModel, pageIndex, specification);
                        }
                    }
                }
            }
            catch (ArgumentNullException)//sort property name is null or empty
            {
            }
            catch (AmbiguousMatchException)//multiple properties found
            {
            }

            //fallback to known case if no matching property found
            return service.GetCollection(service.GetDefaultSpecification(), entity => entity.LastModifiedDate, ListSortDirection.Descending, listModel.PageSize, pageIndex);
        }

        /// <summary>
        /// Builds the sort expression when given the property name
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="propertyName">Name of the property of the entity or nested name like Category.Name</param>
        /// <returns></returns>
        internal static Expression<Func<TEntity, TKey>> SortBy<TEntity, TKey>(string propertyName)
        {
            var nestedPropertyNames = propertyName.Split('.').ToList();

            var param = Expression.Parameter(typeof(TEntity), "o");
            var body = Expression.PropertyOrField(param, nestedPropertyNames[0]);

            nestedPropertyNames.RemoveAt(0);

            //if the propertyName is nested eg: Category.Name then create appropriate MemberAccess expression.

            //this loop can be converted to LINQ, but this is more readable.
            foreach (var nestedPropertyName in nestedPropertyNames)
            {
                body = Expression.PropertyOrField(body, nestedPropertyName);
            }

            return Expression.Lambda<Func<TEntity, TKey>>(body, param);
        }

        static IEnumerable<TEntity> GetSortedCollection<TEntity, TRepository, TKey>(EntityService<TEntity, TRepository> service, ListModel listModel, int pageIndex, ISpecification<TEntity> specification = null)
            where TEntity : Entity
            where TRepository : IRepository<TEntity>
        {
            if (specification != null)
                return service.GetCollection(specification, SortBy<TEntity, TKey>(listModel.Column), GetListSortDirection(listModel.GetGridSortOptions().Direction), listModel.PageSize, pageIndex);
            else
                return service.GetCollection(SortBy<TEntity, TKey>(listModel.Column), GetListSortDirection(listModel.GetGridSortOptions().Direction), listModel.PageSize, pageIndex);
        }

        internal static ListSortDirection GetListSortDirection(SortDirection sortDirection)
        {
            if (sortDirection == SortDirection.Ascending)
                return ListSortDirection.Ascending;
            else
                return ListSortDirection.Descending;
        }

        static PropertyInfo GetProperty<T>(string propertyName)
        {
            var nestedPropertyNames = propertyName.Split('.').ToList();

            var type = typeof(T);
            PropertyInfo propertyInfo = null;

            foreach (var nestedPropertyName in nestedPropertyNames)
            {
                propertyInfo = type.GetProperty(nestedPropertyName);
                type = propertyInfo.PropertyType;
            }

            return propertyInfo;
        }

    }
}