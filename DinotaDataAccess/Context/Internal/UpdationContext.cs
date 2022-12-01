using System.Collections.Generic;
using Dinota.Core.Data;
using Dinota.Core.Data.Context;

namespace Dinota.DataAccces.Context.Internal
{
    [System.Diagnostics.DebuggerDisplay("UserName = {UserName} Changes = {Changes}")]
    internal class UpdationContext : EntityContext, IUpdationContext
    {
        public UpdationContext(DomainContext domainContext, Entity entity)
            : base(domainContext, entity)
        {
        }

        private Dictionary<string, object> _changes;

        public IDictionary<string, object> Changes
        {
            get { return _changes ?? (_changes = GetDetectedChanges()); }
        }

        public string GenerateID(int idType)
        {
            return new IdGenerationStrategy(DomainContext).GenerateID(idType);
        }

        protected virtual Dictionary<string, object> GetDetectedChanges()
        {
            _changes = new Dictionary<string, object>();

            var dbEntity = DomainContext.Entry(Entity);

            foreach (var propertyName in dbEntity.OriginalValues.PropertyNames)
            {
                if (dbEntity.Property(propertyName).IsModified)
                {
                    _changes.Add(propertyName, dbEntity.OriginalValues.GetValue<object>(propertyName));
                }
            }

            return _changes;
        }
    }
}
