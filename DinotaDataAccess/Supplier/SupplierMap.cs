using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.Supplier
{
    public class SupplierMap : EntityTypeConfiguration<Domain.Supplier.Supplier>
    {
        public SupplierMap()
        {
            ToTable("Supplier");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
          
        }
    }
}
