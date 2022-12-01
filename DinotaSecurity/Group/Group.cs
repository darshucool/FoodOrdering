using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dinota.Core.Data;


namespace Dinota.Security.Group
{
    public class Group : Entity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public string Name { get; set; }

        public virtual ICollection<Login.Login> Logins { get; set; }

        public virtual ICollection<Role.Role> Roles { get; set; }

        public override bool ExpireOnDelete
        {
            get
            {
                return false;
            }
        }

    }
}
