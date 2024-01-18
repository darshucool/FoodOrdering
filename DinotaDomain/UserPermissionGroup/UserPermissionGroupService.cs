using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.UserPermissionGroup
{
    public class UserPermissionGroupService : EntityCrudService<UserPermissionGroup, IUserPermissionGroupRepo>
    {
        public UserPermissionGroupService(IUserPermissionGroupRepo repository)
            : base(repository)
        {
        }

    
    }
}
