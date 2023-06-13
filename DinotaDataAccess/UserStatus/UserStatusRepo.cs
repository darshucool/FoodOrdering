using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.UserStatus;

namespace Dinota.DataAccces.UserStatus
{
    public class UserStatusRepo : CrudRepository<Domain.UserStatus.UserStatus>, IUserStatusRepo
    {
        public UserStatusRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
