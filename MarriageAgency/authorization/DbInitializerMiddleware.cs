﻿using TestingSystem;
using TestingSystem.Models;

namespace TestingSystem.Middleware
{
    public class DbInitializerMiddleware
    {
        private readonly RequestDelegate _next;
        public DbInitializerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public Task Invoke(HttpContext context, IServiceProvider serviceProvider, TestingSystemDbContext dbContext)
        {
            if (!(context.Session.Keys.Contains("starting")))
            {
                DbUserInitializer.Initialize(serviceProvider).Wait();
                DbInitilializer.Initialize(dbContext, serviceProvider);
                context.Session.SetString("starting", "Yes");
            }

            return _next.Invoke(context);
        }
    }

    public static class DbInitializerExtensensions
    {
        public static IApplicationBuilder UseDbInitializer(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DbInitializerMiddleware>();
        }
    }
}
