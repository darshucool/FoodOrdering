using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.MenuMultiOption;

namespace Dinota.DataAccces.MenuMultiOption
{
    public class MenuMultiOptionRepo : CrudRepository<Domain.MenuMultiOption.MenuMultiOption>, IMenuMultiOptionRepo
    {
        public MenuMultiOptionRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
