using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.PaymentInfo
{
    public class PaymentInfoService : EntityCrudService<PaymentInfo, IPaymentInfoRepo>
    {
        public PaymentInfoService(IPaymentInfoRepo repository)
            : base(repository)
        {
        }

    
    }
}
