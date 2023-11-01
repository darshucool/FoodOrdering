using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.BarRecovery;

namespace Dinota.DataAccces.BarRecovery
{
    public class BarRecoveryRepo : CrudRepository<Domain.BarRecovery.BarRecovery>, IBarRecoveryRepo
    {
        public BarRecoveryRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
