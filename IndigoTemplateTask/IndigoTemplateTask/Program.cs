using IndigoTemplateTask.DAL;
using IndigoTemplateTask.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;

namespace IndigoTemplateTask
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("server = .; database=Indigo; Trusted_connection=true; Integrated security=true; Encrypt=false"));


            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            var app = builder.Build();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Manage}/{action=Index}/{id?}"
                );
            });



            app.MapControllerRoute(
                name: "Home",
                pattern: "{controller=home}/{action=index}/{id?}");

            app.Run();
        }
    }
}