using Microsoft.EntityFrameworkCore;
using Pustok_Temp.DAL;

namespace Pustok_Temp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("server = DESKTOP-8LGITHD; database=PustokDB; Trusted_connection=true; Integrated security=true; Encrypt=false"));
            var app = builder.Build();
            app.UseStaticFiles();
            app.UseRouting();

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