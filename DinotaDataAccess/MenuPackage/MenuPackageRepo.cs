using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.MenuPackage;

namespace Dinota.DataAccces.MenuPackage
{
    public class MenuPackageRepo : CrudRepository<Domain.MenuPackage.MenuPackage>, IMenuPackageRepo
    {
        public MenuPackageRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
