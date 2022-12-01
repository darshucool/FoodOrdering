using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.EventAttendance;

namespace Dinota.DataAccces.EventAttendance
{
    public class EventAttendanceRepo : CrudRepository<Domain.EventAttendance.EventAttendance>, IEventAttendanceRepo
    {
        public EventAttendanceRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
