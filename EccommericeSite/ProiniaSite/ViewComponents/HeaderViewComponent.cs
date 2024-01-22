

namespace ProiniaSite.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {

        AppDbContext _dbContext;

        public HeaderViewComponent(AppDbContext dbContext)
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

