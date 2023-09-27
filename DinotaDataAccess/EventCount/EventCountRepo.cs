using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.EventCount;

namespace Dinota.DataAccces.EventCount
{
    public class EventCountRepo : CrudRepository<Domain.EventCount.EventCount>, IEventCountRepo
    {
        public EventCountRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
