using Dinota.Core.Data;
using Dinota.Core.Extensions;

namespace Dinota.Domain.User
{
    public class SystemUserInfoService : EntityService<SystemUserInfo, ISystemUserInfoRepo>
    {
        public SystemUserInfoService(ISystemUserInfoRepo repository) : base(repository)
        {

        }
    }
}
