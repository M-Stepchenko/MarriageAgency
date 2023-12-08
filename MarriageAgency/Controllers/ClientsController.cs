using MarriageAgency.Models;
using MarriageAgency.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MarriageAgency.Controllers
{
    [Authorize(Roles = "admin")]
    public class ClientsController : Controller
    {
        MarriageAgencyContext db;
        ClientsService service;
        public ClientsController(MarriageAgencyContext _context, ClientsService _service)
        {
            db = _context;
            service = _service;
        }

        public ActionResult Index()
        {
            ViewData["List"] = service.GetAllClients();
            return View();
        }

        public ActionResult Info(int clientId)
        {
            var client = service.GetClientById(clientId);
            ViewData["ViewTitle"] = "Данные клиента";
            ViewData["Id"] = clientId;

            ViewData["Name"] = client.Name;
            ViewData["Bithdate"] = DateOnly.FromDateTime((DateTime)client.Bithdate);
            ViewData["Nationality"] = client.Nationality.Name;
            ViewData["Sex"] = client.Sex;
            ViewData["Height"] = client.Height;
            ViewData["Weight"] = client.Weight;
            ViewData["FamilyStatus"] = client.FamilyStatus.Name;
            ViewData["Children"] = client.Children;
            ViewData["Job"] = client.Job;
            ViewData["BadHabbits"] = client.BadHabbits;
            ViewData["Hobby"] = client.Hobby;
            ViewData["Passport"] = client.Passport;
            ViewData["Address"] = client.Address;
            ViewData["DesiredPartner"] = client.DesiredPartner;
            ViewData["ZodiacSign"] = client.ZodiacSign.Name;
            ViewData["PhoneNumber"] = client.PhoneNumber;

            return View("Info");
        }

        public async Task<IActionResult> Delete(int clientId)
        {
            service.DeleteClientById(clientId);
            return RedirectToAction("Index", "Clients");
        }
    }
}
