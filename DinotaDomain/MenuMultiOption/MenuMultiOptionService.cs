using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.MenuMultiOption
{
    public class MenuMultiOptionService : EntityCrudService<MenuMultiOption, IMenuMultiOptionRepo>
    {
        public MenuMultiOptionService(IMenuMultiOptionRepo repository)
            : base(repository)
        {
        }

    
    }
}
