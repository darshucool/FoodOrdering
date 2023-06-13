using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.UserStatus
{
    public class UserStatusMap : EntityTypeConfiguration<Domain.UserStatus.UserStatus>
    {
        public UserStatusMap()
        {
            ToTable("UserStatus");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
          
        }
    }
}
