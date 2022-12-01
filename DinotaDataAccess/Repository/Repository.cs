using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Dinota.Core.Data;
using Dinota.Core.Extensions;
using Dinota.Core.Specification;
using Dinota.DataAccces.Context;

namespace Dinota.DataAccces.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        protected IQueryable<TEntity> Queryable;

        protected IDbSet<TEntity> DbSet;

        protected Repository(DomainContext domainContext)
        {
            DomainContext = domainContext;
            Queryable = DbSet = DomainContext.Set<TEntity>();
        }
        
        protected DomainContext DomainContext { get; private set; }

        public virtual TEntity GetByKey(params object[] keys)
        {
            return DbSet.Find(keys);
        }

        public virtual TEntity GetBy(ISpecification<TEntity> specification)
        {
            return Queryable.Where(specification.EvalPredicate).SingleOrDefault();
        }

        public virtual IEnumerable<TEntity> GetCollection<TKey>(ISpecification<TEntity> specification, Expression<Func<TEntity, TKey>> sortExpression, ListSortDirection sortDirection)
        {
            specification.ThrowIfNull("specification");
            sortExpression.ThrowIfNull("sortExpression");

            return Queryable.Where(specification.EvalPredicate).OrderBy(sortExpression, sortDirection).AsNoTracking();
        }

        public virtual IEnumerable<TEntity> GetCollection<TKey>(Expression<Func<TEntity, TKey>> sortExpression, ListSortDirection sortDirection, int pageSize, int pageIndex)
        {
            sortExpression.ThrowIfNull("sortExpression");

            return Queryable.OrderBy(sortExpression, sortDirection).AsNoTracking();
        }

        public virtual IEnumerable<TEntity> GetCollection<TKey>(ISpecification<TEntity> specification, Expression<Func<TEntity, TKey>> sortExpression, ListSortDirection sortDirection, int pageSize, int pageIndex)
        {
            specification.ThrowIfNull("specification");
            sortExpression.ThrowIfNull("sortExpression");

            return Queryable.Where(specification.EvalPredicate).OrderBy(sortExpression, sortDirection).AsNoTracking();
        }

        public virtual int Count(ISpecification<TEntity> specification)
        {
            specification.ThrowIfNull("specification");

            return Queryable.Count(specification.EvalPredicate);
        }

        public virtual void Include<TProperty>(Expression<Func<TEntity, TProperty>> path)
        {
            Queryable = Queryable.Include(path);
        }
    }
}
