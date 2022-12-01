using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.UserType;

namespace Dinota.DataAccces.UserType
{
    public class UserTypeRepo : CrudRepository<Domain.UserType.UserType>, IUserTypeRepo
    {
        public UserTypeRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }


    }
}
