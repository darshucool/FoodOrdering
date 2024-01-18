using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Domain.UserArea;

namespace Dinota.DataAccces.UserArea
{
    public class UserAreaRepo : CrudRepository<Domain.UserArea.UserArea>, IUserAreaRepo
    {
        public UserAreaRepo(DomainContext domainContext)
            : base(domainContext)
        {
        }
    }
}
