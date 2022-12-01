using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.Rank
{
    public class RankService : EntityCrudService<Rank, IRankRepo>
    {
        public RankService(IRankRepo repository)
            : base(repository)
        {
        }

    
    }
}
