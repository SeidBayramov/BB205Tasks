using Microsoft.AspNetCore.Mvc;

namespace FinalProcetStudent.Conroller
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
