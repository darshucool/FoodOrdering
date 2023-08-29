using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.SetMenuHeader;

namespace Dinota.DataAccces.SetMenuHeader
{
    public class SetMenuHeaderRepo : CrudRepository<Domain.SetMenuHeader.SetMenuHeader>, ISetMenuHeaderRepo
    {
        public SetMenuHeaderRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
