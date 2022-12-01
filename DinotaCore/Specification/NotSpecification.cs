using System;
using System.Linq;
using System.Linq.Expressions;
using Dinota.Core.Data;

namespace Dinota.Core.Specification
{
    /// <summary>
    /// Negates the given Specification
    /// <seealso cref="http://blogs.rev-net.com/ddewinter/2009/05/31/linq-expression-trees-and-the-specification-pattern/"/> 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    internal class NotSpecification<TEntity> : ISpecification<TEntity>
        where TEntity : Entity
    {
        public NotSpecification(Expression<Func<TEntity, bool>> s)
        {
            EvalPredicate = Expression.Lambda<Func<TEntity, bool>>(
                Expression.Not(s.Body),
                s.Parameters.Single());
        }

        public Expression<Func<TEntity, bool>> EvalPredicate
        {
            get;
            private set;
        }
    }
}
