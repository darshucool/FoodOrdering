using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.EventCount
{
    public class EventCountMap : EntityTypeConfiguration<Domain.EventCount.EventCount>
    {
        public EventCountMap()
        {
            ToTable("EventCount");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            ;
        }
    }
}
