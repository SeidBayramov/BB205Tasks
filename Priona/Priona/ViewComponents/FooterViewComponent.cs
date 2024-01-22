using Microsoft.AspNetCore.Mvc;
using Priona.DAL.Context;

namespace Priona.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {

        AppDbContext _dbContext;

        public FooterViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var setting = _dbContext.Settings.ToDictionary(x => x.Key, x => x.Value);

            return View(setting);
        }
    }
}
