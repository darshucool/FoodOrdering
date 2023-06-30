using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.MenuOrderExtraItemDetail;

namespace Dinota.DataAccces.MenuOrderExtraItemDetail
{
    public class MenuOrderExtraItemDetailRepo : CrudRepository<Domain.MenuOrderExtraItemDetail.MenuOrderExtraItemDetail>, IMenuOrderExtraItemDetailRepo
    {
        public MenuOrderExtraItemDetailRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
