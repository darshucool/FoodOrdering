using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.MenuItem
{
    public class MenuItemService : EntityCrudService<MenuItem, IMenuItemRepo>
    {
        public MenuItemService(IMenuItemRepo repository)
            : base(repository)
        {
        }

    
    }
}
