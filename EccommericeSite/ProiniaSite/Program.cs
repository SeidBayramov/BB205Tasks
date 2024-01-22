using Microsoft.AspNetCore.Identity;
using System;

namespace ProiniaSite
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromSeconds(10);
            });
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddControllersWithViews();
            builder.Services.AddSession(opt =>
            {
                opt.IdleTimeout = TimeSpan.FromSeconds(10);
            });
            builder.Services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            }).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();

            builder.Services.AddScoped<LayoutService>();
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

   
            var app = builder.Build();


            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication(); 
            app.UseAuthorization();
            app.UseSession();


            app.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
            );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );


            app.Run();
        }
    }
}
