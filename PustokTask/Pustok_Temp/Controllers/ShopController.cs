using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Pustok_Temp.Controllers
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

			Book book = _db.Books.Where(p => p.IsDeleted == false)
                .Include(p => p.Category)
				.Include(p => p.BookImages)
				.Include(p => p.BookTags)
				.ThenInclude(pt => pt.Tag)
				.FirstOrDefault(product => product.Id == Id);

			if (book == null)
			{
				return NotFound();
			}


			DetailVM detailVM = new DetailVM()
			{
				Book = book,
				Books = _db.Books.Where(p => p.IsDeleted == false).Include(p => p.BookImages).Include(p => p.BookTags).Include(p => p.Category).Where(p => p.Id == Id).ToList(),

			};

			return View(detailVM);
		}
	}
}
