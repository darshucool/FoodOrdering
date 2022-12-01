using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.MenuOption
{
    public class MenuOptionMap : EntityTypeConfiguration<Domain.MenuOption.MenuOption>
    {
        public MenuOptionMap()
        {
            ToTable("MenuOption");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
