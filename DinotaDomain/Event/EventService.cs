using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.Event
{
    public class EventService : EntityCrudService<Event, IEventRepo>
    {
        public EventService(IEventRepo repository)
            : base(repository)
        {
        }

    
    }
}
