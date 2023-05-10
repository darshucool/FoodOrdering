using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.IngredientBOC
{
    public class IngredientBOCMap : EntityTypeConfiguration<Domain.IngredientBOC.IngredientBOC>
    {
        public IngredientBOCMap()
        {
            ToTable("IngredientBOC");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.IngredientInfo).WithMany().HasForeignKey(f => f.IngredientUId);
            HasRequired(f => f.MeasurementUnit).WithMany().HasForeignKey(f => f.UnitId);
            Property(x => x.Qty).HasPrecision(18, 4);
            Property(x => x.Price).HasPrecision(18, 4);
            Property(x => x.TotalPrice).HasPrecision(18, 4);
        }
    }
}
