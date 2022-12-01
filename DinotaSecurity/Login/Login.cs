using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Dinota.Core.Data;

namespace Dinota.Security.Login
{
    public class Login : Entity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        public string UserName { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public string PasswordHash { get; set; }

        public DateTime? LastActiveDate { get; set; }

        public virtual ICollection<Group.Group> Groups { get; set; }

        public override bool ExpireOnDelete
        {
            get
            {
                return false;
            }
        }

    }
}
