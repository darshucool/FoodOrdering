using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.RoomNo;

namespace Dinota.DataAccces.RoomNo
{
    public class RoomNoRepo : CrudRepository<Domain.RoomNo.RoomNo>, IRoomNoRepo
    {
        public RoomNoRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
