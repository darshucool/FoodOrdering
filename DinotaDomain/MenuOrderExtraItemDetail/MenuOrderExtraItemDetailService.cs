using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.MenuOrderExtraItemDetail
{
    public class MenuOrderExtraItemDetailService : EntityCrudService<MenuOrderExtraItemDetail, IMenuOrderExtraItemDetailRepo>
    {
        public MenuOrderExtraItemDetailService(IMenuOrderExtraItemDetailRepo repository)
            : base(repository)
        {
        }

    
    }
}
