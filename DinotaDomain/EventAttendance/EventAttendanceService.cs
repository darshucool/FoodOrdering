using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.EventAttendance
{
    public class EventAttendanceService : EntityCrudService<EventAttendance, IEventAttendanceRepo>
    {
        public EventAttendanceService(IEventAttendanceRepo repository)
            : base(repository)
        {
        }

    
    }
}
