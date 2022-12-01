using Dinota.Core.Data;
using Dinota.Core.Data.Context;

namespace Dinota.DataAccces.Context.Internal
{
    [System.Diagnostics.DebuggerDisplay("UserName = {UserName}")]
    internal class InsertionContext : EntityContext, IInsertionContext
    {
        public InsertionContext(DomainContext domainContext, Entity entity)
            : base(domainContext, entity)
        {
        }

        public string GenerateID(int idType, params object[] args)
        {
            return new IdGenerationStrategy(DomainContext).GenerateID(idType, args);
        }
    }
}
