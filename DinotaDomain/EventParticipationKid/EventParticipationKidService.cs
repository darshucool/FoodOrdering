using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.EventParticipationKid
{
    public class EventParticipationKidService : EntityCrudService<EventParticipationKid, IEventParticipationKidRepo>
    {
        public EventParticipationKidService(IEventParticipationKidRepo repository)
            : base(repository)
        {
        }

    
    }
}
