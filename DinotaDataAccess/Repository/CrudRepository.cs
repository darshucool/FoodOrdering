using Dinota.Core.Data;
using Dinota.DataAccces.Context;

namespace Dinota.DataAccces.Repository
{
    public abstract class CrudRepository<TEntity> : Repository<TEntity>, ICrudRepository<TEntity>
        where TEntity : Entity
    {
        protected CrudRepository(DomainContext domainContext)
            : base(domainContext)
        {
        }

        public TEntity Create()
        {
            return DbSet.Create();
        }

        public virtual void Add(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity.ExpireOnDelete)
            {
                DomainContext.ExpireEntity(entity);
            }
            else
            {
                DbSet.Remove(entity);
            }
        }
    }
}
