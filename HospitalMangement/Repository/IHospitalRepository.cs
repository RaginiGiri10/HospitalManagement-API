using HospitalMangement.Models;
using System.Collections.Generic;

namespace HospitalMangement.Repository
{
    public interface IHospitalRepository
    {
        Hospital AddPatientRecords(Hospital hospital);
        List<Hospital> GetAllPatientRecords(string username);
        Hospital GetFullPatientDetailsById(int id, string username);
        List<Hospital> GetFullPatientDetailsByName(string name, string username);
        bool UpdatePatientDetails(int id, Hospital hospital);
        bool DeletePatientDetails(int id);

    }
}
