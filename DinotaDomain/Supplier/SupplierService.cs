using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.Supplier
{
    public class SupplierService : EntityCrudService<Supplier, ISupplierRepo>
    {
        public SupplierService(ISupplierRepo repository)
            : base(repository)
        {
        }

    
    }
}
