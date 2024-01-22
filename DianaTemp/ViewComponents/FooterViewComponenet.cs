namespace DianaTemp.ViewComponents
{
    public class FooterViewComponent:ViewComponent
    {
        private readonly AppDbContext _context;

        public FooterViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            Dictionary<string, string> settings = _context.Settings.ToDictionary(s => s.Key, s => s.Value);

            return View(settings);
        }
    }
}
