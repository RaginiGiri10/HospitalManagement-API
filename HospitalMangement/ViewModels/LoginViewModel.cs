using System.ComponentModel.DataAnnotations;

namespace HospitalMangement.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Username is mandatory")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is mandatory")]
        public string Password { get; set; }

    }
}
