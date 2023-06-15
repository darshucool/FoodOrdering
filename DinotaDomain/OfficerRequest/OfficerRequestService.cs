using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.OfficerRequest
{
    public class OfficerRequestService : EntityCrudService<OfficerRequest, IOfficerRequestRepo>
    {
        public OfficerRequestService(IOfficerRequestRepo repository)
            : base(repository)
        {
        }

    
    }
}
