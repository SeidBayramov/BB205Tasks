using IndigoTemplateTask.DAL;
using IndigoTemplateTask.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IndigoTemplateTask.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public  async Task<IActionResult> Index()
        {
            HomeVm homeVm = new HomeVm()
            {
                Product = await _context.Products.Where(p => p.IsDeleted == false).Include(p => p.ProductImages).ToListAsync()
            };



            return View(homeVm);
        }
    }
}
