

using Microsoft.AspNetCore.Authorization;

namespace ProiniaSite.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class SliderController : Controller
    {
        AppDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public SliderController(AppDbContext context, IWebHostEnvironment _environment)
        {
            _context = context;
            this._environment = _environment;
        }

        public IActionResult Index()
        {
            List<Sliders> sliderList = _context.Sliders.ToList();
            return View(sliderList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Sliders slider)
        {
            if (!slider.ImageFile.ContentType.Contains("image"))
            {
                ModelState.AddModelError("ImageFile", "You can only upload image file");
                return View();
            }
            if (slider.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "You cannort upload image more than 2MB");
                return View();
            }

            slider.ImgUrl = slider.ImageFile.Upload(_environment.WebRootPath, @"\Upload\SliderImage\");

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            var slider = _context.Sliders.FirstOrDefault(s => s.Id == id);

            _context.Sliders.Remove(slider);
            _context.SaveChanges();

            FileManager.DeleteFile(slider.ImgUrl, _environment.WebRootPath, @"\Upload\SliderImage\");
            return RedirectToAction("Index");
        }


        public IActionResult Update(int Id)
        {
            Sliders slider = _context.Sliders.Find(Id);
            return View(slider);
        }



        [HttpPost]
        public IActionResult Update(Sliders newSlider)
        {
            Sliders oldSlider = _context.Sliders.Find(newSlider.Id);

            if (!newSlider.ImageFile.ContentType.Contains("image"))
            {
                ModelState.AddModelError("ImageFile", "You can only upload image file");
                return View();
            }



            if (newSlider.ImageFile.Length > 2097152)
            {
                ModelState.
                AddModelError("ImageFile", "You cannort upload image more than 2MB");
                return View();
            }



            FileManager.DeleteFile(oldSlider.ImgUrl, _environment.WebRootPath, @"\Upload\SliderImage\");
            newSlider.ImgUrl = newSlider.ImageFile.Upload(_environment.WebRootPath, @"\Upload\SliderImage\");
            if (!ModelState.IsValid)
            {
                return View();
            }



            oldSlider.Title = newSlider.Title;
            oldSlider.SubTitle = newSlider.SubTitle;
            oldSlider.Description = newSlider.Description;
            oldSlider.ImgUrl = newSlider.ImgUrl;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}