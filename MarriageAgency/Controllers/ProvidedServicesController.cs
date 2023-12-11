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

        [HttpGet]
        public ActionResult Index(int pageNumber = 1)
        {
            int employeeId = 0;
            int serviceId = 0;
            string userName = "all";
            bool withEmptyItem = true;
            List<SelectListItem> employees = employeesService.GetEmployeeAsSelectListItems(withEmptyItem);
            List<SelectListItem> allServices = allServicesService.GetAllServicesAsSelectListItems(withEmptyItem);
            if (User.IsInRole("admin"))
            {
                HttpContext.Request.Cookies.TryGetValue("EmployeeId", out string? employee);
                HttpContext.Request.Cookies.TryGetValue("ServiceId", out string? service);
                HttpContext.Request.Cookies.TryGetValue("Client", out string? client);

                HttpContext.Response.Cookies.Append("EmployeeId", employee);
                HttpContext.Response.Cookies.Append("ServiceId", service);
                HttpContext.Response.Cookies.Append("Client", client == null ? "all" : client);

                employeeId = employee == null ? 0 : Convert.ToInt32(employee);
                serviceId = service == null ? 0 : Convert.ToInt32(service);
                userName = client == null ? "all" : client;
            }

            
            var userId = "";
            if (User.IsInRole("client"))
            {
                userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            ViewData["List"] = providedServicesService.GetProvidedServices(userId, pageNumber, pageSize, employeeId, serviceId, userName);
            int count = providedServicesService.GetRowCount(userId, pageNumber, pageSize, employeeId, serviceId, userName);
            ProvidedServicesViewModel viewModel = new ProvidedServicesViewModel { EmployeeId = employeeId, ServiceId = serviceId, UserName = userName };
            viewModel.Pagination = new PaginationViewModel(count, pageNumber, pageSize);

            ViewBag.Employees = employees;
            ViewBag.AllServices = allServices;
            return View(viewModel);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult Index(ProvidedServicesViewModel model)
        {
            HttpContext.Response.Cookies.Append("EmployeeId", model.EmployeeId.ToString());
            HttpContext.Response.Cookies.Append("ServiceId", model.ServiceId.ToString());
            HttpContext.Response.Cookies.Append("Client", model.UserName == null ? "all" : model.UserName);

            bool withEmptyItem = true;
            var userId = "";
            List<SelectListItem> employees = employeesService.GetEmployeeAsSelectListItems(withEmptyItem);
            List<SelectListItem> allServices = allServicesService.GetAllServicesAsSelectListItems(withEmptyItem);

            ViewData["List"] = providedServicesService.GetProvidedServices(userId, 1, pageSize, model.EmployeeId, model.ServiceId, model.UserName);
            int count = providedServicesService.GetRowCount(userId, 1, pageSize, model.EmployeeId, model.ServiceId, model.UserName);
            ProvidedServicesViewModel viewModel = new ProvidedServicesViewModel { EmployeeId = model.EmployeeId, ServiceId = model.ServiceId, UserName = model.UserName };
            viewModel.Pagination = new PaginationViewModel(count, 1, pageSize);

            ViewBag.Employees = employees;
            ViewBag.AllServices = allServices;
            return View(viewModel);
        }

        [Authorize(Roles="client")]
        [HttpGet]
        public ActionResult NewOrder()
        {
            List<SelectListItem> employees = employeesService.GetEmployeeAsSelectListItems();
            List<SelectListItem> allServices = allServicesService.GetAllServicesAsSelectListItems();

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