using Dinota.Core.Data;


namespace Dinota.Security.Group
{
    public class GroupService : EntityCrudService<Group, IGroupRepo>
    {
        private ISecurityCache _securityCache;

        public GroupService(IGroupRepo repository, ISecurityCache securityCache) : base(repository)
        {
            _securityCache = securityCache;
        }
    }
}
