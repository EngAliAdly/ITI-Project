using System.ComponentModel.DataAnnotations;

namespace ClinicMaster.Core.ViewModel
{
    public class RegisterViewModel
    {
        //[Required(ErrorMessage = " Name Required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = " Email Required")]
        [EmailAddress(ErrorMessage = "invalid mail formate")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = " Password Required")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = " Confirm password Required")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        public bool? IsActive { get; set; }
    }
}
