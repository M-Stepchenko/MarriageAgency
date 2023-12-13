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
    [Authorize]
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
            bool withEmptyItem = true;
            List<SelectListItem> employees = employeesService.GetEmployeeAsSelectListItems(withEmptyItem);
            List<SelectListItem> allServices = allServicesService.GetAllServicesAsSelectListItems(withEmptyItem);
            
            HttpContext.Request.Cookies.TryGetValue("EmployeeId", out string? employee);
            HttpContext.Request.Cookies.TryGetValue("ServiceId", out string? service);
            HttpContext.Request.Cookies.TryGetValue("Client", out string? client);

            HttpContext.Response.Cookies.Append("EmployeeId", employee);
            HttpContext.Response.Cookies.Append("ServiceId", service);
            HttpContext.Response.Cookies.Append("Client", client);

            int employeeId = Convert.ToInt32(employee);
            int serviceId = Convert.ToInt32(service);

            int count = providedServicesService.GetRowCount(employeeId, serviceId, client);
            ViewData["List"] = providedServicesService.GetProvidedServices(pageNumber, pageSize, employeeId, serviceId, client);
            ProvidedServicesViewModel viewModel = new ProvidedServicesViewModel { EmployeeId = employeeId, ServiceId = serviceId, UserName = client == "all" ? "" : client, Count = count };
            viewModel.Pagination = new PaginationViewModel(count, pageNumber, pageSize);

            ViewBag.Employees = employees;
            ViewBag.AllServices = allServices;
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Index(ProvidedServicesViewModel model)
        {
            HttpContext.Response.Cookies.Append("EmployeeId", model.EmployeeId.ToString());
            HttpContext.Response.Cookies.Append("ServiceId", model.ServiceId.ToString());
            string client;
            if (User.IsInRole("admin"))
            {
                client = model.UserName == null ? "all" : model.UserName;
            }
            else
            {
                client = User.FindFirstValue(ClaimTypes.Name);
            }
            HttpContext.Response.Cookies.Append("Client", client);

            bool withEmptyItem = true;
           
            List<SelectListItem> employees = employeesService.GetEmployeeAsSelectListItems(withEmptyItem);
            List<SelectListItem> allServices = allServicesService.GetAllServicesAsSelectListItems(withEmptyItem);

            int count = providedServicesService.GetRowCount(model.EmployeeId, model.ServiceId, client);
            ViewData["List"] = providedServicesService.GetProvidedServices(1, pageSize, model.EmployeeId, model.ServiceId, client);
            ProvidedServicesViewModel viewModel = new ProvidedServicesViewModel { EmployeeId = model.EmployeeId, ServiceId = model.ServiceId, UserName = model.UserName, Count = count };
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