using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.Event;

namespace Dinota.DataAccces.Event
{
    public class EventRepo : CrudRepository<Domain.Event.Event>, IEventRepo
    {
        public EventRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
