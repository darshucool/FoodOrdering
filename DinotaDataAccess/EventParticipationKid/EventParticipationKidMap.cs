﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;

namespace Dinota.DataAccces.EventParticipationKid
{
    public class EventParticipationKidMap : EntityTypeConfiguration<Domain.EventParticipationKid.EventParticipationKid>
    {
        public EventParticipationKidMap()
        {
            ToTable("EventParticipationKid");
            HasKey(refData => refData.UId);
            Property(refData => refData.UId).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
