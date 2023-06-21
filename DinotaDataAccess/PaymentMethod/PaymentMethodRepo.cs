using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.PaymentMethod;

namespace Dinota.DataAccces.PaymentMethod
{
    public class PaymentMethodRepo : CrudRepository<Domain.PaymentMethod.PaymentMethod>, IPaymentMethodRepo
    {
        public PaymentMethodRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
