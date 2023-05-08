using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.F140Header
{
    public class F140HeaderService : EntityCrudService<F140Header, IF140HeaderRepo>
    {
        public F140HeaderService(IF140HeaderRepo repository)
            : base(repository)
        {
        }

    
    }
}
