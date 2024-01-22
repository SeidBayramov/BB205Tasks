using ExamAPP1.DAL;
using ExamAPP1.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ExamAPP1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
          
            builder.Services.AddControllersWithViews();


            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Password.RequireLowercase = true;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._";
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

        }
    }
}