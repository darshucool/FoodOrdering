using System.Linq;
using Dinota.Security.Group;
using Dinota.Security.Role;

namespace MIMS.Models
{
    public class GrantRoleModel
    {
        public GrantRoleModel(Group @group, int functionAreaId)
        {
            Group = group;
            FunctionAreaId = functionAreaId;
        }

        public Group Group { get; set; }

        public int FunctionAreaId { get; set; }

        public bool HasPermission()
        {
            return Group.Roles.Any(r => r.FunctionalAreaId == FunctionAreaId);
        }

        public bool HasWritePermission()
        {
            return Group.Roles.Any(r => r.FunctionalAreaId == FunctionAreaId && (r is WritableRole));
        }
    }
}