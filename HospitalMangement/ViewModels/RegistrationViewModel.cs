using System.ComponentModel.DataAnnotations;

namespace HospitalMangement.ViewModels
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Email is mandatory")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is mandatory")]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Password & Confirm Password must match.")]
        public string ConfirmPassword { get; set; }


    }
}
