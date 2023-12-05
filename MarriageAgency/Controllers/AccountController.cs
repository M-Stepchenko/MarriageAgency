using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MarriageAgency.Models;
using Microsoft.EntityFrameworkCore;
using MarriageAgency.Services;
using System.Security.Claims;

namespace MarriageAgency.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ClientsService _clientsService;

        MarriageAgencyContext db;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, MarriageAgencyContext context, ClientsService clientsService)
        {
            db = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _clientsService = clientsService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Nationalities = _clientsService.GetNationalities();
            ViewBag.ZodiacSigns = _clientsService.GetZodiazSigns();
            ViewBag.FamilyStatuses = _clientsService.GetFamilyStatuses();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User { UserName = model.Login };

                // добавляем пользователя
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    db.Clients.Add(new Client { Name = model.Name, Job = model.Job, Hobby = model.Hobby, UserId = user.Id,
                        Address = model.Address, BadHabbits = model.BadHabbits, Bithdate = model.Bithdate.ToDateTime(TimeOnly.MinValue),
                        Height = model.Height, Children = model.Children, DesiredPartner = model.DesiredPartner,  Weight = model.Weight,
                        Passport = model.Passport, PhoneNumber = model.PhoneNumber, Sex = model.Sex, FamilyStatusId = model.FamilyStatus,
                        NationalityId = model.Nationality, ZodiacSignId = model.ZodiacSign });
                    db.SaveChanges();
                    // установка куки и роли
                    await _userManager.AddToRoleAsync(user, "client");
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Login, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Info()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = _clientsService.GetClientByUserId(userId);

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

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> InfoEdit()
        {
            ViewBag.Nationalities = _clientsService.GetNationalities();
            ViewBag.ZodiacSigns = _clientsService.GetZodiazSigns();
            ViewBag.FamilyStatuses = _clientsService.GetFamilyStatuses();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var client = _clientsService.GetClientByUserId(userId);
            var model = new RegisterViewModel {Name = client.Name, Job = client.Job, Hobby = client.Hobby,
                        Address = client.Address, BadHabbits = client.BadHabbits, Bithdate = DateOnly.FromDateTime((DateTime)client.Bithdate),
                        Height = (int)client.Height, Children = (int)client.Children, DesiredPartner = client.DesiredPartner,  Weight = (int)client.Weight,
                        Passport = client.Passport, PhoneNumber = client.PhoneNumber, Sex = client.Sex, FamilyStatus = (int)client.FamilyStatus.Id,
                        Nationality = (int)client.Nationality.Id, ZodiacSign = (int)client.ZodiacSign.Id };

            return View("Register", model);
        }
    }
}
