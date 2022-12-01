using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.AdminUser;

namespace Dinota.DataAccces.AdminUser
{
    public class AdminUserRepo : CrudRepository<Domain.AdminUser.AdminUser>, IAdminUserRepo
    {
        public AdminUserRepo(DomainContext domainContext) : base(domainContext)
        {
        }
    }
}
