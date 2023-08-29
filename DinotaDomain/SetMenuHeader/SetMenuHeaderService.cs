using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.SetMenuHeader
{
    public class SetMenuHeaderService : EntityCrudService<SetMenuHeader, ISetMenuHeaderRepo>
    {
        public SetMenuHeaderService(ISetMenuHeaderRepo repository)
            : base(repository)
        {
        }

    
    }
}
