﻿
using Microsoft.AspNetCore.Authorization;

namespace ProiniaSite.Areas.Manage.Controllers
{

    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
