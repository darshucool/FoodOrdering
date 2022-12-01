using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Dinota.Core.Data
{
    /// <summary>
    /// Defines the basic contract for read only operations for a particular entity.
    /// Should be implemented by the ORM provider
    /// </summary>
    /// <remarks>Business logic should not be implemented here. Collection methods are not guaranteed to return change tracking entities </remarks>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity> : IRepository
        where TEntity : Entity
    {
        TEntity GetByKey(params object[] keys);

        TEntity GetBy(Specification.ISpecification<TEntity> specification);

        IEnumerable<TEntity> GetCollection<TKey>(Specification.ISpecification<TEntity> specification, Expression<Func<TEntity, TKey>> sortExpression, ListSortDirection sortDirection);

        IEnumerable<TEntity> GetCollection<TKey>(Expression<Func<TEntity, TKey>> sortExpression, ListSortDirection sortDirection, int pageSize, int pageIndex);

        IEnumerable<TEntity> GetCollection<TKey>(Specification.ISpecification<TEntity> specification, Expression<Func<TEntity, TKey>> sortExpression, ListSortDirection sortDirection, int pageSize, int pageIndex);

        int Count(Specification.ISpecification<TEntity> specification);

        void Include<TProperty>(Expression<Func<TEntity, TProperty>> path);
    }

    /// <summary>
    /// Marker interface for repositories
    /// </summary>
    public interface IRepository { }
}
