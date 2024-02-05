using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.UserTrn
{
    public class UserTrnMap : EntityTypeConfiguration<Domain.UserTrn.UserTrn>
    {
        public UserTrnMap()
        {
            ToTable("UserTrn");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.UserStatus).WithMany().HasForeignKey(f => f.StatusId);

        }
    }
}
