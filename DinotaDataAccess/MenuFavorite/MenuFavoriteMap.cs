using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.MenuFavorite
{
    public class MenuFavoriteMap : EntityTypeConfiguration<Domain.MenuFavorite.MenuFavorite>
    {
        public MenuFavoriteMap()
        {
            ToTable("MenuFavorite");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.MenuItem).WithMany().HasForeignKey(f => f.MenuItemUId);
        }
    }
}
