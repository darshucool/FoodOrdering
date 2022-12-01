using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.Core.Data;
using Dinota.Domain.Filter;
using Dinota.Core.Specification;

namespace Dinota.Domain.UserPermission
{
    public class UserPermissionService : EntityCrudService<UserPermission, IUserPermissionRepo>
    {
        public UserPermissionService(IUserPermissionRepo repository)
            : base(repository)
        {
        }

    
    }
}
