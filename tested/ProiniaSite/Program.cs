using Microsoft.EntityFrameworkCore;
using ProiniaSite.DAL;
using System;

namespace ProiniaSite
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
              name: "areas",
              pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );

            app.MapControllerRoute(
             name: "default",
              pattern: "{controller=home}/{action=index}/{id?}");


            app.Run();

        }

    }
}