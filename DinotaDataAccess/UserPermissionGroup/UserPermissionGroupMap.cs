using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.UserPermissionGroup
{
    public class UserPermissionGroupMap : EntityTypeConfiguration<Domain.UserPermissionGroup.UserPermissionGroup>
    {
        public UserPermissionGroupMap()
        {
            ToTable("UserPermissionGroup");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
          
        }
    }
}
