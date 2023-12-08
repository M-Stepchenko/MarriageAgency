using MarriageAgency.Models;
using MarriageAgency.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MarriageAgency.Controllers
{
    public class AllServicesController : Controller
    {
        MarriageAgencyContext db;
        AllServicesService allServicesService;
        public AllServicesController(MarriageAgencyContext _context, AllServicesService _allServicesService)
        {
            db = _context;
            allServicesService = _allServicesService;
        }

        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 290)]
        public ActionResult Index()
        {
            ViewData["List"] = allServicesService.GetAllServices();
            return View();
        }
    }
}
