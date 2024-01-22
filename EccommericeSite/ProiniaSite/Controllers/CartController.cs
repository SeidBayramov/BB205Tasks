using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProiniaSite.ViewModel;
using System.Collections.Generic;
using System.Linq;

namespace ProiniaSite.Controllers
{
    public class CartController : Controller
    {
        AppDbContext _context;

        public CartController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {

            var jsonCookie = Request.Cookies["Basket"];
            List<BasketItemVm> basketItems = new List<BasketItemVm>();
            if (jsonCookie != null)
            {
                var cookieItems = JsonConvert.DeserializeObject<List<CookieItemVm>>(jsonCookie);

                bool countCheck = false;
                List<CookieItemVm> deletedCookie = new List<CookieItemVm>();
                foreach (var item in cookieItems)
                {
                    Product product = _context.Products.Where(p => p.IsDeleted == false).Include(p => p.ProductImages.Where(p => p.IsPrime == true)).FirstOrDefault(p => p.Id == item.Id);
                    if (product == null)
                    {
                        deletedCookie.Add(item);
                        continue;
                    }

                    basketItems.Add(new BasketItemVm()
                    {
                        Id = item.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Count = item.Count,
                        ImgUrl = product.ProductImages.FirstOrDefault().ImgUrl
                    });
                }
                if (deletedCookie.Count > 0)
                {
                    foreach (var delete in deletedCookie)
                    {
                        cookieItems.Remove(delete);
                    }
                    Response.Cookies.Append("Basket", JsonConvert.SerializeObject(cookieItems));
                }


            }
            return View(basketItems);
        }



        public IActionResult AddBasket(int id)
        {
            if (id <= 0) return BadRequest();
            Product product = _context.Products.Where(p => p.IsDeleted == false).FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            List<CookieItemVm> basket;
            var json = Request.Cookies["Basket"];

            if (json != null)
            {
                basket = JsonConvert.DeserializeObject<List<CookieItemVm>>(json);
                var existProduct = basket.FirstOrDefault(p => p.Id == id);
                if (existProduct != null)
                {
                    existProduct.Count += 1;
                }
                else
                {
                    basket.Add(new CookieItemVm()
                    {
                        Id = id,
                        Count = 1
                    });
                }

            }
            else
            {
                basket = new List<CookieItemVm>();
                basket.Add(new CookieItemVm()
                {
                    Id = id,
                    Count = 1
                });
            }


            var cookieBasket = JsonConvert.SerializeObject(basket);
            Response.Cookies.Append("Basket", cookieBasket);






            return RedirectToAction(nameof(Index), "Home");
        }
        public IActionResult RemoveBasketItem(int id)
        {
            var cookieBasket = Request.Cookies["Basket"];
            if (cookieBasket != null)
            {
                List<CookieItemVm> basket = JsonConvert.DeserializeObject<List<CookieItemVm>>(cookieBasket);

                var deleteElement = basket.FirstOrDefault(p => p.Id == id);
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
            var basketCookieJson = Request.Cookies["Basket"];

            return Content(basketCookieJson);
        }
    }
}