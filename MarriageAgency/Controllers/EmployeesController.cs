using MarriageAgency.Models;
using MarriageAgency.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarriageAgency.Controllers
{
    public class EmployeesController : Controller
    {
        MarriageAgencyContext db;
        EmployeesService employeesService;
        public EmployeesController(MarriageAgencyContext _context, EmployeesService _employeesService)
        {
            db = _context;
            employeesService = _employeesService;
        }

        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 290)]
        public ActionResult Index()
        {
            ViewData["List"] = employeesService.GetEmployees();
            return View();
        }
    }
}
