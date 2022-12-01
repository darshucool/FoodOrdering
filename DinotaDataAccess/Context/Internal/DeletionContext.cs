using Dinota.Core.Data;
using Dinota.Core.Data.Context;

namespace Dinota.DataAccces.Context.Internal
{
    internal class DeletionContext : UpdationContext, IDeletionContext
    {
        public DeletionContext(DomainContext domainContext, Entity entity)
            : base(domainContext, entity)
        {
        }
    }
}
