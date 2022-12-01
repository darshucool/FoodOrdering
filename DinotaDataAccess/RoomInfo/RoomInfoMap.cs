using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.RoomInfo
{
    public class RoomInfoMap : EntityTypeConfiguration<Domain.RoomInfo.RoomInfo>
    {
        public RoomInfoMap()
        {
            ToTable("RoomInfo");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.Room).WithMany().HasForeignKey(f => f.RoomUId);
            HasRequired(f => f.UserBase).WithMany().HasForeignKey(f => f.UserUId);
        }
    }
}
