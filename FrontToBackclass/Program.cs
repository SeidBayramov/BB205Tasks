using FrontToBack.DAL;
using Microsoft.EntityFrameworkCore;

namespace FrontToBack
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options=> options.UseSqlServer("server=DESKTOP-8LGITHD;database=bb205_site;Trusted_connection=true;Integrated security=true;Encrypt=false"));

            var app = builder.Build();


            app.MapControllerRoute(
                 name: "default",
                  pattern: "{controller=home}/{action=index}/{id?}");

            app.UseStaticFiles();



            app.Run();
        }
    }
}