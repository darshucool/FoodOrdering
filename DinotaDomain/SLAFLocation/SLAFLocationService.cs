using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.SLAFLocation
{
    public class SLAFLocationService : EntityCrudService<SLAFLocation, ISLAFLocationRepo>
    {
        public SLAFLocationService(ISLAFLocationRepo repository)
            : base(repository)
        {

        }

    
    }
}
