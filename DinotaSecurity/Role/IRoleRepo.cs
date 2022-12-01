using System.Collections.Generic;
using Dinota.Core.Data;

namespace Dinota.Security.Role
{
    public interface IRoleRepo : IRepository<Role>
    {
        IEnumerable<Role> GetUserRoles(string userName);

        IEnumerable<Role> GetCollection();

        ReadableRole GetReadableRole(int functionalAreaId);

        IEnumerable<Role> GetRoles(int functionalAreaId);
   
    }
}
