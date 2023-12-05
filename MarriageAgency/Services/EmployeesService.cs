using MarriageAgency.Models;

namespace MarriageAgency.Services
{
    public class EmployeesService
    {
        MarriageAgencyContext db;
        public EmployeesService(MarriageAgencyContext context)
        {
            db = context;
        }

        public List<Employee> GetEmployees()
        {
            return db.Employees.ToList();
        }
    }
}
