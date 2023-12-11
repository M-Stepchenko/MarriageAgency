using MarriageAgency.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MarriageAgency.Services
{
    public class AllServicesService
    {
        MarriageAgencyContext db;
        public AllServicesService(MarriageAgencyContext context)
        {
            db = context;
        }

        public List<AllService> GetAllServices()
        {
            return db.AllServices.ToList();
        }

        public List<SelectListItem> GetAllServicesAsSelectListItems(bool withEmptyItem = false)
        {
            List<SelectListItem> allServices = new List<SelectListItem>();

            if (withEmptyItem)
            {
                allServices.Add(new SelectListItem { Value = "", Text = "Все услуги" });
            }

            GetAllServices().ForEach(s =>
            {
                allServices.Add(new SelectListItem { Value = s.Id.ToString(), Text = s.Name + " " + s.Cost.ToString() + " р." });
            });

            return allServices;
        }
    }
}
