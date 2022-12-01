using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.MenuItem;

namespace Dinota.DataAccces.MenuItem
{
    public class MenuItemRepo : CrudRepository<Domain.MenuItem.MenuItem>, IMenuItemRepo
    {
        public MenuItemRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
