using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Priona.Business.ViewModel;
using Priona.Core.Entity;
using Priona.DAL.Context;

namespace Priona.Controllers
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

            //    string cookie = Request.Cookies["Name"];
            //    string session = HttpContext.Session.GetString("Name");
            //    return Content(session);

            //    if(cookie == null) {  return NotFound(); }


            Product product = _db.Products
                .Where(p => p.IsDeleted == false)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductTags)
                .ThenInclude(pt => pt.Tag)
                .FirstOrDefault(product => product.Id == Id);

            if (product == null)
            {
                return NotFound();
            }


            DetailVM detailVM = new DetailVM()
            {
                Product = product,
                Products = _db.Products.Include(p => p.ProductImages).Include(p => p.Category).Where(p => p.Id == Id).ToList(),

            };

            return View(detailVM);
        }
    }
}
