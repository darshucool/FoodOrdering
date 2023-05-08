using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.F140Data
{
    public class F140DataService : EntityCrudService<F140Data, IF140DataRepo>
    {
        public F140DataService(IF140DataRepo repository)
            : base(repository)
        {
        }

    
    }
}
