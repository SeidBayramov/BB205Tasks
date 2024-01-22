using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Pustok_Temp.Controllers
{
	public class BasketController : Controller
	{
		AppDbContext _context;
		public BasketController(AppDbContext context)
		{
			_context = context;
		}
		public IActionResult Index()
		{
			var jsonCookie = Request.Cookies["Basket"];
			List<BasketProductVm> basketProduct = new List<BasketProductVm>();
			if (jsonCookie != null)
			{
				var cookieProduct = JsonConvert.DeserializeObject<List<CookieBasketProductVM>>(jsonCookie);
				bool checkCount = false;
				//List<CookieBasketProductVM> deleetedCookie = new List<CookieBasketProductVM>();
				foreach (var item in cookieProduct)
				{
					Book book = _context.Books.Where(b => b.IsDeleted == false).Include(b => b.BookImages.Where(b => b.IsPrime == true)).FirstOrDefault(b => b.Id == item.Id);
					if (book != null)
					{
						//deleetedCookie.Add(item);
						basketProduct.Add(new BasketProductVm()
						{
							Id = item.Id,
							Name = book.Title,
							Price = book.Price,
							Count = item.Count,
							ImgUrl = book.BookImages.FirstOrDefault().ImgUrl
						});
						continue;
					}

				}
				/*if (deleetedCookie.Count > 0)
                {
                    foreach (var delete in deleetedCookie)
                    {
                        cookieProduct.Remove(delete);
                    }
                };*/
				Response.Cookies.Append("Basket", JsonConvert.SerializeObject(cookieProduct));
			}
			return View(basketProduct);
		}

		public IActionResult AddItem(int Id)
		{
			if (Id <= 0)
			{
				return BadRequest();
			}
			Book book = _context.Books.FirstOrDefault(x => x.Id == Id);
			if (book == null)
			{
				return NotFound();
			}
			List<CookieBasketProductVM> basketItems;

			var json = Request.Cookies["Basket"];

			if (json != null)
			{
				basketItems = JsonConvert.DeserializeObject<List<CookieBasketProductVM>>(json);
				var existProduct = basketItems.FirstOrDefault(x => x.Id == Id);
				if (existProduct != null)
				{
					existProduct.Count += 1;
				}
				else
				{
					basketItems.Add(new CookieBasketProductVM()
					{
						Id = Id,
						Count = 1
					});
				}
			}
			else
			{
				basketItems = new List<CookieBasketProductVM>();
				basketItems.Add(new CookieBasketProductVM()
				{
					Id = Id,
					Count = 1
				});
			}

			var CookieBasket = JsonConvert.SerializeObject(basketItems);
			Response.Cookies.Append("Basket", CookieBasket);

			return RedirectToAction(nameof(Index), "Home");
		}





		public IActionResult RemoveItem(int Id)
		{
			var cookieBasket = Request.Cookies["Basket"];
			if (cookieBasket != null)
			{
				List<CookieBasketProductVM> basket = JsonConvert.DeserializeObject<List<CookieBasketProductVM>>(cookieBasket);

				var deleteElement = basket.FirstOrDefault(p => p.Id == Id);
				if (deleteElement != null)
				{
					basket.Remove(deleteElement);
				}


				Response.Cookies.Append("Basket", JsonConvert.SerializeObject(basket));
				return Ok();
			}
			return NotFound();

		}

		public IActionResult GetBasket(int Id)
		{
			var BasketCookieJson = Request.Cookies["Basket"];
			return Content(BasketCookieJson);
		}
	}
}
