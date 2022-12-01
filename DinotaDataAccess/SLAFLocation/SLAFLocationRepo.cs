using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.SLAFLocation;

namespace Dinota.DataAccces.SLAFLocation
{
    public class SLAFLocationRepo : CrudRepository<Domain.SLAFLocation.SLAFLocation>, ISLAFLocationRepo
    {
        public SLAFLocationRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
