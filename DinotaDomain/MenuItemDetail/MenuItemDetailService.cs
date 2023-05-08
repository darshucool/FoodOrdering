using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.MenuItemDetail
{
    public class MenuItemDetailService : EntityCrudService<MenuItemDetail, IMenuItemDetailRepo>
    {
        public MenuItemDetailService(IMenuItemDetailRepo repository)
            : base(repository)
        {
        }

    
    }
}
