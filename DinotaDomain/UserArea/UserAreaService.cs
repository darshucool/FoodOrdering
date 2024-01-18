using Dinota.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dinota.Domain.UserArea
{
    public class UserAreaService : EntityCrudService<UserArea, IUserAreaRepo>
    {
        public UserAreaService(IUserAreaRepo repository)
            : base(repository)
        {
        }

    
    }
}
