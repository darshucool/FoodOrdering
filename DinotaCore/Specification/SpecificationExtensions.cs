using System;
using System.Linq.Expressions;
using Dinota.Core.Data;

namespace Dinota.Core.Specification
{
    public static class SpecificationExtensions
    {
        /// <summary>
        /// Combines two specifications using logical AND
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <param name="s1">First specification</param>
        /// <param name="s2">Second specification</param>
        /// <returns>Composite specification</returns>
        public static ISpecification<TEntity> And<TEntity>(this ISpecification<TEntity> s1, ISpecification<TEntity> s2)
            where TEntity : Entity
        {
            return new AndSpecification<TEntity>(s1.EvalPredicate, s2.EvalPredicate);
        }

        /// <summary>
        /// Combines two specifications using logical AND
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <param name="s1">First specification</param>
        /// <param name="predicate">Predicate to combine</param>
        /// <returns>Composite specification</returns>
        public static ISpecification<TEntity> And<TEntity>(this ISpecification<TEntity> s1, Expression<Func<TEntity, bool>> predicate)
            where TEntity : Entity
        {
            return new AndSpecification<TEntity>(s1.EvalPredicate, predicate);
        }

        /// <summary>
        /// Adds condition to filter entities that are not expired
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <param name="s1">Specification to combine with</param>
        /// <returns>Composite specification</returns>
        public static ISpecification<TEntity> AndNotExpired<TEntity>(this ISpecification<TEntity> s1)
            where TEntity : Entity
        {
            return new AndSpecification<TEntity>(s1.EvalPredicate, e => e.ExpiryDate == null);
        }

        /// <summary>
        /// Adds condition to filter entities that are expired
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <param name="s1">Specification to combine with</param>
        /// <returns>Composite specification</returns>
        public static ISpecification<TEntity> AndExpired<TEntity>(this ISpecification<TEntity> s1)
            where TEntity : Entity
        {
            return new AndSpecification<TEntity>(s1.EvalPredicate, e => e.ExpiryDate != null);
        }

        /// <summary>
        /// Combines two specifications using logical OR
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <param name="s1">First specification</param>
        /// <param name="s2">Second specification</param>
        /// <returns>Composite specification</returns>
        public static ISpecification<TEntity> Or<TEntity>(this ISpecification<TEntity> s1, ISpecification<TEntity> s2)
            where TEntity : Entity
        {
            return new OrSpecification<TEntity>(s1.EvalPredicate, s2.EvalPredicate);
        }

        /// <summary>
        /// Combines two specifications using logical OR
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <param name="s1">First specification</param>
        /// <param name="predicate">Predicate to combine</param>
        /// <returns>Composite specification</returns>
        public static ISpecification<TEntity> Or<TEntity>(this ISpecification<TEntity> s1, Expression<Func<TEntity, bool>> predicate)
            where TEntity : Entity
        {
            return new OrSpecification<TEntity>(s1.EvalPredicate, predicate);
        }

        /// <summary>
        /// Negates the specification
        /// </summary>
        /// <typeparam name="TEntity">The entity type</typeparam>
        /// <param name="s">Specification to negate</param>
        /// <returns>Composite specification</returns>
        public static ISpecification<TEntity> Not<TEntity>(this ISpecification<TEntity> s)
            where TEntity : Entity
        {
            return new NotSpecification<TEntity>(s.EvalPredicate);
        }
    }
}
