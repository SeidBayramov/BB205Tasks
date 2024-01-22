using Carvilla.DAL.Context;
using Carvilla.MapperProfile.CarProfile;
using Carvilla.Models;
using Carvilla.ViewModels;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Carvilla
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllersWithViews();

            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddValidatorsFromAssemblyContaining<LoginVmValidator>();

            builder.Services.AddAutoMapper(typeof(DefaultCarProfile).Assembly);
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });


            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequireDigit = true;
                opt.Password.RequiredLength = 4;
                opt.Password.RequireNonAlphanumeric = true;
                opt.Password.RequireUppercase = true;
                opt.Lockout.MaxFailedAccessAttempts = 4;
                opt.Password.RequireLowercase = true;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._";
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


            var app = builder.Build();

            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllerRoute(
              name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}