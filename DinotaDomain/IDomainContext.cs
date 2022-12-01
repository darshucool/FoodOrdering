using System.Collections.Generic;
using Dinota.Core.Data;
using Dinota.Core.Xml;

namespace Dinota.Domain
{
    /// <summary>
    /// Provides the Unit of work facility when dealing with domain entities.
    /// </summary>
    public interface IDomainContext : IDataContext
    {
        /// <summary>
        /// The name of the user associated with this context.
        /// </summary>
        string UserName { get; }

        /// <summary>
        /// Tracing entity state.
        /// </summary>
        /// <param name="trackingEntities"></param>
        void EntityStateTracking(List<TrackingEntityRecord> trackingEntities);
    }
}
