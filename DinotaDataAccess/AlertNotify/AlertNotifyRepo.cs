using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.AlertNotify;

namespace Dinota.DataAccces.AlertNotify
{
    public class AlertNotifyRepo : CrudRepository<Domain.AlertNotify.AlertNotify>, IAlertNotifyRepo
    {
        public AlertNotifyRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
