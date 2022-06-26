using System;

namespace HospitalMangement.ViewModels
{
    public class HospitalDetailsViewModel
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public int PatientAge { get; set; }
        public string PatientGender { get; set; }
       public string Department { get; set; }
        public string DoctorName { get; set; }
        public double DoctorFee { get; set; }
        public DateTime RegisterDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
