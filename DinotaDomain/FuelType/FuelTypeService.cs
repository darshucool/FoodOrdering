using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.FuelType
{
    public class FuelTypeService : EntityCrudService<FuelType, IFuelTypeRepo>
    {
        public FuelTypeService(IFuelTypeRepo repository)
            : base(repository)
        {
        }

    
    }
}
