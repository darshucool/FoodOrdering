
using Dinota.DataAccces.Context;
using Dinota.DataAccces.Role;
using Dinota.Security;
using Dinota.Security.Membership;
using Dinota.Security.Role;


namespace Dinota.DataAccces.Membership
{
    public class RoleProvider : RoleProviderBase
    {
        protected override ISecurityContext GetSecurityContext()
        {
            return new SecurityContext();
        }

        protected override RoleService GetRoleService(ISecurityContext securityContext)
        {
            return new RoleService(new RoleRepo((SecurityContext)securityContext), Configuration.Instance.Container.Resolve<ISecurityCache>());
        }
    
    }
}
