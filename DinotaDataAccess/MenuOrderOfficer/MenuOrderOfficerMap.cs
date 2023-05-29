using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.MenuOrderOfficer
{
    public class MenuOrderOfficerMap : EntityTypeConfiguration<Domain.MenuOrderOfficer.MenuOrderOfficer>
    {
        public MenuOrderOfficerMap()
        {
            ToTable("MenuOrderOfficer");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.UserBase).WithMany().HasForeignKey(f => f.UserId);
            HasRequired(f => f.MenuOrderHeader).WithMany().HasForeignKey(f => f.MeanuOrderHeaderUId);
        }
    }
}
