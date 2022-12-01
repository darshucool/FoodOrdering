using System;
using System.Linq.Expressions;
using Dinota.Core.Data;

namespace Dinota.Core.Specification
{
    /// <summary>
    /// Default implementation of ISpecification<see cref="ISpecification{TEntity}"/>
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    [System.Diagnostics.DebuggerDisplay("Predicate = {EvalPredicate}")]
    public class Specification<TEntity> : ISpecification<TEntity>
        where TEntity : Entity
    {
        public Specification(Expression<Func<TEntity, bool>> evalPredicate)
        {
            EvalPredicate = evalPredicate;
        }

        public Expression<Func<TEntity, bool>> EvalPredicate
        {
            get;
            private set;
        }
    }
}
