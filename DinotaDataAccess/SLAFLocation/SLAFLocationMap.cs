using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.SLAFLocation
{
    public class SLAFLocationMap : EntityTypeConfiguration<Domain.SLAFLocation.SLAFLocation>
    {
        public SLAFLocationMap()
        {
            ToTable("SLAFLocation");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
