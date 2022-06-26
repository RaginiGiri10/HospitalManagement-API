using HospitalMangement.Models;
using System.Collections.Generic;

namespace HospitalMangement.Repository
{
    public interface IDepartmentRepository
    {
        List<Department> GetAllDepartments();
    }
}
