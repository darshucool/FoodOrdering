using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.RoomInfo
{
    public class RoomInfoService : EntityCrudService<RoomInfo, IRoomInfoRepo>
    {
        public RoomInfoService(IRoomInfoRepo repository)
            : base(repository)
        {
        }

    
    }
}
