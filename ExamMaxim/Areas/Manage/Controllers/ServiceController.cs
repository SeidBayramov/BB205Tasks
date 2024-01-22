using ExamMaxim.Areas.Manage.ViewModels;
using ExamMaxim.DAL.Context;
using ExamMaxim.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExamMaxim.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles ="Admin")]
    public class ServiceController : Controller
    {
        private readonly AppDbContext _context;
        public ServiceController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var service = await _context.Services.ToListAsync();
            return View(service);
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(ServiceCreateVm createVm)
        {
            if (!ModelState.IsValid)
            {
                return View(createVm);
            }
            Service service = new Service()
            {
                Title = createVm.Title,
                Description = createVm.Description,
                IconUrl = createVm.IconUrl,
            };
            
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int id)
        {
            if(id <= 0 && id==null)
            {
                throw new ArgumentException();
            }
            
            var service= await _context.Services.Where(c=>c.Id==id).FirstOrDefaultAsync();

            if (service == null)
            {
                return View();
            }
            ServiceUpdateVm serviceUpdateVm = new ServiceUpdateVm()
            {
                Id = id,
                Title = service.Title,
                Description = service.Description,
                IconUrl = service.IconUrl,
            };

            return View(serviceUpdateVm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(ServiceUpdateVm updateVm)
        {
            if (!ModelState.IsValid)
            {
                return View(updateVm);
            }
            var existservice= await _context.Services.Where(c=>c.Id==updateVm.Id).FirstOrDefaultAsync();

            if (existservice == null)
            {
                throw new Exception();
            }
            existservice.Title = updateVm.Title;
            existservice.Description = updateVm.Description;
            existservice.IconUrl = updateVm.IconUrl;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            if(id<=0 && id == null)
            {
                throw new ArgumentException();
                return View();
            }
            var service = await _context.Services.Where(c => c.Id == id).FirstOrDefaultAsync();
             _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
