using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pustok_Temp.DAL;
using Pustok_Temp.Helpers;
using Pustok_Temp.Models;

namespace Pustok_Temp.Areas.Manage.Controllers
{

    [Area("Manage")]
	[Authorize(Roles = "Admin")]
	public class BookController : Controller
    {
        private readonly IWebHostEnvironment _environment;
        AppDbContext _context;
        public BookController(AppDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }
		#region Index
		public IActionResult Table ()
        {
            AdminVM adminVM = new AdminVM();
            adminVM.books = _context.Books.Where(p => p.IsDeleted == false)
                .Include(b => b.Category)
                .Include(b => b.BookImages)
                .Include(b => b.BookTags)
                .ThenInclude(bt => bt.Tag)
                .ToList();
            return View(adminVM);
        }
        #endregion 
        #region Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Tags = await _context.Tags.ToListAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateBookVM createBookVM)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Tags = await _context.Tags.ToListAsync();

            if (createBookVM is null) { return View("Error"); }
            if (!ModelState.IsValid) { return View(); }

            bool resultCategory = await _context.Categories.AnyAsync(c => c.Id == createBookVM.CategoryId);
            if (!resultCategory)
            {
                ModelState.AddModelError("CategoryId", "There is no such category like that");
                return View();
            }
            Book book = new Book()
            {
                Title = createBookVM.Title,
                Description = createBookVM.Description,
                Author = createBookVM.Author,
                BookCode = createBookVM.BookCode,
                Price = createBookVM.Price,
                ImgUrl = createBookVM.ImgUrl,
                CategoryId = createBookVM.CategoryId,
                Tag = createBookVM.Tag,
                BookImages = new List<BookImages>()
            };

            if (createBookVM.TagIds != null)
            {
                foreach (int tagId in createBookVM.TagIds)
                {
                    bool resultTag = await _context.Tags.AnyAsync(c => c.Id == tagId);
                    if (!resultTag)
                    {
                        ModelState.AddModelError("TagIds", $"There is no such tag with this ID: {tagId}");
                        return View();
                    }

                    BookTag bookTag = new BookTag()
                    {
                        Book = book,
                        TagId = tagId
                    };

                    _context.BookTags.Add(bookTag);
                }
            }

            if (!createBookVM.MainPhoto.CheckContent("image/"))
            {
                ModelState.AddModelError("MainPhoto", "Duzgun format daxil edin");
                return View();
            }
            if (!createBookVM.HoverPhoto.CheckContent("image/"))
            {
                ModelState.AddModelError("MainPhoto", "Duzgun format daxil edin");
                return View();
            }
            BookImages mainImage = new BookImages()
            {
                IsPrime = true,
                Book = book,
                ImgUrl = createBookVM.MainPhoto.Upload(_environment.WebRootPath, "/Upload/BookImage/")
            };
            BookImages hoverImage = new BookImages()
            {
                IsPrime = false,
                Book = book,
                ImgUrl = createBookVM.MainPhoto.Upload(_environment.WebRootPath, "/Upload/BookImage/")
            };
            book.BookImages.Add(mainImage);
            book.BookImages.Add(hoverImage);

            TempData["Error"] = "";
            foreach (var item in createBookVM.Photos)
            {
                if (!item.CheckContent("image/"))
                {
                    TempData["Error"] += $"{item.FileName} is not Image File";
                    continue;
                }
                BookImages bookImages = new BookImages()
                {
                    IsPrime = null,
                    Book = book,
                    ImgUrl = item.Upload(_environment.WebRootPath, "/Upload/BookImage/")
                };
                book.BookImages.Add(bookImages);
            }

            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
            return RedirectToAction("Table");
        }
        #endregion
        #region Delete
        public IActionResult Delete(int id)
        {
            Book book = _context.Books.Where(p => p.IsDeleted == false).Include(b => b.BookImages).FirstOrDefault(b => b.Id == id);
            if (book is null)
            {
                return View("Error");
            }
            foreach (var item in book.BookImages)
            {
                item.ImgUrl.DeleteFile(_environment.WebRootPath, "/Upload/BookImage/");
            }

            _context.Books.Remove(book);
            _context.SaveChanges();

            return RedirectToAction("Table");
        }
        #endregion Delete
        #region Update
        public async Task<IActionResult> Update(int id)
        {
            Book book = await _context.Books.Where(p => p.IsDeleted == false)
                .Include(b => b.Category)
                .Include(b => b.BookTags)
                .ThenInclude(b => b.Tag)
                .Include(b => b.BookImages)
                .Where(b => b.Id == id)
                .FirstOrDefaultAsync();

            if (book is null) { return View("Error"); }
            ViewBag.Categories = await _context.Categories.ToListAsync();

            var tags = await _context.Tags.ToListAsync();
            ViewBag.Tags = tags;
            List<int> tagIds = new();
            foreach (var tag in tags)
                foreach (var item in book.BookTags)
                    if (tag.Id == item.TagId)
                        tagIds.Add(tag.Id);

            UpdateBookVM updateBookVM = new UpdateBookVM()
            {
                Id = id,
                Category = book.Category,
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                BookCode = book.BookCode,
                Price = book.Price,
                ImgUrl = book.ImgUrl,
                CategoryId = book.CategoryId,
                BookTags = book.BookTags,
                TagIds = tagIds,
                BookImagesVm = new List<BookImageVm>()
            };

            foreach (var item in book.BookImages)
            {
                BookImageVm bookImageVm = new BookImageVm()
                {
                    Id = item.Id,
                    ImgUrl = item.ImgUrl,
                    IsPrime = item.IsPrime
                };
                updateBookVM.BookImagesVm.Add(bookImageVm);
            }

            return View(updateBookVM);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateBookVM updateBookVM)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Tags = await _context.Tags.ToListAsync();

