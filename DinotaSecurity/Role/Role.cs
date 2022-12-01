using System.Collections.Generic;
using Dinota.Core.Data;

namespace Dinota.Security.Role
{
    public abstract class Role : Entity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public short FunctionalAreaId { get; set; }

        public virtual FunctionalArea.FunctionalArea FunctionalArea { get; set; }

        public virtual ICollection<Group.Group> UserGroups { get; set; }
    
    }
}
