using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.MenuPackage
{
    public class MenuPackageService : EntityCrudService<MenuPackage, IMenuPackageRepo>
    {
        public MenuPackageService(IMenuPackageRepo repository)
            : base(repository)
        {
        }

    
    }
}
