using System;
using Dinota.DataAccces.Context;
using Dinota.Domain;

namespace Dinota.DataAccces.Repository
{
    public abstract class CrudSyncRepository<TEntity> : CrudRepository<TEntity>
        where TEntity : SyncEntity
    {
        private readonly DateTime? _lastSyncTime;

        protected CrudSyncRepository(DomainContext domainContext) : base(domainContext)
        {
            domainContext.UnderlyingObjectContext.ObjectMaterialized += ObjectMaterialized;
            _lastSyncTime = domainContext.GetLastSyncTime();
        }

        private void ObjectMaterialized(object sender, System.Data.Objects.ObjectMaterializedEventArgs e)
        {
            if (e.Entity is TEntity)
            {
                var syncEntity = (TEntity)e.Entity;

                if (_lastSyncTime == null)
                {
                    syncEntity.IsSynced = false;
                }
                else
                {
                    syncEntity.IsSynced = _lastSyncTime.Value > syncEntity.CreationDate;
                }
            }
        }
    }
}
