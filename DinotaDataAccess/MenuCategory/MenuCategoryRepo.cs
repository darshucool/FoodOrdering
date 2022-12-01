using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.MenuCategory;

namespace Dinota.DataAccces.MenuCategory
{
    public class MenuCategoryRepo : CrudRepository<Domain.MenuCategory.MenuCategory>, IMenuCategoryRepo
    {
        public MenuCategoryRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
