using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.District;

namespace Dinota.DataAccces.District
{
    public class DistrictRepo : CrudRepository<Domain.District.District>, IDistrictRepo
    {
        public DistrictRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
