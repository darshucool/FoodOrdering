using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.MeasurementUnit
{
    public class MeasurementUnitService : EntityCrudService<MeasurementUnit, IMeasurementUnitRepo>
    {
        public MeasurementUnitService(IMeasurementUnitRepo repository)
            : base(repository)
        {
        }

    
    }
}
