using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.MenuOption
{
    public class MenuOptionService : EntityCrudService<MenuOption, IMenuOptionRepo>
    {
        public MenuOptionService(IMenuOptionRepo repository)
            : base(repository)
        {
        }

    
    }
}
