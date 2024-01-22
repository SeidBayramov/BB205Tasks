using JofExam.DAL.Context;
using JofExam.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace JofExam.Controllers
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
            var fruit=_context.Fruits.ToList();
            return View(fruit);
        }
 
    }
}
