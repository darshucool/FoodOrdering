using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.MenuOrder
{
    public class MenuOrderMap : EntityTypeConfiguration<Domain.MenuOrder.MenuOrder>
    {
        public MenuOrderMap()
        {
            ToTable("MenuOrder");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.MenuItem).WithMany().HasForeignKey(f => f.MenuItemUId);
            HasRequired(f => f.UserBase).WithMany().HasForeignKey(f => f.UserId);
            HasRequired(f => f.MenuOption).WithMany().HasForeignKey(f => f.OptionType1);
            HasRequired(f => f.MenuMultiOption).WithMany().HasForeignKey(f => f.OptionType2);
        }
    }
}
