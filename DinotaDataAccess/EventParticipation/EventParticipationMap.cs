using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.EventParticipation
{
    public class EventParticipationMap : EntityTypeConfiguration<Domain.EventParticipation.EventParticipation>
    {
        public EventParticipationMap()
        {
            ToTable("EventParticipation");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasRequired(f => f.UserBase).WithMany().HasForeignKey(f => f.UserId);
        }
    }
}
