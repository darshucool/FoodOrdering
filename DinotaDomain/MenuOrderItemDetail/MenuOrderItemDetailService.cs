using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.MenuOrderItemDetail
{
    public class MenuOrderItemDetailService : EntityCrudService<MenuOrderItemDetail, IMenuOrderItemDetailRepo>
    {
        public MenuOrderItemDetailService(IMenuOrderItemDetailRepo repository)
            : base(repository)
        {
        }

    
    }
}
