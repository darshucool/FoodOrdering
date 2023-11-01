using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.MenuOrderHeader
{
    public class MenuOrderHeaderMap : EntityTypeConfiguration<Domain.MenuOrderHeader.MenuOrderHeader>
    {
        public MenuOrderHeaderMap()
        {
            ToTable("MenuOrderHeader");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.UserBase).WithMany().HasForeignKey(f => f.CreatedUserId);
        }
    }
}
