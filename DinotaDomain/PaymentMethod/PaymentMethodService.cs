using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.PaymentMethod
{
    public class PaymentMethodService : EntityCrudService<PaymentMethod, IPaymentMethodRepo>
    {
        public PaymentMethodService(IPaymentMethodRepo repository)
            : base(repository)
        {
        }

    
    }
}
