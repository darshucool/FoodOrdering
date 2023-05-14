using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.MenuOrderHeader
{
    public class MenuOrderHeaderService : EntityCrudService<MenuOrderHeader, IMenuOrderHeaderRepo>
    {
        public MenuOrderHeaderService(IMenuOrderHeaderRepo repository)
            : base(repository)
        {
        }

    
    }
}
