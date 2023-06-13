using System;
using System.ComponentModel.DataAnnotations;
using Dinota.Core.Data;

namespace Dinota.Domain.User
{
    public abstract class UserBase : Entity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(32)]
        [Display(Name = "Username")]
        public string UserName { get; set; }
        
        [Required]
        [StringLength(128)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(64)]
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [StringLength(64)]
        [Display(Name = "Telephone")]
        public string Telephone1 { get; set; }

        public int RankUId { get; set; }
        public string ServiceNo { get; set; }
        public int DivisionId { get; set; }
        public int UserTypeId { get; set; }
        public int LocationUId { get; set; }
        public int? UserMode { get; set; }
        public string NIC { get; set; }
        //public byte TypeEnum { get; set; }

        public string PasswordHash { get; set; }
      
        public DateTime? LastActiveDate { get; set; }
        [Display(Name = "This supplier is a fabricator as well")]
        public bool IsFabUser { get; set; }
        public bool Active { get; set; }
        public int LivingStatus { get; set; }
        public bool IsFirstLogin { get; set; }
        public UserType.UserType UserType { get; set; }
        public virtual Rank.Rank Rank { get; set; }
        public Division.Division Division { get; set; }
        public virtual SLAFLocation.SLAFLocation SLAFLocation { get; set; }
        public virtual UserStatus.UserStatus UserStatus { get; set; }
        //public override bool ExpireOnDelete
        //{
        //    get
        //    {
        //        return false;
        //    }
        //}
       
    }
}
