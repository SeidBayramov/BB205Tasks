using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Priona.Business.ViewModel;
using Priona.DAL.Context;

namespace Priona.Controllers
{
    public class HomeController : Controller
    {

        AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {

            //Response.Cookies.Append("Name", "Seid", new CookieOptions()
            //{
            //    MaxAge = TimeSpan.FromMinutes(1)
            //});

            //HttpContext.Session.SetString("Name", "Seid");


            HomeVM homeVm = new HomeVM()
            {
                Sliders = await _db.Sliders.ToListAsync(),
                Products = await _db.Products.Where(p => p.IsDeleted == false).Include(p => p.ProductImages).ToListAsync()
            };

            return View(homeVm);
        }
    }
}
