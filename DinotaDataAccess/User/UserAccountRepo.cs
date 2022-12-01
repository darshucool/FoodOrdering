using System.Collections.Generic;
using System.Linq;
using Dinota.Core.Specification;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.User;

namespace Dinota.DataAccces.User
{
    public class UserAccountRepo : CrudRepository<UserAccount>, IUserAccountRepo
    {
        public UserAccountRepo(DomainContext domainContext) : base(domainContext)
        {
        }

    }
}
