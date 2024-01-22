using Microsoft.AspNetCore.Mvc;

namespace Pustok_Temp.Areas.Manage.Controllers
{

    [Area("Manage")]
    public class AdminController : Controller
    {
        AppDbContext _db;
        public AdminController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
