using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.FuelType
{
    public class FuelTypeMap : EntityTypeConfiguration<Domain.FuelType.FuelType>
    {
        public FuelTypeMap()
        {
            ToTable("FuelType");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
