using FrontToBack.DAL;
using FrontToBack.Models;
using FrontToBack.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FrontToBack.Controllers
{
    public class HomeController : Controller
    {


        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {

            _context = context;
        }


        public IActionResult Index()
        {
            HomeVM homeVM = new HomeVM()
            {
                Sliders = _context.sliders.ToList(),
                Card = _context.cards.ToList(),
                Services= _context.services.OrderBy(x => x.Time).Take(6).ToList()

        };
            return View(homeVM);
        }
    }
}

