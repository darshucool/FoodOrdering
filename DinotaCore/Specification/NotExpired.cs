using Dinota.Core.Data;

namespace Dinota.Core.Specification
{
    /// <summary>
    /// Specification to represent not expired entities
    /// </summary>
    /// <remarks>This does not check <![CDATA[ExpiryDate < DateTime.Now]]></remarks>
    /// <typeparam name="TEntity"></typeparam>
    public class NotExpired<TEntity> : Specification<TEntity>
        where TEntity : Entity
    {
        public NotExpired()
            : base(e => e.ExpiryDate == null)
        {
        }
    }
}
