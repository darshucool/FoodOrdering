using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.UserTrn;

namespace Dinota.DataAccces.UserTrn
{
    public class UserTrnRepo : CrudRepository<Domain.UserTrn.UserTrn>, IUserTrnRepo
    {
        public UserTrnRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
