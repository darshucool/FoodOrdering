using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.MeasurementUnit
{
    public class MeasurementUnitMap : EntityTypeConfiguration<Domain.MeasurementUnit.MeasurementUnit>
    {
        public MeasurementUnitMap()
        {
            ToTable("MeasurementUnit");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
