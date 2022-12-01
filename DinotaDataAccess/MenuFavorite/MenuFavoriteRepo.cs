using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.MenuFavorite;

namespace Dinota.DataAccces.MenuFavorite
{
    public class MenuFavoriteRepo : CrudRepository<Domain.MenuFavorite.MenuFavorite>, IMenuFavoriteRepo
    {
        public MenuFavoriteRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
