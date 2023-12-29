using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.Supplier;

namespace Dinota.DataAccces.Supplier
{
    public class SupplierRepo : CrudRepository<Domain.Supplier.Supplier>, ISupplierRepo
    {
        public SupplierRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
