using MarriageAgency.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Security.Claims;

namespace MarriageAgency.Services
{
    public class ProvidedServicesService
    {
        MarriageAgencyContext db;
        public ProvidedServicesService(MarriageAgencyContext context)
        {
            db = context;
        }

        public List<ProvidedService> GetProvidedServices(string userId, int pageNumber, int pageSize)
        {
            var providedServices = (from ps in db.ProvidedServices
                                    join e in db.Employees on ps.EmployeeId equals e.Id
                                    join a in db.AllServices on ps.ServiceId equals a.Id
                                    join c in db.Clients on ps.ClientId equals c.Id
                                    where userId == "" || userId ==  c.UserId
                                    orderby ps.Date descending, a.Name
                                    select new ProvidedService()
                                    {
                                        Id = ps.Id,
                                        Client = c,
                                        Service = a,
                                        Employee = e,
                                        Date = ps.Date
                                    }).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return providedServices;
        }

        public int GetRowCount(string userId)
        {
            var count = (from ps in db.ProvidedServices
                                    join c in db.Clients on ps.ClientId equals c.Id
                                    where userId == "" || userId == c.UserId
                                    select ps.Id ).Count();
            return count;
        }
    }
}
