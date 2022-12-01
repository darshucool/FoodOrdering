using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.Rank;

namespace Dinota.DataAccces.Rank
{
    public class RankRepo : CrudRepository<Domain.Rank.Rank>, IRankRepo
    {
        public RankRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
