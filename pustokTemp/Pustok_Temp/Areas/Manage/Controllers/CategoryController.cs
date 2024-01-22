using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok_Temp.DAL;
using Pustok_Temp.Models;

namespace Pustok_Temp.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {
        AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            List<Categories> categories = await _context.categories.Include(p => p.ParentCategory).ToListAsync();

            return View(categories);
        }

        public IActionResult Create()
        {

            return View();

        }


        [HttpPost]
        public async Task<IActionResult> Create(Categories category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.categories.AddAsync(category);
            _context.SaveChangesAsync();

            return RedirectToAction("Index");

        }

        public IActionResult Update(int id)
        {

            Categories category = _context.categories.Find(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Categories newcategory)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Categories oldcategory = _context.categories.Find(newcategory.Id);
            if (oldcategory == null)
            {
                return View();
            }
            oldcategory.Name = newcategory.Name;
            oldcategory.ParentCategoryId = newcategory.ParentCategoryId;
            _context.SaveChanges();


            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {

            Categories category = _context.categories.Find(id);

            if (category != null)
            {
                _context.categories.Remove(category);
                _context.SaveChanges();

            }

            return RedirectToAction("Index");
        }
    }
}