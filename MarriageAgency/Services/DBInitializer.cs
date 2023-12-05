using Azure;
using MarriageAgency.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System.Threading.Tasks;

namespace MarriageAgency.Services
{
    public class DBInitializer
    {
        MarriageAgencyContext db;
        public DBInitializer(MarriageAgencyContext context)
        {
            db = context;
        }

        public async Task Initialize(IServiceProvider serviceProvider)
        {
            InitializeClients();
            await InitializeUserIdentities(serviceProvider);
        }

        public void InitializeClients()
        {
            db.Database.EnsureCreated();

            if (db.Employees.Any())
            {
                return;
            }

            db.Employees.Add(new Employee { Name = "Иван Иванов", Bithdate = new DateTime(1968, 10, 14), Position = "Директор"});
            db.Employees.Add(new Employee { Name = "Николай Ковалев", Bithdate = new DateTime(1983, 05, 01), Position = "Управляющий" });
            db.Employees.Add(new Employee { Name = "Кристина Ковалева", Bithdate = new DateTime(1984, 07, 20), Position = "Управляющий" });
            db.Employees.Add(new Employee { Name = "Петр Дроздов", Bithdate = new DateTime(1975, 03, 04), Position = "Начальник отдела" });
            db.Employees.Add(new Employee { Name = "Алёна Петрова", Bithdate = new DateTime(1988, 12, 31), Position = "Начальник отдела" });
            db.Employees.Add(new Employee { Name = "Денис Родионов", Bithdate = new DateTime(1996, 04, 25), Position = "Начальник отдела" });
            db.Employees.Add(new Employee { Name = "София Иванова", Bithdate = new DateTime(1980, 09, 11), Position = "Начальник отдела" });
            db.Employees.Add(new Employee { Name = "Елизовета Прохорова", Bithdate = new DateTime(1992, 02, 141), Position = "Сотрудник" });
            db.Employees.Add(new Employee { Name = "Любовь Румянцева", Bithdate = new DateTime(1993, 03, 21), Position = "Сотрудник" });
            db.Employees.Add(new Employee { Name = "Виктория Михайлова", Bithdate = new DateTime(1993, 03, 21), Position = "Сотрудник" });
            db.Employees.Add(new Employee { Name = "Анна Гаврилова", Bithdate = new DateTime(1999, 05, 07), Position = "Сотрудник" });
            db.Employees.Add(new Employee { Name = "Егор Константинов", Bithdate = new DateTime(1984, 07, 10), Position = "Сотрудник" });
            db.Employees.Add(new Employee { Name = "Константин Соболев", Bithdate = new DateTime(1965, 11, 11), Position = "Сотрудник" });
            db.Employees.Add(new Employee { Name = "Евгений Ушаков", Bithdate = new DateTime(1989, 04, 03), Position = "Сотрудник" });
            db.Employees.Add(new Employee { Name = "Василиса Олейникова", Bithdate = new DateTime(2000, 05, 07), Position = "Сотрудник" });
            db.Employees.Add(new Employee { Name = "Евгения Елисеева", Bithdate = new DateTime(1996, 04, 08), Position = "Сотрудник" });
            db.Employees.Add(new Employee { Name = "Никита Левин", Bithdate = new DateTime(1994, 12, 15), Position = "Сотрудник" });
            db.Employees.Add(new Employee { Name = "Виктория Мухина", Bithdate = new DateTime(1989, 09, 26), Position = "Сотрудник" });
            db.Employees.Add(new Employee { Name = "Ярослав Гусев", Bithdate = new DateTime(1972, 07, 23), Position = "Сотрудник" });
            db.Employees.Add(new Employee { Name = "Дарья Шаповалова", Bithdate = new DateTime(1999, 09, 01), Position = "Сотрудник" });

            db.SaveChanges();
        }

        public static async Task InitializeUserIdentities(IServiceProvider serviceProvider)
        {
            UserManager<User> userManager = serviceProvider.GetRequiredService<UserManager<User>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            var n = userManager.Users.Count();

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("client") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("client"));
            }

            if (await userManager.FindByNameAsync("admin") == null)
            {
                User admin = new User
                {
                    UserName = "admin",
                };

                var result = await userManager.CreateAsync(admin, "Admin1_");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }

            if (userManager.Users.ToList().Count > 1)
            {
                return;
            }

            int usersNumber = 500;
            string login, password;

            int userId;

            for (userId = 1; userId <= usersNumber; userId++)
            {
                login = "client" + userId.ToString();
                password = "Client" + userId.ToString() + "_";
                User newUser = new User { UserName = login};
                IdentityResult result = await userManager.CreateAsync(newUser, password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, "client");
                }
                // db.Users.Add(new User { Login = login, Password = password, Email = email, Name = name, Surname = surname });
            }

            var a = userManager.Users.ToList();
        }
    }
}

