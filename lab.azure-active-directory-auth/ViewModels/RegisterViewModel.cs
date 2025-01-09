using System.ComponentModel.DataAnnotations;

namespace lab.azure_active_directory_auth.ViewModels
{
    public partial class RegisterViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string? UserEmail { get; set; }

        //[Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string? UserPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("UserPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string? UserConfirmPassword { get; set; }

        public string? RoleName { get; set; }

        public string? ReturnUrl { get; set; }
    }
}
