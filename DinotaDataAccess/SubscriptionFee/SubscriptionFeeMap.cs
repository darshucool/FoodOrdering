using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.SubscriptionFee
{
    public class SubscriptionFeeMap : EntityTypeConfiguration<Domain.SubscriptionFee.SubscriptionFee>
    {
        public SubscriptionFeeMap()
        {
            ToTable("SubscriptionFee");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
          
        }
    }
}
