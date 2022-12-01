using System;
using System.Linq.Expressions;
using Dinota.Core.Data;
using Dinota.Core.Extensions;

namespace Dinota.Core.Specification
{
    /// <summary>
    /// Composite Specification to represent logical OR
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    internal class OrSpecification<TEntity> : ISpecification<TEntity>
        where TEntity : Entity
    {
        public OrSpecification(Expression<Func<TEntity, bool>> s1, Expression<Func<TEntity, bool>> s2)
        {
            EvalPredicate = s1.Or(s2);
        }

        public Expression<Func<TEntity, bool>> EvalPredicate
        {
            get;
            private set;
        }
    }
}
