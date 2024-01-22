using Microsoft.EntityFrameworkCore;
using System;
using WebSite.DAL.StoriesApp.DAL;

namespace WebSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            }
            );
            var app = builder.Build();


            app.UseStaticFiles();

            app.MapControllerRoute(
                 name: "default",
                  pattern: "{controller=home}/{action=index}/{id?}");


            app.Run();

        }

    }
}