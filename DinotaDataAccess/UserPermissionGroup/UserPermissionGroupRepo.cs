using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.UserPermissionGroup;

namespace Dinota.DataAccces.UserPermissionGroup
{
    public class UserPermissionGroupRepo : CrudRepository<Domain.UserPermissionGroup.UserPermissionGroup>, IUserPermissionGroupRepo
    {
        public UserPermissionGroupRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
