using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.MenuCategory
{
    public class MenuCategoryMap : EntityTypeConfiguration<Domain.MenuCategory.MenuCategory>
    {
        public MenuCategoryMap()
        {
            ToTable("MenuCategory");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
