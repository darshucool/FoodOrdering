using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.F140Data
{
    public class F140DataMap : EntityTypeConfiguration<Domain.F140Data.F140Data>
    {
        public F140DataMap()
        {
            ToTable("F140Data");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.MeasurementUnit).WithMany().HasForeignKey(f => f.MeasurementUnitId);
            HasRequired(f => f.MenuItem).WithMany().HasForeignKey(f => f.MenuItemId);
            HasRequired(f => f.IngredientInfo).WithMany().HasForeignKey(f => f.IngridientUId);
            HasRequired(f => f.F140Header).WithMany().HasForeignKey(f => f.F140HeaderUId);
            Property(x => x.Qty).HasPrecision(18, 4);
            Property(x => x.Amount).HasPrecision(18, 4);
        }
    }
}
