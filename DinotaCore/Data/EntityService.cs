using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using Dinota.Core.Specification;

namespace Dinota.Core.Data
{
    /// <summary>
    /// Exposes services provided by the Entity.
    /// </summary>
    /// <typeparam name="TEntity">The entity type serviced by this Service</typeparam>
    /// <typeparam name="TRepository">The repository type associated with the entity</typeparam>
    public abstract class EntityService<TEntity, TRepository>
        where TEntity : Entity
        where TRepository : IRepository<TEntity>
    {
        protected EntityService(TRepository repository)
        {
            Repository = repository;
        }

        protected virtual TRepository Repository { get; private set; }

        public virtual TEntity GetByKey(params object[] keys)
        {
            return Repository.GetByKey(keys);
        }

        public virtual TEntity GetBy(ISpecification<TEntity> specification)
        {
            return Repository.GetBy(specification);
        }

        public virtual IEnumerable<TEntity> GetCollection<TKey>(Expression<Func<TEntity, TKey>> sortExpression)
        {
            return Repository.GetCollection(GetDefaultSpecification(), sortExpression, ListSortDirection.Ascending);
        }

        public virtual IEnumerable<TEntity> GetCollection<TKey>(ISpecification<TEntity> specification, Expression<Func<TEntity, TKey>> sortExpression, ListSortDirection sortDirection = ListSortDirection.Ascending)
        {
            return Repository.GetCollection(specification, sortExpression, sortDirection);
        }

        public virtual IEnumerable<TEntity> GetCollection<TKey>(Expression<Func<TEntity, TKey>> sortExpression, ListSortDirection sortDirection, int pageSize, int pageIndex)
        {
            return Repository.GetCollection(sortExpression, sortDirection, pageSize, pageIndex);
        }

        public virtual IEnumerable<TEntity> GetCollection<TKey>(ISpecification<TEntity> specification, Expression<Func<TEntity, TKey>> sortExpression, ListSortDirection sortDirection, int pageSize, int pageIndex)
        {
            return Repository.GetCollection(specification, sortExpression, sortDirection, pageSize, pageIndex);
        }

        public virtual int Count(ISpecification<TEntity> specification)
        {
            return Repository.Count(specification);
        }

        /// <summary>
        /// Specifies the related objects to include in the query results.
        /// </summary>
        /// <typeparam name="TProperty">The type of the navigation property being included</typeparam>
        /// <param name="path">A lambda expression representing the path to include.</param>
        /// <returns>This instance to recursively call this in fluent style</returns>
        public virtual EntityService<TEntity, TRepository> Include<TProperty>(Expression<Func<TEntity, TProperty>> path)
        {
            Repository.Include(path);

            return this;
        }

        /// <summary>
        /// Returns the default specification for the entity.
        /// <remarks>Default implementation excludes expired entities</remarks>
        /// </summary>
        /// <returns></returns>
        public virtual ISpecification<TEntity> GetDefaultSpecification()
        {
            return new NotExpired<TEntity>();
        }

    }
}
