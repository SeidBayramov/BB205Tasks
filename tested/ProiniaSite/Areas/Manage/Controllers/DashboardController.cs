﻿using Microsoft.AspNetCore.Mvc;

namespace ProiniaSite.Areas.Manage.Controllers
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
