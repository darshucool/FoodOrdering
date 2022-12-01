using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.EventParticipation
{
    public class EventParticipationService : EntityCrudService<EventParticipation, IEventParticipationRepo>
    {
        public EventParticipationService(IEventParticipationRepo repository)
            : base(repository)
        {
        }

    
    }
}
