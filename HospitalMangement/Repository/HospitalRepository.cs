using HospitalMangement.DBContext;
using HospitalMangement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HospitalMangement.Repository
{
    public class HospitalRepository : IHospitalRepository
    {
        HospitalDbContext _hospitalDbContext;
        public HospitalRepository(HospitalDbContext hospitalDbContext)
        {
            _hospitalDbContext = hospitalDbContext;
        }
        public Hospital AddPatientRecords(Hospital hospital)
        {
           
            _hospitalDbContext.Hospitals.Add(hospital);
            _hospitalDbContext.SaveChanges();
            return hospital;
        }

       

        public List<Hospital> GetAllPatientRecords(string username)
        {
            return _hospitalDbContext.Hospitals.Include(d => d.Department).Where(x=>x.CreatedBy==username).ToList();
        }

        
        public Hospital GetFullPatientDetailsById(int id,string username)
        {
            return _hospitalDbContext.Hospitals.FirstOrDefault(p => p.Id == id);
        }


        public List<Hospital> GetFullPatientDetailsByName(string name, string username)
        {
            return _hospitalDbContext.Hospitals.Include(d => d.Department).Where(x => x.PatientName.Contains(name) && x.CreatedBy == username).ToList();
        }

        public bool UpdatePatientDetails(int id, Hospital hospital)
        {
            bool isPatientRecordUpdated = false;
            var patientRecordTobeUpdated = _hospitalDbContext.Hospitals.FirstOrDefault(p => p.Id == id);

            if (patientRecordTobeUpdated != null)
            {
                patientRecordTobeUpdated.PatientName = hospital.PatientName;
                patientRecordTobeUpdated.PatientAge = hospital.PatientAge;
                patientRecordTobeUpdated.PatientGender = hospital.PatientGender;
                patientRecordTobeUpdated.DepartmentId = hospital.DepartmentId;
                patientRecordTobeUpdated.DoctorName = hospital.DoctorName;
                patientRecordTobeUpdated.DoctorFee = hospital.DoctorFee;
                _hospitalDbContext.SaveChanges();

                isPatientRecordUpdated = true;

            }

            return isPatientRecordUpdated;

        }
        public bool DeletePatientDetails(int id)
        {
            bool isPatientRecordRemoved = false;
            var patientRecordTobeRemoved = _hospitalDbContext.Hospitals.FirstOrDefault(p => p.Id == id);
            if (patientRecordTobeRemoved != null)
            {
                _hospitalDbContext.Hospitals.Remove(patientRecordTobeRemoved);
                _hospitalDbContext.SaveChanges();
                isPatientRecordRemoved = true;
            }
            return isPatientRecordRemoved;

        }
    }
}
