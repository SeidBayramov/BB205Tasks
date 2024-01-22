
namespace Pustok_Temp.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class CategoryController : Controller
    {
        AppDbContext _context;
        public CategoryController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            AdminVM adminVM = new AdminVM();
            adminVM.categories = _context.Categories.ToList();
            return View(adminVM);
        }
        public IActionResult Create()
        {
            ICollection<Category> categories = _context.Categories.ToList();
            CreateCategoryVM categoryVM = new CreateCategoryVM()
            {
                categories = categories
            };
            return View(categoryVM);
        }
        [HttpPost]
        public IActionResult Create(CreateCategoryVM categoryVM)
        {
            Category category = new Category();
            if (!ModelState.IsValid)
            {
                return View("Error");
            }
            if (categoryVM.ParentCategoryId != "null")
            {
                category.ParentCategoryId = int.Parse(categoryVM.ParentCategoryId);
            }
            category.Name = categoryVM.Name;

            _context.Categories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Update(int Id)
        {
            ICollection<Category> categories = await _context.Categories.ToListAsync();
            Category category = await _context.Categories.FindAsync(Id);
            CreateCategoryVM categoryVM = new CreateCategoryVM
            {
                Id = Id,
                Name = category.Name,
                ParentCategoryId = $"{category.ParentCategoryId}",
                categories = categories
            };

            return View(categoryVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(CreateCategoryVM categoryVM)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Category oldCategory = await _context.Categories.FindAsync(categoryVM.Id);

            oldCategory.Name = categoryVM.Name;
            if (categoryVM.ParentCategoryId != "null")
            {
                oldCategory.ParentCategoryId = int.Parse(categoryVM.ParentCategoryId);
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            Category category = _context.Categories.Find(id);
            foreach (var item in _context.Categories.ToList())
            {
                if (item.ParentCategoryId == category.Id)
                {
                    _context.Categories.Remove(item);
                }
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
