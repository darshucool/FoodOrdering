using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.MenuOrderHeader;

namespace Dinota.DataAccces.MenuOrderHeader
{
    public class MenuOrderHeaderRepo : CrudRepository<Domain.MenuOrderHeader.MenuOrderHeader>, IMenuOrderHeaderRepo
    {
        public MenuOrderHeaderRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
