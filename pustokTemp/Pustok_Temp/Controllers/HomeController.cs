using Microsoft.AspNetCore.Mvc;
using Pustok_Temp.DAL;
using Pustok_Temp.ViewModels;

namespace Pustok_Temp.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                authors = _context.authors.ToList(),
                books = _context.books.ToList(),
                books_img = _context.bookimages.ToList(),
                categories = _context.categories.ToList(),
                sliders = _context.sliders.ToList(),
            };
            return View(homeVM);
        }
    }
}
