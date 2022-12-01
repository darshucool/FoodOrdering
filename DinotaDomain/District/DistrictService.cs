using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.District
{
    public class DistrictService : EntityCrudService<District, IDistrictRepo>
    {
        public DistrictService(IDistrictRepo repository)
            : base(repository)
        {
        }

    
    }
}
