using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebSite.DAL.StoriesApp.DAL;
using WebSite.ViewModels;
using WebSite.ViewModels.StoriesApp.ViewModels;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            HomeVM vm = new HomeVM()
            {
                _stories = _context.Stories.ToList(),
                _recipes = _context.Recipes.ToList(),
                _categories = _context.Categories.ToList()
            };
            return View(vm);
        }
        public IActionResult Stories()
        {
            HomeVM vm = new HomeVM()
            {
                _stories = _context.Stories.ToList()
            };
            return View(vm);
        }
        public IActionResult Recipes()
        {
            HomeVM vm = new HomeVM()
            {
                _recipes = _context.Recipes.ToList()
            };
            return View(vm);
        }
        public IActionResult SingleStory(int id)
        {
            HomeVM homeVM = new HomeVM();
            homeVM._categories = _context.Categories
                .Include(x => x.Stories)
                .ToList();
            return View(homeVM);
        }
        public IActionResult SingleRecipe(int id)
        {
            HomeVM homeVM = new HomeVM();
            homeVM._categories = _context.Categories
                .Include(x => x.Recipes)
                .ToList();
            return View(homeVM);
        }
    }
}
