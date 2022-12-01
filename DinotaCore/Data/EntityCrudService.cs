using Dinota.Core.Extensions;

namespace Dinota.Core.Data
{
    /// <summary>
    /// Extends the EntityService base with CRUD support
    /// </summary>
    /// <typeparam name="TEntity">The entity type serviced by this Service</typeparam>
    /// <typeparam name="TRepository">The repository type associated with the entity</typeparam>
    public abstract class EntityCrudService<TEntity, TRepository> : EntityService<TEntity, TRepository>
        where TEntity : Entity
        where TRepository : ICrudRepository<TEntity>
    {
        protected EntityCrudService(TRepository repository)
            : base(repository)
        {
        }

        /// <summary>
        /// Creates new instance of the entity and performs any logic related to creation of the entity.
        /// <remarks>Derived services can have their own Create methods and may not support the default parameter less Create.
        /// The created entity has to be added to the service using Add method inorder to be persisted</remarks>
        /// </summary>
        /// <returns></returns>
        public virtual TEntity Create()
        {
            return Repository.Create();
        }

        public virtual void Add(TEntity entity)
        {
            entity.ThrowIfNull("entity");

            Repository.Add(entity);
        }

        public virtual void Delete(TEntity entity)
        {
            entity.ThrowIfNull("entity");

            Repository.Delete(entity);
        }
    }
}
