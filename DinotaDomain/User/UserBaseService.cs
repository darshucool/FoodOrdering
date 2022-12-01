using System.Collections.Generic;
using Dinota.Core.Data;
using Dinota.Core.Extensions;

namespace Dinota.Domain.User
{
    public class UserBaseService : EntityCrudService<UserBase, IUserBaseRepo>
    {
        public UserBaseService(IUserBaseRepo repository) : base(repository)
        {
        }

    }
}
