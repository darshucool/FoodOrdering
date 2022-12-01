using System;
using System.Linq.Expressions;
using Dinota.Core.Data;

namespace Dinota.Core.Specification
{
    /// <summary>
    /// Interface to create specifications. i.e Apply various filters conditions on entities.
    /// See also <seealso cref="http://en.wikipedia.org/wiki/Specification_pattern"/>
    /// </summary>
    /// <typeparam name="TEntity">The entity type</typeparam>
    public interface ISpecification<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// The predicate to be evaluated
        /// </summary>
       Expression<Func<TEntity, bool>> EvalPredicate { get; }
    }
}