            Book existBook = await _context.Books
                .Include(b => b.Category)
                .Include(b => b.BookTags)
                .ThenInclude(b => b.Tag)
                .Include(b => b.BookImages).
                Where(b => b.Id == updateBookVM.Id).FirstOrDefaultAsync();
            if (existBook is null) { return View("Error"); }
            if (!ModelState.IsValid) { return View(); }

            bool resultCategory = await _context.Categories.AnyAsync(c => c.Id == updateBookVM.CategoryId);
            if (!resultCategory)
            {
                ModelState.AddModelError("CategoryId", "There is no such category like that");
                return View();
            }

            foreach (var item in updateBookVM.TagIds)
            {
                bool resultTag = await _context.Tags.AnyAsync(t => t.Id == item);
                if (!resultTag)
                {
                    ModelState.AddModelError("TagIds", "There is no such tag like that");
                    return View();
                }
            }
            List<int> newSelectTagId = updateBookVM.TagIds.Where(tagId => !existBook.BookTags.Exists(b => b.TagId == tagId)).ToList();
            existBook.ImgUrl = updateBookVM.ImgUrl;
            existBook.Title = updateBookVM.Title;
            existBook.Description = updateBookVM.Description;
            existBook.Author = updateBookVM.Author;
            existBook.Price = updateBookVM.Price;
            existBook.BookCode = updateBookVM.BookCode;
            existBook.ImgUrl = updateBookVM.ImgUrl;
            existBook.CategoryId = updateBookVM.CategoryId;

            foreach (var newTagId in newSelectTagId)
            {
                BookTag bookTag = new BookTag()
                {
                    TagId = newTagId,
                    BookId = updateBookVM.Id,
                };
                _context.BookTags.Add(bookTag);
            }
            List<BookTag> removedTag = existBook.BookTags.Where(bt => !updateBookVM.TagIds.Contains(bt.TagId)).ToList();
            _context.BookTags.RemoveRange(removedTag);

            if (updateBookVM.MainPhoto is not null)
            {
                if (!updateBookVM.MainPhoto.CheckContent("image/"))
                {
                    ModelState.AddModelError("MainPhoto", "Please enter correct format");
                    return View();
                }
                BookImages existMainPhoto = existBook.BookImages.FirstOrDefault(p => p.IsPrime == true);
                existMainPhoto.ImgUrl.DeleteFile(_environment.WebRootPath, @"\Upload\BookImage\");
                existBook.BookImages.Remove(existMainPhoto);
                BookImages bookImage = new BookImages()
                {
                    ImgUrl = updateBookVM.MainPhoto.Upload(_environment.WebRootPath, @"\Upload\BookImage\"),
                    BookId = existBook.Id,
                    IsPrime = true
                };
                existBook.BookImages.Add(bookImage);
            }
            if (updateBookVM.HoverPhoto is not null)
            {
                if (!updateBookVM.HoverPhoto.CheckContent("image/"))
                {
                    ModelState.AddModelError("HoverPhoto", "Please enter correct format");
                    return View();
                }
                BookImages existHoverPhoto = existBook.BookImages.FirstOrDefault(p => p.IsPrime == false);
                existHoverPhoto.ImgUrl.DeleteFile(_environment.WebRootPath, @"\Upload\BookImage\");
                existBook.BookImages.Remove(existHoverPhoto);
                BookImages bookImage = new BookImages()
                {
                    ImgUrl = updateBookVM.HoverPhoto.Upload(_environment.WebRootPath, @"\Upload\BookImage\"),
                    BookId = existBook.Id,
                    IsPrime = false
                };
                existBook.BookImages.Add(bookImage);
            }

            updateBookVM.ImageIds = new();

            List<BookImages> removeList = existBook.BookImages.Where(b => !updateBookVM.ImageIds.Exists(imgId => imgId == b.Id) && b.IsPrime == null).ToList();
            if (removeList.Count > 0)
            {
                foreach (var item in removeList)
                {
                    existBook.BookImages.Remove(item);
                    item.ImgUrl.DeleteFile(_environment.WebRootPath, @"\Upload\BookImage\");
                }
            }

            TempData["Error"] = "";
            if (updateBookVM.Photos is not null)
            {
                foreach (IFormFile addImage in updateBookVM.Photos)
                {
                    if (!addImage.CheckContent("image/"))
                    {
                        TempData["Error"] += $"{addImage.FileName} is not Image File";
                        continue;
                    }
                    BookImages bookImages = new BookImages()
                    {
                        IsPrime = null,
                        ImgUrl = addImage.Upload(_environment.WebRootPath, "/Upload/BookImage/"),
                        BookId = existBook.Id
                    };
                    existBook.BookImages.Add(bookImages);
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Table");
        }
        #endregion Update
    }
}