using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.SupplierInvoice;

namespace Dinota.DataAccces.SupplierInvoice
{
    public class SupplierInvoiceRepo : CrudRepository<Domain.SupplierInvoice.SupplierInvoice>, ISupplierInvoiceRepo
    {
        public SupplierInvoiceRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
