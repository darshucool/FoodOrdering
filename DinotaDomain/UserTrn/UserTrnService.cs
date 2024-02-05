using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.UserTrn
{
    public class UserTrnService : EntityCrudService<UserTrn, IUserTrnRepo>
    {
        public UserTrnService(IUserTrnRepo repository)
            : base(repository)
        {
        }

    
    }
}
