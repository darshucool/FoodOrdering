using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.Town
{
    public class TownService : EntityCrudService<Town, ITownRepo>
    {
        public TownService(ITownRepo repository)
            : base(repository)
        {
        }

    
    }
}
