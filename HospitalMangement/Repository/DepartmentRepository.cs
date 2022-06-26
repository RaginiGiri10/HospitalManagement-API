using HospitalMangement.DBContext;
using HospitalMangement.Models;
using System.Collections.Generic;
using System.Linq;

namespace HospitalMangement.Repository
{
    public class DepartmentRepository : IDepartmentRepository
    {
        HospitalDbContext _hospitalDbContext;
        public DepartmentRepository(HospitalDbContext hospitalDbContext)
        {
            _hospitalDbContext = hospitalDbContext;
        }
        public List<Department> GetAllDepartments()
        {
            return _hospitalDbContext.Departments.ToList();
        }
    }
}
