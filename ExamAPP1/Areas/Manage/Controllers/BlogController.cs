using ExamAPP1.Areas.Manage.ViewModels;
using ExamAPP1.DAL;
using ExamAPP1.Entities;
using ExamAPP1.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ExamAPP1.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize]
    public class BlogController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public BlogController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            List<Blog> blogs = await _context.Blogs.ToListAsync();
            return View(blogs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(BlogCreateVm createVm)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!createVm.ImageFile.CheckContent("image/"))
            {
                ModelState.AddModelError("Image", "Please check your image type");
                return View();
            }

            if (!createVm.ImageFile.CheckLenght(3000000))
            {
                ModelState.AddModelError("Image", "3mb dan boyukdur ");
                return View();
            }

            Blog blog = new Blog()
            {
                Title = createVm.Title,
                Description = createVm.Description,
                ImageUrl = createVm.ImageFile.Upload(_env.WebRootPath, @"\Upload\Blog\")
            };
            await _context.AddAsync(blog);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id <= 0)
            {
                return View();
            }
            Blog blog = await _context.Blogs.Where(c => c.Id == id).FirstOrDefaultAsync();

            if (blog == null) { return View(); }


            BlogUpdateVm vm = new BlogUpdateVm()
            {
                Id = blog.Id,
                Title = blog.Title,
                Description = blog.Description,
                ImageUrl = blog.ImageUrl,
            };

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(BlogUpdateVm updateVm)
        {
            Blog exist = await _context.Blogs.Where(c => c.Id == updateVm.Id).FirstOrDefaultAsync();

            if (exist == null) { return View(); }

            if (!ModelState.IsValid) { return View(); }

       
            if (updateVm.ImageFile == null)
            {
                exist.Title = updateVm.Title;
                exist.Description = updateVm.Description;
                await _context.SaveChangesAsync();
            }
            else {

                if (!updateVm.ImageFile.CheckContent("image/"))
                {
                    ModelState.AddModelError("Image", "Please check your image type");
                    return View();
                }

                if (!updateVm.ImageFile.CheckLenght(3000000))
                {
                    ModelState.AddModelError("Image", "3mb dan boyukdur ");
                    return View();
                }

                exist.Title = updateVm.Title;
                exist.Description = updateVm.Description;
                exist.ImageUrl = updateVm.ImageFile.Upload(_env.WebRootPath, @"\Upload\Blog\");

                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int Id)
        {
            Blog blog = await _context.Blogs.Where(c => c.Id == Id).FirstOrDefaultAsync();
            if (blog == null) { return View(); }
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}