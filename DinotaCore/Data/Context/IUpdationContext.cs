using System.Collections.Generic;

namespace Dinota.Core.Data.Context
{
    public interface IUpdationContext : IEntityContext
    {
        /// <summary>
        /// The properties that have been changed and their original values
        /// </summary>
        IDictionary<string, object> Changes { get; }

        string GenerateID(int idType);
    }
}
