using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.UserType
{
    public class UserTypeService : EntityCrudService<UserType, IUserTypeRepo>
    {
        public UserTypeService(IUserTypeRepo repository)
            : base(repository)
        {
        }

    
    }
}
