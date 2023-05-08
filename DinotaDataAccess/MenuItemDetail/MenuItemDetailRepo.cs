using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.MenuItemDetail;

namespace Dinota.DataAccces.MenuItemDetail
{
    public class MenuItemDetailRepo : CrudRepository<Domain.MenuItemDetail.MenuItemDetail>, IMenuItemDetailRepo
    {
        public MenuItemDetailRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
