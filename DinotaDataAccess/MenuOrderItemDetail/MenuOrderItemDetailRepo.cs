using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.MenuOrderItemDetail;

namespace Dinota.DataAccces.MenuOrderItemDetail
{
    public class MenuOrderItemDetailRepo : CrudRepository<Domain.MenuOrderItemDetail.MenuOrderItemDetail>, IMenuOrderItemDetailRepo
    {
        public MenuOrderItemDetailRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
