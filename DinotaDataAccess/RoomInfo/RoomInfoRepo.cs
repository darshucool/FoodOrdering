using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.RoomInfo;

namespace Dinota.DataAccces.RoomInfo
{
    public class RoomInfoRepo : CrudRepository<Domain.RoomInfo.RoomInfo>, IRoomInfoRepo
    {
        public RoomInfoRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
