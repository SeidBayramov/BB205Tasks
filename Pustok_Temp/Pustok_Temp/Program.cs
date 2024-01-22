using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Pustok_Temp.DAL;

namespace Pustok_Temp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("server = .; database=PustokBitArtiq; Trusted_connection=true; Integrated security=true; Encrypt=false"));
            builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
            {
				options.Password.RequireDigit = true;
				options.Password.RequireLowercase = true;
				options.Password.RequireNonAlphanumeric = true;
				options.Password.RequireUppercase = true;
				options.Password.RequiredLength = 6;
				options.Password.RequiredUniqueChars = 1;
			}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();


            var app = builder.Build();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthentication();
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