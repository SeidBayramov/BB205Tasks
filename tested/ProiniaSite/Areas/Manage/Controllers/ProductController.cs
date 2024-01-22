using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiniaSite.Areas.Manage.ViewModels.Product;
using ProiniaSite.DAL;
using ProiniaSite.Models;

namespace ProiniaSite.Areas.Manage.Controllers
{

    [Area("Manage")]
    public class ProductController : Controller
    {

        AppDbContext _context { get; set; }

        public ProductController(AppDbContext context)
        {
            _context = context;
        }

        public async  Task<IActionResult> Index()
        {
            List<Product> product= await _context.Products.Include(product=>product.Category)
                .Include(product=>product.ProductTags)
                .ThenInclude(product => product.Tag).ToListAsync();

            return View(product);
        }

        public async Task<IActionResult> Create( )
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Tags = await _context.Tags.ToListAsync();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVm createProductVm)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Tags = await _context.Tags.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View();
            }
            bool resultCategory = await _context.Categories.AnyAsync(c => c.Id == createProductVm.CategoryId);

            if (!resultCategory)
            {
                ModelState.AddModelError("CategoryId", "Bele bir category ID yoxdu");
            }


            Product product = new Product()
            {
                Name=createProductVm.Name,
                Price=createProductVm.Price,
                Description=createProductVm.Description,
                SKU=createProductVm.SKU,
                CategoryId=createProductVm.CategoryId,
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Update(int id)
        {
            Product product = await _context.Products.Where(product => product.Id == id).FirstOrDefaultAsync();
           if(product == null)
            {
               return View("Error");
            }


            return View(product);
        }


        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return View("Error");
            }
            return RedirectToAction(nameof(Index));
        }


    }
}
