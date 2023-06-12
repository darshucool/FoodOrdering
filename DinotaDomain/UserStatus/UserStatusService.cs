using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.UserStatus
{
    public class UserStatusService : EntityCrudService<UserStatus, IUserStatusRepo>
    {
        public UserStatusService(IUserStatusRepo repository)
            : base(repository)
        {
        }

    
    }
}
