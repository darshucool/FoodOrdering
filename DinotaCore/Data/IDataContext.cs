using System;

namespace Dinota.Core.Data
{
    /// <summary>
    /// Provides the Unit of work facility when dealing with entities.
    /// </summary>
    public interface IDataContext : IDisposable
    {
        /// <summary>
        /// Submits the changes made to entities.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}
