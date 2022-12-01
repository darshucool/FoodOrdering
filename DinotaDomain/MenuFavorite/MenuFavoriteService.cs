using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.MenuFavorite
{
    public class MenuFavoriteService : EntityCrudService<MenuFavorite, IMenuFavoriteRepo>
    {
        public MenuFavoriteService(IMenuFavoriteRepo repository)
            : base(repository)
        {
        }

    
    }
}
