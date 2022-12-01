
namespace Dinota.Core.Data
{
    /// <summary>
    /// Adds Add, Delete facilities to the repository
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface ICrudRepository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        /// <summary>
        /// Creates new instance of the entity.
        /// <remarks>The created entity has to be added to the repository using Add method inorder to be persisted</remarks>
        /// </summary>
        /// <returns></returns>
        TEntity Create();

        /// <summary>
        /// Adds an entity to the repository for it to be persisted.
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// Deletes a persisted entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(TEntity entity);
    }
}
