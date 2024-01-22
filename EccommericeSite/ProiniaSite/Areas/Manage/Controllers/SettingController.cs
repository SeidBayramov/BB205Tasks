

using Microsoft.AspNetCore.Authorization;

namespace ProiniaSite.Areas.Manage.Controllers
{
        [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class SettingController : Controller
    {

        AppDbContext _context;

        public SettingController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Setting> settings = _context.Settings.ToList();
            return View(settings);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create( Setting setting)
        {
            if (ModelState.IsValid)
            {
                _context.Settings.Add(setting);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(setting);
        }

        public IActionResult Update(int id)
        {
            Setting setting = _context.Settings.Find(id);

            if (setting == null)
            {
                return NotFound();
            }

            return View(setting);
        }

        [HttpPost]
        public IActionResult Update(Setting newSetting)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Setting oldSetting = _context.Settings.Find(newSetting.Id);

            if (oldSetting == null)
            {
                return View();
            }

            oldSetting.Key = newSetting.Key;
            oldSetting.Value = newSetting.Value;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }




        public IActionResult Delete(int id)
        {
            var setting = _context.Settings.FirstOrDefault(s => s.Id == id);

            if (setting == null)
            {
                return NotFound();
            }

            _context.Settings.Remove(setting);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
