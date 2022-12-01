using System.Collections.Generic;
using System.Linq;
using Dinota.Core.Specification;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.UserPermission;

namespace Dinota.DataAccces.UserPermission
{
    public class UserPermissionRepo : CrudRepository<Domain.UserPermission.UserPermission>, IUserPermissionRepo
    {
        public UserPermissionRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }

       
    }
}
