using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.SetMenuDetail
{
    public class SetMenuDetailService : EntityCrudService<SetMenuDetail, ISetMenuDetailRepo>
    {
        public SetMenuDetailService(ISetMenuDetailRepo repository)
            : base(repository)
        {
        }

    
    }
}
