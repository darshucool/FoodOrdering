using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.Town
{
    public class TownMap : EntityTypeConfiguration<Domain.Town.Town>
    {
        public TownMap()
        {
            ToTable("Town");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
