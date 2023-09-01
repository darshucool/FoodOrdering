using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.PaymentInfo
{
    public class PaymentInfoMap : EntityTypeConfiguration<Domain.PaymentInfo.PaymentInfo>
    {
        public PaymentInfoMap()
        {
            ToTable("PaymentInfo");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
          
        }
    }
}
