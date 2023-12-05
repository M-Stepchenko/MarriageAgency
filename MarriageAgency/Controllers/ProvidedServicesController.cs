using MarriageAgency.Models;
using MarriageAgency.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MarriageAgency.Controllers
{
    public class ProvidedServicesController : Controller
    {
        MarriageAgencyContext db;
        ProvidedServicesService service;
        public ProvidedServicesController(MarriageAgencyContext _context, ProvidedServicesService _service)
        {
            db = _context;
            service = _service;
        }

        // GET: ProvidedServices
        [Authorize(Roles="client")]
        //[ResponseCache(Location = ResponseCacheLocation.Any, Duration = 290)]
        public ActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["List"] = service.GetProvidedServices(userId);
            return View();
        }
    }
}