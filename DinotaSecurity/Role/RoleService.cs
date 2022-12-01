using System.Collections.Generic;
using System.Linq;
using Dinota.Core.Data;
using Dinota.Core.Specification;

namespace Dinota.Security.Role
{
    public class RoleService : EntityService<Role, IRoleRepo>
    {
        private ISecurityCache _securityCache;

        public RoleService(IRoleRepo repository, ISecurityCache securityCache)
            : base(repository)
        {
            _securityCache = securityCache;
        }

        public IEnumerable<Role> GetUserRoles(string userName)
        {
            return Repository.GetUserRoles(userName).ToList();
        }

        public List<Role> GetRoles()
        {
            return Repository.GetCollection().ToList();
        }

        public override ISpecification<Role> GetDefaultSpecification()
        {
            return new Specification<Role>(r => true);
        }

        public ReadableRole GetReadableRole(int functionalAreaId)
        {
            return Repository.GetReadableRole(functionalAreaId);
        }

        public IEnumerable<Role> GetRoles(int functionalAreaId)
        {
            return Repository.GetRoles(functionalAreaId);
        }

    }
}
