using AutoMapper;
using Carvilla.Areas.Manage.ViewModels;
using Carvilla.DAL.Context;
using Carvilla.Helpers;
using Carvilla.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Carvilla.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CarController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        private readonly IMapper _map;

        public CarController(AppDbContext context, IWebHostEnvironment env, IMapper map)
        {
            _context = context;
            _env = env;
            _map = map;
        }

        public async Task<IActionResult> Index()
        {
            var car = await _context.Cars.ToListAsync();
            return View(car);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CarCreateVm vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            if (!vm.Image.CheckImage())
            {
                ModelState.AddModelError("", "Please check photo type or length");
                return View(vm);
            }

            var car = _map.Map<Car>(vm);
            car.ImageUrl = vm.Image.Upload(_env.WebRootPath, "Upload");

         
            await _context.AddAsync(car);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Car");

        }
        public async Task<IActionResult>Update(int id)
        {
            if(id == 0)
            {
                throw new Exception();
            }

            var car = await _context.Cars.Where(x => x.Id == id).FirstOrDefaultAsync();
            if (car == null)
            {

                ModelState.AddModelError("", "Sehvlik bas verdi");
                return View();
            }

            var updateVm= _map.Map<CarUpdateVm>(car);

           
            return View(updateVm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CarUpdateVm vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Sehvlik bas verdi");
                    return View(vm);
                }
                var oldcar = await _context.Cars.Where(x => x.Id == vm.Id).FirstOrDefaultAsync();
                if (oldcar == null)
                {
                    ModelState.AddModelError("", "Sehvlik bas verdi");
                    return View(vm);
                }
              

                if (vm.Image != null)
                {
                    if (!vm.Image.CheckImage())
                    {
                        ModelState.AddModelError("", "Please check photo type or length");
                        return View(vm);
                    }

                    _map.Map(vm, oldcar);
                    oldcar.ImageUrl = vm.Image.Upload(_env.WebRootPath, "Upload");
                    await _context.SaveChangesAsync();

             
                }

                oldcar.Price = vm.Price;
                oldcar.Description = vm.Description;
                oldcar.Name = vm.Name;

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            catch (DbUpdateException ex)
            {
                ModelState.AddModelError("", "Databasa da deyisiklik zamani xeta bas verdi yeniden cehd edin");
                return View(vm);
               
            }

        }
        public async Task<IActionResult> Delete(int id)
        {
           if(id == 0)
            {
                throw new Exception();
            }
            var car = await _context.Cars.Where(x => x.Id == id).FirstOrDefaultAsync();

            if(car == null)
            {
                ModelState.AddModelError("", "Sehvlik bas verdi");
            }

            _context.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");

        }

    }
}
