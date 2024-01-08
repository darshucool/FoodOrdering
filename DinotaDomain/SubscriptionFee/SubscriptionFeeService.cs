using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.SubscriptionFee
{
    public class SubscriptionFeeService : EntityCrudService<SubscriptionFee, ISubscriptionFeeRepo>
    {
        public SubscriptionFeeService(ISubscriptionFeeRepo repository)
            : base(repository)
        {
        }

    
    }
}
