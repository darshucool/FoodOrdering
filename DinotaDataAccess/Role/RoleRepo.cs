using System.Collections.Generic;
using System.Linq;
using Dinota.DataAccces.Repository;
using Dinota.Security.Role;
using Dinota.DataAccces.Context;


namespace Dinota.DataAccces.Role
{
    public class RoleRepo : Repository<Security.Role.Role>, IRoleRepo
    {
        public RoleRepo(SecurityContext securityContext)
            : base(securityContext)
        {

        }

        public IEnumerable<Security.Role.Role> GetUserRoles(string userName)
        {
            //todo: profile this query
            return DbSet.Where(role => role.UserGroups.Any(grp => grp.Logins.Any(user => user.UserName == userName)));
        }

        public IEnumerable<Security.Role.Role> GetCollection()
        {
            return DbSet;
        }

        public ReadableRole GetReadableRole(int functionalAreaId)
        {
            return DbSet.OfType<ReadableRole>().Where(role => role.FunctionalAreaId == functionalAreaId).First();
        }

        public IEnumerable<Security.Role.Role> GetRoles(int functionalAreaId)
        {
            return DbSet.Where(role => role.FunctionalAreaId == functionalAreaId);
        }
    }
}
