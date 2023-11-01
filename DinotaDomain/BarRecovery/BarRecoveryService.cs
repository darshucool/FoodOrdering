using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.BarRecovery
{
    public class BarRecoveryService : EntityCrudService<BarRecovery, IBarRecoveryRepo>
    {
        public BarRecoveryService(IBarRecoveryRepo repository)
            : base(repository)
        {
        }

    
    }
}
