using Dinota.DataAccces.Context;
using Dinota.DataAccces.Repository;
using Dinota.Security.Group;
using Dinota.Core.Data;


namespace Dinota.DataAccces.Group
{
    public class GroupRepo : CrudRepository<Security.Group.Group>, IGroupRepo
    {
        public GroupRepo(SecurityContext securityContext)
            : base(securityContext)
        {
        }
    }
}
