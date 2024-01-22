using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProiniaSite.DAL;
using ProiniaSite.Models;

namespace ProiniaSite.Controllers
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
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductTags)
                .ThenInclude(pt => pt.Tag)
                .FirstOrDefault(product => product.Id == Id);

            return View(product);
        }
    }
}
