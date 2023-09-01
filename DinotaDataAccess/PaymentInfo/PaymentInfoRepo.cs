using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.PaymentInfo;

namespace Dinota.DataAccces.PaymentInfo
{
    public class PaymentInfoRepo : CrudRepository<Domain.PaymentInfo.PaymentInfo>, IPaymentInfoRepo
    {
        public PaymentInfoRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
