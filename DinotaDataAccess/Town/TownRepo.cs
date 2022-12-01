using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.Town;

namespace Dinota.DataAccces.Town
{
    public class TownRepo : CrudRepository<Domain.Town.Town>, ITownRepo
    {
        public TownRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
