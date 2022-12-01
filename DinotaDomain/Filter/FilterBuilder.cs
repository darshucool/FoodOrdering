using System;
using Dinota.Core.Data;
using Dinota.Core.Specification;
using Dinota.Domain.User;

namespace Dinota.Domain.Filter
{
    public class FilterBuilder<TEntity> where TEntity : Entity
    {
        private readonly Func<ISpecification<TEntity>, UserBase, ISpecification<TEntity>> _filter;

        public FilterBuilder()
        {            
        }

        public FilterBuilder(Func<ISpecification<TEntity>, UserBase, ISpecification<TEntity>> filter)
        {
            _filter = filter;
        }

        public virtual ISpecification<TEntity> BuildUserFilter(ISpecification<TEntity> specification, UserBase user)
        {
            if (_filter == null)
            {
                return specification;
            }

            return _filter(specification, user);
        }
    }
}
