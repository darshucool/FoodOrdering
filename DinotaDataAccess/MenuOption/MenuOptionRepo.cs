using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.MenuOption;

namespace Dinota.DataAccces.MenuOption
{
    public class MenuOptionRepo : CrudRepository<Domain.MenuOption.MenuOption>, IMenuOptionRepo
    {
        public MenuOptionRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
