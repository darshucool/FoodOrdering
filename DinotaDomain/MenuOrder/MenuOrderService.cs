using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.MenuOrder
{
    public class MenuOrderService : EntityCrudService<MenuOrder, IMenuOrderRepo>
    {
        public MenuOrderService(IMenuOrderRepo repository)
            : base(repository)
        {
        }

    
    }
}
