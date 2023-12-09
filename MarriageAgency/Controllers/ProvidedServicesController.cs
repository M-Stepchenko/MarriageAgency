using MarriageAgency.Models;
using MarriageAgency.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace MarriageAgency.Controllers
{
    public class ProvidedServicesController : Controller
    {
        MarriageAgencyContext db;
        ProvidedServicesService providedServicesService;
        AllServicesService allServicesService;
        EmployeesService employeesService;
        ClientsService clientsService;
        
        private int pageSize = 20;

        public ProvidedServicesController(MarriageAgencyContext _context, ClientsService _clientsService, ProvidedServicesService _providedServicesService, AllServicesService _allService, EmployeesService _employeesService)
        {
            db = _context;
            providedServicesService = _providedServicesService;
            allServicesService = _allService;
            employeesService = _employeesService;
            clientsService = _clientsService;
        }

        public ActionResult Index(int pageNumber = 1)
        {
            var userId = "";

            if (User.IsInRole("client"))
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            ViewData["List"] = providedServicesService.GetProvidedServices(userId, pageNumber, pageSize);
            int count = providedServicesService.GetRowCount(userId);
            ProvidedServicesViewModel viewModel = new ProvidedServicesViewModel();
            viewModel.Pagination = new PaginationViewModel(count, pageNumber, pageSize);
            return View(viewModel);
        }

        [Authorize(Roles="client")]
        [HttpGet]
        public ActionResult NewOrder()
        {
            List<SelectListItem> employees = new List<SelectListItem>();
            List<SelectListItem> allServices = new List<SelectListItem>();

            allServicesService.GetAllServices().ForEach(s =>
                {
                    allServices.Add(new SelectListItem { Value = s.Id.ToString(), Text = s.Name + " " + s.Cost.ToString() + " р." });
                });

            employeesService.GetEmployees().ForEach(s =>
            {
                employees.Add(new SelectListItem { Value = s.Id.ToString(), Text = s.Name });
            });

            ViewBag.Employees = employees;
            ViewBag.AllServices = allServices;
            return View();
        }

        [Authorize(Roles = "client")]
        [HttpPost]
        public async Task<IActionResult> NewOrder(NewOrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                Client client = clientsService.GetClientByUserId(userId);

                db.ProvidedServices.Add(new ProvidedService
                {
                    ServiceId = model.Name,
                    ClientId = client.Id,
                    EmployeeId = model.Employee,
                    Date = model.Date.ToDateTime(TimeOnly.MinValue)
                });
             
                db.SaveChanges();
                return RedirectToAction("Index", "ProvidedServices");
            }

            return View(model);
        }
    }
}