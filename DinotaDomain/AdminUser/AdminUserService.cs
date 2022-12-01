using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.Core.Data;

namespace Dinota.Domain.AdminUser
{
    public class AdminUserService : EntityCrudService<AdminUser,IAdminUserRepo>
    {
        public AdminUserService(IAdminUserRepo repository) : base(repository)
        {
        }
    }
}
