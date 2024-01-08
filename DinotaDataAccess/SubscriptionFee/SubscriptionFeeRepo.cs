using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.SubscriptionFee;

namespace Dinota.DataAccces.SubscriptionFee
{
    public class SubscriptionFeeRepo : CrudRepository<Domain.SubscriptionFee.SubscriptionFee>, ISubscriptionFeeRepo
    {
        public SubscriptionFeeRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
