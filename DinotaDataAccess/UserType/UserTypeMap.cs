using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.UserType
{
    public class UserTypeMap : EntityTypeConfiguration<Domain.UserType.UserType>
    {
        public UserTypeMap()
        {
            ToTable("UserType");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.Division).WithMany().HasForeignKey(f => f.DivisionId);
        }
    }
}
