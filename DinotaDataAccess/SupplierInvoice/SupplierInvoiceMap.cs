using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.SupplierInvoice
{
    public class SupplierInvoiceMap : EntityTypeConfiguration<Domain.SupplierInvoice.SupplierInvoice>
    {
        public SupplierInvoiceMap()
        {
            ToTable("SupplierInvoice");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.Supplier).WithMany().HasForeignKey(f => f.SupplierUId);

        }
    }
}
