using ExamAPP1.DAL;
using ExamAPP1.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamAPP1.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {

            List<Blog> blogs = await _context.Blogs.ToListAsync();

            return View(blogs);
        }
    }
}
