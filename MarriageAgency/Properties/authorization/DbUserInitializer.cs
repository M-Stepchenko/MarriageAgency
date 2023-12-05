using TestingSystem.Models;
using Microsoft.AspNetCore.Identity;

namespace TestingSystem
{
    public class DbUserInitializer
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (await roleManager.FindByNameAsync("admin") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("admin"));
            }
            if (await roleManager.FindByNameAsync("user") == null)
            {
                await roleManager.CreateAsync(new IdentityRole("user"));
            }

            if (await userManager.FindByNameAsync("admin") == null)
            {
                ApplicationUser admin = new ApplicationUser
                {
                    Email = "admin@gmail.com",
                    UserName = "admin",
                    Name = "Admin",
                    Surname = "admin",
                };

                var result = await userManager.CreateAsync(admin, "Admin1_");
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(admin, "admin");
                }
            }

            if(userManager.Users.ToList().Count > 1)
            {
                return;
            }

            Random randomObj = new Random(1);

            int usersNumber = 500;
            string login, password, email, name, surname;

            string[] nameVocabulary = { "Иван", "Петр", "Фёдор", "Максим", "Даниил" };
            int userId;


            for (userId = 1; userId <= usersNumber; userId++)
            {
                login = "Login" + userId.ToString();
                password = "Password" + userId.ToString() + "_";
                email = "user" + userId.ToString() + "@gmail.com";
                name = nameVocabulary[randomObj.Next(nameVocabulary.Length)];
                surname = nameVocabulary[randomObj.Next(nameVocabulary.Length)] + "ов";
                ApplicationUser newUser = new ApplicationUser { UserName = login, Email = email, Name = name, Surname = surname };
                IdentityResult result = await userManager.CreateAsync(newUser, password);
               
                if(result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newUser, "user");
                }
                
                // db.Users.Add(new User { Login = login, Password = password, Email = email, Name = name, Surname = surname });
            }

            var a = userManager.Users.ToList();
        }
    }
}
