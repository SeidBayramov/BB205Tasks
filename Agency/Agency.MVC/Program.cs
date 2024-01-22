using Agency.Busines.Services.Implementations;
using Agency.Busines.Services.Interface;
using Agency.Core.Entities;
using Agency.DAL.Context;
using Agency.DAL.Repository.Implementations;
using Agency.DAL.Repository.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Agency.MVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
            builder.Services.AddScoped<IAccountService, AccountService>();
            builder.Services.AddScoped<IPortfolioRepository, PortfolioRepository>();
            builder.Services.AddScoped<IPortfolioService, PortfolioService>();


            builder.Services.AddDbContext<AppDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });
            builder.Services.AddIdentity<AppUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 8;
                opt.Password.RequireNonAlphanumeric = true;
                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789._";
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
            builder.Services.AddAutoMapper(typeof(MapperProfilo).Assembly);
            var app = builder.Build();
            app.UseAuthentication();

            app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
);

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.UseStaticFiles();

            app.UseAuthorization();


            app.Run();
        }
    }
}