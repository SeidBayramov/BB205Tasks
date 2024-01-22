using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok_Temp.DAL;
using Pustok_Temp.Helpers;
using Pustok_Temp.Models;

namespace Pustok_Temp.Areas.Manage.Controllers
{

    [Area("Manage")]
    public class BookController : Controller
    {
        AppDbContext _context;
        private readonly IWebHostEnvironment _environment;


        public BookController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
        public async Task<IActionResult> Index()
        {
            List<Book> books = await _context.books
                .Include(p => p.Authors)
                .Include(p=>p.Bookimages).
                ToListAsync();

            return View(books);
         
        }
        public IActionResult Create()
        {

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (!book.ImageFile.ContentType.Contains("image"))
            {
                ModelState.AddModelError("ImageFile", "You can only upload image file");
                return View();
            }
            if (book.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "You cannort upload image more than 2MB");
                return View();
            }

            book.ImgUrl = book.ImageFile.Upload(_environment.WebRootPath, @"\Upload\BookImage\");

            if (!ModelState.IsValid)
            {
                return View();
            }

            await _context.books.AddAsync(book);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public   async Task<IActionResult> Delete(int id)
        {

            Book book =  await _context.books.FindAsync(id);

            if (book != null)
            {
                _context.books.Remove(book);
                _context.SaveChangesAsync();

            }

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int id)
        {

            Book book = await _context.books.FindAsync(id);
            return View(book);
        }

        [HttpPost]
        public IActionResult Update(Book newSlider)
        {
            Book oldSlider = _context.books.Find(newSlider.Id);

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



            FileManager.DeleteFile(oldSlider.ImgUrl, _environment.WebRootPath, @"\Upload\BookImage\");
            newSlider.ImgUrl = newSlider.ImageFile.Upload(_environment.WebRootPath, @"\Upload\BookImage\");
            if (!ModelState.IsValid)
            {
                return View();
            }


            oldSlider.Title = newSlider.Title;
            oldSlider.Price = newSlider.Price;
            oldSlider.ImgUrl = newSlider.ImgUrl;
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}