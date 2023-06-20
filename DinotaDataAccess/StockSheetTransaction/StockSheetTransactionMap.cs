using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.StockSheetTransaction
{
    public class StockSheetTransactionMap : EntityTypeConfiguration<Domain.StockSheetTransaction.StockSheetTransaction>
    {
        public StockSheetTransactionMap()
        {
            ToTable("StockSheetTransaction");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.IngredientInfo).WithMany().HasForeignKey(f => f.IngredientUId);
        }
    }
}
