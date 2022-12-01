using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.RoomNo
{
    public class RoomNoService : EntityCrudService<RoomNo, IRoomNoRepo>
    {
        public RoomNoService(IRoomNoRepo repository)
            : base(repository)
        {
        }

    
    }
}
