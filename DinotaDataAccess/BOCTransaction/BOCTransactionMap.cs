using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.BOCTransaction
{
    public class BOCTransactionMap : EntityTypeConfiguration<Domain.BOCTransaction.BOCTransaction>
    {
        public BOCTransactionMap()
        {
            ToTable("BOCTransaction");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.MenuOrderHeader).WithMany().HasForeignKey(f => f.MenuOrderUId);
            HasRequired(f => f.IngredientBOC).WithMany().HasForeignKey(f => f.IngriedientBOCUId);
            Property(x => x.IssueStock).HasPrecision(18, 4);
            Property(x => x.PresentStock).HasPrecision(18, 4);
            Property(x => x.RemainingStock).HasPrecision(18, 4);


        }
    }
}
