using System.Collections.Generic;

namespace HospitalMangement.HospitalResponse.Authentication
{
    public class RegisterResponse
    {
        public bool IsRegistrationSuccessfull { get; set; }

        public IEnumerable<string>? Errors { get; set; }

    }
}
