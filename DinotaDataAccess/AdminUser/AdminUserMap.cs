using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.AdminUser
{
    public class AdminUserMap : EntityTypeConfiguration<Domain.AdminUser.AdminUser>
    {
        public AdminUserMap()
        {
        }
    }
}
