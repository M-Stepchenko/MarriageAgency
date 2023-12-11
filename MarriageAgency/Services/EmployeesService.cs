using MarriageAgency.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public List<SelectListItem> GetEmployeeAsSelectListItems(bool withEmptyItem = false)
        {
            List<SelectListItem> employees = new List<SelectListItem>();
            
            if (withEmptyItem) 
            {
                employees.Add(new SelectListItem { Value = "", Text = "Все сотрудники" });   
            }

            GetEmployees().ForEach(s =>
            {
                employees.Add(new SelectListItem { Value = s.Id.ToString(), Text = s.Name });
            });

            return employees;
        }
    }
}
