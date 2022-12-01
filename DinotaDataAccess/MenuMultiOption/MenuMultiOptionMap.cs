using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.MenuMultiOption
{
    public class MenuMultiOptionMap : EntityTypeConfiguration<Domain.MenuMultiOption.MenuMultiOption>
    {
        public MenuMultiOptionMap()
        {
            ToTable("MenuMultiOption");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
