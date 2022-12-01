using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MIMS.Models
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Current password")]
        [StringLength(32)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} characters long and not more than 32 characters."
            , MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }

    public class LogOnModel
    {
        [Required]
        [Display(Name = "Username")]
        [StringLength(32)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(32)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }

    public class RegisterModel
    {
        [Required]
        [Remote("IsUsernameAvailable", "Users")]
        [Display(Name = "Username")]
        [StringLength(32)]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Only letters and numbers are allowed.")]
        public string UserName { get; set; }

        [Required]
        [StringLength(128)]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} characters long and not more than 32 characters."
            , MinimumLength = 6)]
        [DataType(DataType.Password)]
        [MaxLength(32), MinLength(6)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [StringLength(32)]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(64)]
        [Display(Name = "Email")]
        [RegularExpression(@"^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$",
            ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [StringLength(64)]
        [Display(Name = "Telephone")]
        public string Telephone1 { get; set; }

        [Display(Name = "This supplier is a fabricator as well")]
        public bool IsFabUser { get; set; }

    }

    public class ResetPasswordModel
    {
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required]
        [StringLength(32, ErrorMessage = "The {0} must be at least {2} characters long and not more than 32 characters."
            , MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string NewPassword { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
