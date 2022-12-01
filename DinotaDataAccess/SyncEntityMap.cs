using System.Data.Entity.ModelConfiguration;
using Dinota.Domain;

namespace Dinota.DataAccces
{
    public class SyncEntityMap<TEntity> : EntityTypeConfiguration<TEntity>
        where TEntity : SyncEntity
    {
        public SyncEntityMap()
        {
            Ignore(entity => entity.IsSynced);
        }
    }
}
