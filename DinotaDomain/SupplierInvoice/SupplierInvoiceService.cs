using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.SupplierInvoice
{
    public class SupplierInvoiceService : EntityCrudService<SupplierInvoice, ISupplierInvoiceRepo>
    {
        public SupplierInvoiceService(ISupplierInvoiceRepo repository)
            : base(repository)
        {
        }

    
    }
}
