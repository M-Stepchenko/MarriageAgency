using MarriageAgency.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MarriageAgency.Services
{
    public class ClientsService
    {
        MarriageAgencyContext db;
        public ClientsService(MarriageAgencyContext context)
        {
            db = context;
        }

        public Client GetClientByUserId(string userId)
        {
            var client = (from c in db.Clients
                          join n in db.Nationalities on c.NationalityId equals n.Id
                          join z in db.ZodiacSigns on c.ZodiacSignId equals z.Id
                          join f in db.FamilyStatuses on c.FamilyStatusId equals f.Id
                          where c.UserId == userId
                          select new Client 
                          {
                              Id = c.Id,
                              Name = c.Name,
                              Bithdate = c.Bithdate,
                              Nationality = n,
                              Sex = c.Sex,
                              Height = c.Height,
                              Weight = c.Weight,
                              FamilyStatus = f,
                              PhoneNumber = c.PhoneNumber,
                              Children = c.Children,
                              Hobby = c.Hobby,
                              BadHabbits = c.BadHabbits,
                              Job = c.Job,
                              Passport = c.Passport,
                              Address = c.Address,
                              DesiredPartner = c.DesiredPartner,
                              ZodiacSign = z
                          }).SingleOrDefault();
            
            return client;
        }

        public List<SelectListItem> GetNationalities() 
        {
            var nationalities = db.Nationalities.Select(n => new SelectListItem { Value = n.Id.ToString(), Text = n.Name }).ToList();
            return nationalities;
        }

        public List<SelectListItem> GetZodiazSigns()
        {
            var zodiacSigns = db.ZodiacSigns.Select(z => new SelectListItem { Value = z.Id.ToString(), Text = z.Name }).ToList();
            return zodiacSigns;
        }

        public List<SelectListItem> GetFamilyStatuses()
        {
            var familyStatuses = db.FamilyStatuses.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }).ToList();
            return familyStatuses;
        }
    }
}
