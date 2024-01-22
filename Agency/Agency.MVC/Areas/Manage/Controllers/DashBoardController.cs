using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Agency.MVC.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
