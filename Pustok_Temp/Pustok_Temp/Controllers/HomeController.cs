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
            HomeVM homeVM = new HomeVM();
            homeVM.categories = _context.Categories
                .ToList();
            homeVM.blogs = _context.Blogs
                .Include(b => b.BlogImages)
                .Include(b => b.BlogTags)
                .ThenInclude(bt => bt.Tag)
                .ToList();
            homeVM.books = _context.Books
                .Include(x => x.BookImages)
                .Include(x => x.BookTags)
                .ThenInclude(x => x.Tag)
                .ToList();

            return View(homeVM);
        }
    }
}