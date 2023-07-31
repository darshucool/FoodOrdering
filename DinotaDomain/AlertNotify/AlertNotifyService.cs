using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.AlertNotify
{
    public class AlertNotifyService : EntityCrudService<AlertNotify, IAlertNotifyRepo>
    {
        public AlertNotifyService(IAlertNotifyRepo repository)
            : base(repository)
        {
        }

    
    }
}
