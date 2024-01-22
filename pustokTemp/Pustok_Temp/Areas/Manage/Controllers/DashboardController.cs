using Microsoft.AspNetCore.Mvc;

namespace Pustok_Temp.Areas.Manage.Controllers
{

    [Area("Manage")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}


