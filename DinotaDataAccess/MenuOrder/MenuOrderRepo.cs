using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.MenuOrder;

namespace Dinota.DataAccces.MenuOrder
{
    public class MenuOrderRepo : CrudRepository<Domain.MenuOrder.MenuOrder>, IMenuOrderRepo
    {
        public MenuOrderRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
