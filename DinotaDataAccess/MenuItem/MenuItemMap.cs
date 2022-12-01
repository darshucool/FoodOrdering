using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.MenuItem
{
    public class MenuItemMap : EntityTypeConfiguration<Domain.MenuItem.MenuItem>
    {
        public MenuItemMap()
        {
            ToTable("MenuItem");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.MenuCategory).WithMany().HasForeignKey(f => f.MenuCategoryUId);
        }
    }
}
