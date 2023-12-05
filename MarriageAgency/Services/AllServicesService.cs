using MarriageAgency.Models;

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
    }
}
