using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.MenuCategory
{
    public class MenuCategoryService : EntityCrudService<MenuCategory, IMenuCategoryRepo>
    {
        public MenuCategoryService(IMenuCategoryRepo repository)
            : base(repository)
        {
        }

    
    }
}
