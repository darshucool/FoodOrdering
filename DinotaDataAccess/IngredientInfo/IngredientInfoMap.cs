using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.IngredientInfo
{
    public class IngredientInfoMap : EntityTypeConfiguration<Domain.IngredientInfo.IngredientInfo>
    {
        public IngredientInfoMap()
        {
            ToTable("IngredientInfo");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.MeasurementUnit).WithMany().HasForeignKey(f => f.MeasurementUnitUId);

        }
    }
}
