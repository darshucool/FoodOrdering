using Dinota.Core.Data;

namespace Dinota.Domain.Filter
{
    public interface IFilterable<TEntity> where TEntity : Entity
    {
        FilterBuilder<TEntity> GetFilterBuilder();
    }
}
