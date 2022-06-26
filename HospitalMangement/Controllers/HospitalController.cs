using HospitalMangement.Models;
using HospitalMangement.Repository;
using HospitalMangement.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalMangement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HospitalController : ControllerBase
    {
        IHospitalRepository _hospitalRepository;
        IDepartmentRepository _departmentRepository;
        public HospitalController(IHospitalRepository hospitalRepository, IDepartmentRepository departmentRepository)
        {
            _hospitalRepository = hospitalRepository;
            _departmentRepository = departmentRepository;
        }
      
        [HttpPost]
        public IActionResult AddPatientRecords([FromBody]AddPatientRecordViewModel addPatientRecordViewModel)
        {
            var existingPatientRecords = _hospitalRepository.GetAllPatientRecords(User.Identity.Name);
            var record = existingPatientRecords?.FirstOrDefault(i => i.PatientName.ToLower() == addPatientRecordViewModel.PatientName.ToLower() && i.DoctorName.ToLower() == addPatientRecordViewModel.DoctorName.ToLower());
            if (record != null)
            {
                return Conflict("Patient record already exists");
            }
                var hospital = new Hospital
                {
                    PatientName= addPatientRecordViewModel.PatientName,
                    PatientAge = addPatientRecordViewModel.PatientAge,
                    PatientGender = addPatientRecordViewModel.PatientGender,
                    DepartmentId = addPatientRecordViewModel.DepartmentId,
                    DoctorName = addPatientRecordViewModel.DoctorName,
                    DoctorFee = addPatientRecordViewModel.DoctorFee,
                    CreatedBy = User.Identity.Name,
                    RegisterDate = DateTime.Now
                };

                var addedPatientRecords = _hospitalRepository.AddPatientRecords(hospital);
           

            return Ok(new { Message = "Patient Added" });
        }
        [HttpGet]
        [Route("/api/hospital/addpatient")]
        public IActionResult AddPatientRecords()
        {
            AddPatientRecordViewModel addPatientRecordViewModel = new AddPatientRecordViewModel
            {
                DepartmentList = _departmentRepository.GetAllDepartments()
            };
            return Ok(addPatientRecordViewModel);
        }

        [HttpGet]
        public IActionResult GetPatientRecords()
        {
            var hospitals = _hospitalRepository.GetAllPatientRecords(User.Identity.Name);

            List<HospitalDetailsViewModel> hospitalDetailsListViewModel = new List<HospitalDetailsViewModel>();

            foreach (var hospital in hospitals)
            {
                HospitalDetailsViewModel hospitalDetailsViewModel = new HospitalDetailsViewModel
                {
                    Id = hospital.Id,
                    PatientName = hospital.PatientName,
                    PatientAge = hospital.PatientAge,
                    PatientGender = hospital.PatientGender,
                    Department = hospital.Department.DepartmentName,
                    DoctorName = hospital.DoctorName,
                    DoctorFee = hospital.DoctorFee,
                    CreatedBy=User.Identity.Name,
                     RegisterDate = DateTime.Now

                };
                hospitalDetailsListViewModel.Add(hospitalDetailsViewModel);
            }
            if (hospitalDetailsListViewModel == null)
            {
                return NotFound("No Records Found");
            }
            return Ok(hospitalDetailsListViewModel);

        }
        
        [HttpGet("{id}")]

        public IActionResult GetPatientById(int id)
        {
            var patient = _hospitalRepository.GetFullPatientDetailsById(id,User.Identity.Name);
          
            if (patient == null)
            {
                return NotFound($"Patient with id ={id} is not found");
            }

            var getPatientByIdViewModel = new GetPatientByIdViewModel
            {
                Id=patient.Id,
                PatientName= patient.PatientName,
                PatientAge = patient.PatientAge,
                PatientGender = patient.PatientGender,
                DepartmentId=patient.DepartmentId,
                DepartmentList= _departmentRepository.GetAllDepartments(),
                DoctorName = patient.DoctorName,
                DoctorFee = patient.DoctorFee
            };
            return Ok(getPatientByIdViewModel);
        }

        [HttpGet("patient/{name}")]
        public IActionResult GetPatientByName(string name)
        {
            var allPatientByName = _hospitalRepository.GetFullPatientDetailsByName(name, User.Identity.Name);

            List<HospitalDetailsViewModel> hospitalDetailsListViewModel = new List<HospitalDetailsViewModel>();

            foreach (var patient in allPatientByName)
            {
                HospitalDetailsViewModel hospitalDetailsViewModel = new HospitalDetailsViewModel
                {
                    Id = patient.Id,
                    PatientName = patient.PatientName,
                    PatientAge = patient.PatientAge,
                    PatientGender = patient.PatientGender,
                    Department = patient.Department.DepartmentName,
                    DoctorName = patient.DoctorName,
                    DoctorFee = patient.DoctorFee,
                    CreatedBy = User.Identity.Name,
                    RegisterDate = DateTime.Now
                };
                hospitalDetailsListViewModel.Add(hospitalDetailsViewModel);
            }
            if (hospitalDetailsListViewModel == null)
            {
                return NotFound("No Records Found");
            }

            return Ok(hospitalDetailsListViewModel);
        }
        [HttpPut("{id}")]
        public IActionResult UpdatePatientRecord(int id, [FromBody] UpdatePatientRecordViewModel updatePatientRecordViewModel)
        {
            Hospital hospital = new Hospital
            {
                PatientName = updatePatientRecordViewModel.PatientName,
                PatientAge = updatePatientRecordViewModel.PatientAge,
                PatientGender = updatePatientRecordViewModel.PatientGender,
                DepartmentId = updatePatientRecordViewModel.DepartmentId,
                DoctorName = updatePatientRecordViewModel.DoctorName,
                DoctorFee = updatePatientRecordViewModel.DoctorFee,
                CreatedBy = User.Identity.Name,
                RegisterDate = DateTime.Now
            };

            bool isPatientRecordUpdated = _hospitalRepository.UpdatePatientDetails(id, hospital);

            if (!isPatientRecordUpdated)
            {
                return NotFound($"Patient with id = {id} is not found.");
            }
            return Ok(new { m=$"Patient with id {id}updated successfully" });

        }
        [HttpDelete("{id}")]
        public IActionResult DeletePatientRecord(int id)
        {
            bool isRecordRemoved = _hospitalRepository.DeletePatientDetails(id);
            if (!isRecordRemoved)
            {
                return NotFound($"Patient with id = {id} is not found.");
            }
            return Ok(new { v = $"Patient with id {id} deleted successfully" });
        }

    }
}
