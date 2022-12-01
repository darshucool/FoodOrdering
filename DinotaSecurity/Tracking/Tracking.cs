using System.ComponentModel.DataAnnotations;
using Dinota.Core.Data;
using Dinota.Domain.User;

namespace Dinota.Security.Tracking
{
    public class Tracking : Entity
    {
        public int Uid { get; set; }

        public virtual UserBase User { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
