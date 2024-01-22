using Azure;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pustok_Temp.ViewModels.ProiniaSite.ViewModel;

namespace Pustok_Temp.ViewComponents
{
	public class BookViewComponent:ViewComponent
	{

		AppDbContext _context;

		public BookViewComponent(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			var jsonCookie =Request.Cookies["Basket"];
			List<BasketItemVM> basketVMs = new List<BasketItemVM>();
			bool Coutcheck = false;

			if (jsonCookie != null)
			{
				var tempBasketVMs = JsonConvert.DeserializeObject<List<BasketItemVM>>(jsonCookie);

				for (int i = tempBasketVMs.Count - 1; i >= 0; i--)
				{
					var item = tempBasketVMs[i];

					Book product = _context.Books.Where(p => p.IsDeleted == false)
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
			}

			return View(basketVMs);
		}
	}
}


