using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.EventCount
{
    public class EventCountService : EntityCrudService<EventCount, IEventCountRepo>
    {
        public EventCountService(IEventCountRepo repository)
            : base(repository)
        {
        }

    
    }
}
