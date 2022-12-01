using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.MeasurementUnit;

namespace Dinota.DataAccces.MeasurementUnit
{
    public class MeasurementUnitRepo : CrudRepository<Domain.MeasurementUnit.MeasurementUnit>, IMeasurementUnitRepo
    {
        public MeasurementUnitRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
