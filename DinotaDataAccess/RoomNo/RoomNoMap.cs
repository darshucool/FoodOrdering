using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.RoomNo
{
    public class RoomNoMap : EntityTypeConfiguration<Domain.RoomNo.RoomNo>
    {
        public RoomNoMap()
        {
            ToTable("RoomNo");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
