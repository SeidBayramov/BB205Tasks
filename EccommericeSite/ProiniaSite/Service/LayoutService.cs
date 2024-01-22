using Newtonsoft.Json;
using ProiniaSite.DAL;

namespace ProiniaSite.Service
{
    public class LayoutService
    {
        AppDbContext _context;
        IHttpContextAccessor _http;

        public LayoutService(AppDbContext context, IHttpContextAccessor http)
        {
            _context = context;
            _http = http;
        }

        public async Task<Dictionary<string, string>> GetSetting()
        {
            Dictionary<string, string> setting = _context.Settings.ToDictionary(s => s.Key, s => s.Value);
            return setting;
        }
        public async Task<List<BasketItemVm>> GetBasket()
        {
            var jsonCookie = _http.HttpContext.Request.Cookies["Basket"];
            List<BasketItemVm> basketItems = new List<BasketItemVm>();
            if (jsonCookie != null)
            {
                var cookieItems = JsonConvert.DeserializeObject<List<CookieItemVm>>(jsonCookie);

                bool countCheck = false;
                List<CookieItemVm> deletedCookie = new List<CookieItemVm>();
                foreach (var item in cookieItems)
                {
                    Product product = await _context.Products.Where(p => p.IsDeleted == false).Include(p => p.ProductImages.Where(p => p.IsPrime == true)).FirstOrDefaultAsync(p => p.Id == item.Id);
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
                    _http.HttpContext.Response.Cookies.Append("Basket", JsonConvert.SerializeObject(cookieItems));
                }



            }
            return basketItems;
        }
    }
}