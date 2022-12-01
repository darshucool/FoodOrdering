using System;
using Dinota.Core.Data;
using Dinota.Core.Data.Context;

namespace Dinota.DataAccces.Context.Internal
{
    internal class EntityContext : IEntityContext
    {
        protected EntityContext(DomainContext domainContext, Entity entity)
        {
            if (domainContext == null)
                throw new ArgumentNullException("domainContext");

            if (entity == null)
                throw new ArgumentNullException("entity");

            DomainContext = domainContext;
            UserName = domainContext.UserName;
            Entity = entity;
        }

        public string UserName
        {
            get;
            private set;
        }

        protected DomainContext DomainContext { get; private set; }

        protected Entity Entity { get; private set; }

        public TService GetService<TService>()
        {
            return Configuration.Instance.Container.Resolve<TService>();
        }
    }
}
