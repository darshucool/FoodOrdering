using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.EventParticipationKid;

namespace Dinota.DataAccces.EventParticipationKid
{
    public class EventParticipationKidRepo : CrudRepository<Domain.EventParticipationKid.EventParticipationKid>, IEventParticipationKidRepo
    {
        public EventParticipationKidRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
