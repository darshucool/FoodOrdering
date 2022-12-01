using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.Division;

namespace Dinota.DataAccces.Division
{
    public class DivisionRepo : CrudRepository<Domain.Division.Division>, IDivisionRepo
    {
        public DivisionRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
