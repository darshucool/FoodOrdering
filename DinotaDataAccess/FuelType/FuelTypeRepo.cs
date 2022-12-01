using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.FuelType;

namespace Dinota.DataAccces.FuelType
{
    public class FuelTypeRepo : CrudRepository<Domain.FuelType.FuelType>, IFuelTypeRepo
    {
        public FuelTypeRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
