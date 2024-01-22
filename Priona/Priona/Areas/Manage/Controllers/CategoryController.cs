using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Priona.Core.Entity;
using Priona.DAL.Context;

namespace Priona.Areas.Manage.Controllers
{

    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {

        AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.Include(p => p.Products).ToList();

            return View(categories);
        }

        public IActionResult Create()
        {

            return View();

        }


        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _context.Categories.Add(category);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }

        public IActionResult Update(int id)
        {

            Category category = _context.Categories.Find(id);
            return View(category);
        }

        [HttpPost]
        public IActionResult Update(Category newcategory)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Category oldcategory = _context.Categories.Find(newcategory.Id);
            if (oldcategory == null)
            {
                return View();
            }
            oldcategory.Name = newcategory.Name;
            _context.SaveChanges();


            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {

            Category category = _context.Categories.Find(id);

            if (category != null)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();

            }

            return RedirectToAction("Index");
        }
    }
}
