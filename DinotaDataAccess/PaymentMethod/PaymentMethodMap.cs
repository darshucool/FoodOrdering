using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.PaymentMethod
{
    public class PaymentMethodMap : EntityTypeConfiguration<Domain.PaymentMethod.PaymentMethod>
    {
        public PaymentMethodMap()
        {
            ToTable("PaymentMethod");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
          
        }
    }
}
