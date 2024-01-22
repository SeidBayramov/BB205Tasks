using DianaTemp.DAL;
using DianaTemp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DianaTemp.Controllers
{
	public class HomeController : Controller
	{
			AppDbContext _context;

			public HomeController(AppDbContext context)
			{
            _context =context;
			}

			public async Task<IActionResult> Index()
			{

            HomeVm vm = new HomeVm();
            vm.Products = _context.Products
                .Include(p => p.ProductImages)
                .Include(p => p.ProductColours)
                .ThenInclude(p => p.Colour)
                .Include(p => p.ProductSizes)
                .ThenInclude(p => p.Size)
                .Include(p => p.ProductMaterial)
                .ThenInclude(p => p.Material).ToList();
            vm.Categories = _context.Categories.ToList();
            vm.Colors = _context.Colours.ToList();
            vm.Sizes = _context.Sizes.ToList();
            vm.Materials = _context.Materials.ToList();
            return View(vm);
        }
		}
	}