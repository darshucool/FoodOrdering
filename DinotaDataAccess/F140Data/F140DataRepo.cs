using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.F140Data;

namespace Dinota.DataAccces.F140Data
{
    public class F140DataRepo : CrudRepository<Domain.F140Data.F140Data>, IF140DataRepo
    {
        public F140DataRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
