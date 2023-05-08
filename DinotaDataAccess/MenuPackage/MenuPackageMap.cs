using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.MenuPackage
{
    public class MenuPackageMap : EntityTypeConfiguration<Domain.MenuPackage.MenuPackage>
    {
        public MenuPackageMap()
        {
            ToTable("MenuPackage");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.MenuItem).WithMany().HasForeignKey(f => f.CombinedMenuItemId);
        }
    }
}
