using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.AlertNotify
{
    public class AlertNotifyMap : EntityTypeConfiguration<Domain.AlertNotify.AlertNotify>
    {
        public AlertNotifyMap()
        {
            ToTable("AlertNotify");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
