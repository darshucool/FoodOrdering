using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dinota.Core.Data;

namespace Dinota.Security.FunctionalArea
{
    public class FunctionalArea : Entity
    {
        public short Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Role.Role> Roles { get; set; }
    
    }
}
