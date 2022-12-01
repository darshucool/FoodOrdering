using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.EventParticipation;

namespace Dinota.DataAccces.EventParticipation
{
    public class EventParticipationRepo : CrudRepository<Domain.EventParticipation.EventParticipation>, IEventParticipationRepo
    {
        public EventParticipationRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
