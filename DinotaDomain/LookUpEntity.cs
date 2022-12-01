using System.ComponentModel.DataAnnotations;
using Dinota.Core.Data;
using Dinota.Core.Extensions;

namespace Dinota.Domain
{
    public abstract class LookUpEntity : Entity
    {
        public int Uid { get; set; }

        private string _id;

        [StringLength(64)]
        public string ID
        {
            get
            {
                return _id;
            }

            set
            {
                if (!value.IsNullOrWhiteSpace())
                {
                    _id = value.Trim();
                }
            }
        }

        [Required]
        [StringLength(50)]
        [Display(Name = "Description")]
        public string Name { get; set; }
    }
}
