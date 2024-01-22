using JofExam.Areas.Manage.ViewModel;
using JofExam.DAL.Context;
using JofExam.Models;
using JofExam.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Authorization;

namespace JofExam.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles ="Admin")]
    public class FruitController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public FruitController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            var fruit= await _context.Fruits.ToListAsync();
            return View(fruit);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(FruitCreateVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (!vm.Image.CheckImage())
            {
                ModelState.AddModelError("Image", "Please check your image type or length");
                return View();
            }

            var fruit = new Fruit()
            {
                Name = vm.Name,
                SubTitle = vm.SubTitle,
                ImageUrl = vm.Image.Upload(_env.WebRootPath, "Upload")
            };
            await _context.AddAsync(fruit);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null || id <= 0)
            {
                return View();
            }

            var fruit = await _context.Fruits.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (fruit == null)
            {
                return View();
            }
            FruitUpdateVm updateVm = new FruitUpdateVm()
            {
                Id = fruit.Id,
                Name = fruit.Name,
                SubTitle = fruit.SubTitle,
                ImageUrl=fruit.ImageUrl
            };
            return View(updateVm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(FruitUpdateVm vm)
        {
            Fruit existFruit=  await _context.Fruits.Where(x=>x.Id==vm.Id).FirstOrDefaultAsync();
            if (existFruit == null)
            {
                return View(vm);
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (vm.Image == null)
            {
                existFruit.Name = vm.Name;
                existFruit.SubTitle = vm.SubTitle;
                await _context.SaveChangesAsync();
            }
            else
            {
                if (!vm.Image.CheckImage())
                {
                    ModelState.AddModelError("Image", "Please check your image type or length");
                    return View();
                }

                existFruit.Name = vm.Name;
                existFruit.SubTitle = vm.SubTitle;
                existFruit.ImageUrl = vm.Image.Upload(_env.WebRootPath, "Upload");

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
            
        }

        public async Task<IActionResult> Delete(int id)
        {
            var fruit = await _context.Fruits.Where(c => c.Id == id).FirstOrDefaultAsync();
            if (fruit == null) { return View(); }
            _context.Fruits.Remove(fruit);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
