using MarriageAgency.Models;
using MarriageAgency.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MarriageAgencyContext>();
builder.Services.AddTransient<EmployeesService>();
builder.Services.AddTransient<AllServicesService>();
builder.Services.AddTransient<ProvidedServicesService>();
builder.Services.AddTransient<ClientsService>();
builder.Services.AddTransient<DBInitializer>();
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<MarriageAgencyContext>();
// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    DBInitializer dbInitializer = context.RequestServices.GetService<DBInitializer>();
    await dbInitializer.Initialize(context.RequestServices);
    await next.Invoke();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
