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

        public List<ProvidedService> GetProvidedServices(string userId, int pageNumber, int pageSize, int employeeId = 0, int serviceId = 0, string userName = "")
        {
            var providedServices = (from ps in db.ProvidedServices
                                    join e in db.Employees on ps.EmployeeId equals e.Id
                                    join a in db.AllServices on ps.ServiceId equals a.Id
                                    join c in db.Clients on ps.ClientId equals c.Id
                                    join u in db.Users on c.UserId equals u.Id
                                    where (userId == "" || userId ==  c.UserId) && (employeeId == 0 || e.Id == employeeId) && (serviceId == 0 || a.Id == serviceId) && (userName == "all" || u.UserName == userName)
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

        public int GetRowCount(string userId, int pageNumber, int pageSize, int employeeId = 0, int serviceId = 0, string userName = "")
        {
            var count = (from ps in db.ProvidedServices
                         join e in db.Employees on ps.EmployeeId equals e.Id
                         join a in db.AllServices on ps.ServiceId equals a.Id
                         join c in db.Clients on ps.ClientId equals c.Id
                         join u in db.Users on c.UserId equals u.Id
                         where (userId == "" || userId == c.UserId) && (employeeId == 0 || e.Id == employeeId) && (serviceId == 0 || a.Id == serviceId) && (userName == "all" || u.UserName == userName)
                         orderby ps.Date descending, a.Name
                         select ps.Id ).Count();
            return count;
        }
    }
}
