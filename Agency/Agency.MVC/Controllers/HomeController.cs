using Agency.Busines.Services.Interface;
using Agency.Core.Entities;
using AutoMapper.Features;
using Microsoft.AspNetCore.Mvc;

namespace Agency.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPortfolioService _service;

        public HomeController(IPortfolioService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Portfolio> portfolios = await _service.PortfolioGetAllAsync();

            return View(portfolios);
        }
    }
}
