using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiniaSite.DAL;
using ProiniaSite.Models;
using ProiniaSite.ViewModel;

namespace ProiniaSite.Controllers
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
            HomeVM homeVM = new HomeVM()
            {
                Sliders=await _db.Sliders.ToListAsync(),
                Products=  await _db.Products.Include(p => p.ProductImages).ToListAsync()
            };

            return View(homeVM);
        }
    }
}
