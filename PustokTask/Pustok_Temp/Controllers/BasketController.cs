using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pustok_Temp.ViewModels.ProiniaSite.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace Pustok_Temp.Controllers
{
	
	public class BasketController : Controller
	{
		private readonly AppDbContext _context;
		private readonly UserManager<AppUser> _userManager;



		public BasketController(AppDbContext context,UserManager<AppUser> userManager)
		{
			_context = context;
			_userManager = userManager;
		}

		public IActionResult Index()
		{
			var jsonCookie = Request.Cookies["Basket"];
			List<BasketItemVM> basketVMs = new List<BasketItemVM>();
			bool Coutcheck = false;

			if (jsonCookie != null)
			{
				var tempBasketVMs = JsonConvert.DeserializeObject<List<BasketItemVM>>(jsonCookie);

				for (int i = tempBasketVMs.Count - 1; i >= 0; i--)
				{
					var item = tempBasketVMs[i];

					Book product = _context.Books.Where(p=>p.IsDeleted==false)
						.Include(p => p.BookImages.Where(pi => pi.IsPrime == true))
						.FirstOrDefault(p => p.Id == item.Id);

					if (product == null)
					{
						Coutcheck = true;
						tempBasketVMs.RemoveAt(i);
						continue;
					}

					basketVMs.Add(new BasketItemVM()
					{
						Id = item.Id,
						Name = product.Title,
						ImgUrl = product.BookImages.FirstOrDefault()?.ImgUrl,
						Count = item.Count,
						Price = product.Price
					});
				}

				if (Coutcheck)
				{
					Response.Cookies.Append("Basket", JsonConvert.SerializeObject(tempBasketVMs));
				}
			}

			return View(basketVMs);
		}

		public  async Task<IActionResult> AddBasket(int id)
		{
			if (id <= 0)
			{
				return BadRequest();
			}

			Book product = _context.Books.Where(p => p.IsDeleted == false).FirstOrDefault(p => p.Id == id);

			if (product == null)
			{
				return NotFound();
			}
			if (User.Identity.IsAuthenticated)
			{

				AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

				BasketItem oldItem = _context.BasketItems.FirstOrDefault(b => b.AppUserId == user.Id && b.BookId==id);

				if (oldItem == null)
				{
					BasketItem newItem = new BasketItem()
					{
						AppUser = user,
						Book = product,
						Price = product.Price,
						Count = 1

					};

					_context.BasketItems.Add(newItem);

				}
				else
				{
					oldItem.Count += 1;
				}
				
				await _context.SaveChangesAsync();
			}

			else { 

				List<BasketItemVM> baskets;
				var json = Request.Cookies["Basket"];

				if (json != null)
				{
					baskets = JsonConvert.DeserializeObject<List<BasketItemVM>>(json);
					var existingProduct = baskets.FirstOrDefault(p => p.Id == id);

					if (existingProduct != null)
					{
						existingProduct.Count += 1;
					}
					else
					{
						baskets.Add(new BasketItemVM()
						{
							Id = id,
							Count = 1
						});
					}
				}
				else
				{
					baskets = new List<BasketItemVM>();
					baskets.Add(new BasketItemVM()
					{
						Id = id,
						Count = 1
					});
				}

				var cookiebasket = JsonConvert.SerializeObject(baskets);
				Response.Cookies.Append("Basket", cookiebasket);
			}

			return RedirectToAction(nameof(Index), "Home");
		}


        public IActionResult RemoveBasketItem(int Id)
        {
            var cookieBasket = Request.Cookies["Basket"];
            if (cookieBasket != null)
            {
                List<CookieItemVm> basket = JsonConvert.DeserializeObject<List<CookieItemVm>>(cookieBasket);

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


        public IActionResult GetBasket()
		{
			var basketJson = Request.Cookies["Basket"];
			return Content(basketJson);
		}
	}
}