using DianaTemp.DAL;
using DianaTemp.Models;
using DianaTemp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DianaTemp.Controllers
{
    public class ShopController : Controller
    {

        AppDbContext _db;

        public ShopController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Detail(int? Id)
        {
            Product product = _db.Products
                           .Where(p => p.IsDeleted == false)
                           .Include(p => p.Category)
                           .Include(p => p.ProductImages)
                           .Include(p => p.ProductColours)
                           .ThenInclude(pt => pt.Colour)
                           .Include(p => p.ProductMaterial)
                           .ThenInclude(pt => pt.Material)
                           .Include(p => p.ProductSizes)
                           .ThenInclude(pt => pt.Size)
                           .FirstOrDefault(product => product.Id == Id);

            if (product == null)
            {
                return NotFound();
            }


            DetailVM detailVM = new DetailVM()
            {
                Product = product,
                Products = _db.Products.Include(p => p.ProductImages).Include(p=>p.ProductMaterial).Include(p => p.ProductColours).Include(p => p.ProductSizes).Include(p => p.Category).Where(p => p.Id == Id).ToList(),

            };

            return View(detailVM);
        }
    }
}