using Microsoft.AspNetCore.Mvc;
using Pustok_Temp.Helpers;

namespace Pustok_Temp.Areas.Manage.Controllers
{
       [Area("Manage")]
        public class BlogController : Controller
        {
            AppDbContext _context;
            private readonly IWebHostEnvironment _environment;
            public BlogController(AppDbContext context, IWebHostEnvironment environment)
            {
                _context = context;
                _environment = environment;
            }
            #region Index
            public IActionResult Table()
            {
                AdminVM adminVM = new AdminVM();
                adminVM.blogs = _context.Blogs
                    .Include(b => b.BlogImages)
                    .Include(b => b.BlogTags)
                    .ThenInclude(bt => bt.Tag)
                    .ToList();
                return View(adminVM);
            }
            #endregion
            #region Create
            public async Task<IActionResult> Create()
            {
                ViewBag.Tags = await _context.Tags.ToListAsync();
                return View();
            }
            [HttpPost]
            public async Task<IActionResult> Create(CreateBlogVM createBlogVM)
            {
                ViewBag.Tags = await _context.Tags.ToListAsync();

                if (createBlogVM is null) { return View("Error"); }
                if (!ModelState.IsValid) { return View(); }

                Blog blog = new Blog()
                {
                    Title = createBlogVM.Title,
                    Description = createBlogVM.Description,
                    ImgUrl = createBlogVM.ImgUrl,
                    Tag = createBlogVM.Tag,
                    BlogImages = new List<BlogImages>()
                };

                if (createBlogVM.TagIds != null)
                {
                    foreach (int tagId in createBlogVM.TagIds)
                    {
                        bool resultTag = await _context.Tags.AnyAsync(c => c.Id == tagId);
                        if (!resultTag)
                        {
                            ModelState.AddModelError("TagIds", $"There is no such tag with this ID: {tagId}");
                            return View();
                        }

                        BlogTag blogTag = new BlogTag()
                        {
                            Blog = blog,
                            TagId = tagId
                        };

                        _context.BlogTags.Add(blogTag);
                    }
                }

                if (!createBlogVM.MainPhoto.CheckContent("image/"))
                {
                    ModelState.AddModelError("MainPhoto", "Duzgun format daxil edin");
                    return View();
                }

                if (!createBlogVM.HoverPhoto.CheckContent("image/"))
                {
                    ModelState.AddModelError("MainPhoto", "Duzgun format daxil edin");
                    return View();
                }
                BlogImages mainImage = new BlogImages()
                {
                    IsPrime = true,
                    Blog = blog,
                    ImgUrl = createBlogVM.MainPhoto.Upload(_environment.WebRootPath, "/Upload/BlogImages/")
                };
                BlogImages hoverImage = new BlogImages()
                {
                    IsPrime = false,
                    Blog = blog,
                    ImgUrl = createBlogVM.MainPhoto.Upload(_environment.WebRootPath, "/Upload/BlogImages/")
                };
                blog.BlogImages.Add(mainImage);
                blog.BlogImages.Add(hoverImage);

                TempData["Error"] = "";
                foreach (var item in createBlogVM.Photos)
                {
                    if (!item.CheckContent("image/"))
                    {
                        TempData["Error"] += $"{item.FileName} is not Image File";
                        continue;
                    }
                    BlogImages BlogImages = new BlogImages()
                    {
                        IsPrime = null,
                        Blog = blog,
                        ImgUrl = item.Upload(_environment.WebRootPath, "/Upload/BlogImages/")
                    };
                    blog.BlogImages.Add(BlogImages);
                }

                await _context.Blogs.AddAsync(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            #endregion
            #region Update
            public async Task<IActionResult> Update(int id)
            {
                Blog blog = await _context.Blogs
                    .Include(b => b.BlogImages)
                    .Include(b => b.BlogTags)
                    .ThenInclude(bt => bt.Tag)
                    .Where(b => b.Id == id)
                    .FirstOrDefaultAsync();
                if (blog is null) { return View("Error"); }
                ViewBag.Categories = await _context.Categories.ToListAsync();

                var tags = await _context.Tags.ToListAsync();
                ViewBag.Tags = tags;
                List<int> tagIds = new();
                foreach (var tag in tags)
                    foreach (var item in blog.BlogTags)
                        if (tag.Id == item.TagId)
                            tagIds.Add(tag.Id);

                UpdateBlogVM updateBlogVM = new UpdateBlogVM()
                {
                    Id = id,
                    Title = blog.Title,
                    Description = blog.Description,
                    ImgUrl = blog.ImgUrl,
                    BlogTags = blog.BlogTags,
                    TagIds = tagIds,
                    BlogImagesVm = new List<BlogImageVm>()
                };

                foreach (var item in blog.BlogImages)
                {
                    BlogImageVm blogImageVm = new BlogImageVm()
                    {
                        Id = item.Id,
                        ImgUrl = item.ImgUrl,
                        IsPrime = item.IsPrime
                    };
                    updateBlogVM.BlogImagesVm.Add(blogImageVm);
                }

                return View(updateBlogVM);
            }
            [HttpPost]
            public async Task<IActionResult> Update(UpdateBlogVM updateBlogVM)
            {
                ViewBag.Categories = await _context.Categories.ToListAsync();
                ViewBag.Tags = await _context.Tags.ToListAsync();

                Blog existBlog = await _context.Blogs
                    .Include(b => b.BlogImages)
                    .Include(b => b.BlogTags)
                    .ThenInclude(bt => bt.Tag)
                    .Where(b => b.Id == updateBlogVM.Id)
                    .FirstOrDefaultAsync();
                if (existBlog is null) { return View("Error"); }
                if (!ModelState.IsValid) { return View(); }


                foreach (var item in updateBlogVM.TagIds)
                {
                    bool resultTag = await _context.Tags.AnyAsync(t => t.Id == item);
                    if (!resultTag)
                    {
                        ModelState.AddModelError("TagIds", "There is no such tag like that");
                        return View();
                    }
                }
                List<int> newSelectTagId = updateBlogVM.TagIds.Where(tagId => !existBlog.BlogTags.Exists(b => b.TagId == tagId)).ToList();
                existBlog.ImgUrl = updateBlogVM.ImgUrl;
                existBlog.Title = updateBlogVM.Title;
                existBlog.Description = updateBlogVM.Description;
                existBlog.ImgUrl = updateBlogVM.ImgUrl;

                foreach (var newTagId in newSelectTagId)
                {
                    BlogTag blogTag = new BlogTag()
                    {
                        TagId = newTagId,
                        BlogId = updateBlogVM.Id,
                    };
                    _context.BlogTags.Add(blogTag);
                }
                List<BlogTag> removedTag = existBlog.BlogTags.Where(bt => !updateBlogVM.TagIds.Contains(bt.TagId)).ToList();
                _context.BlogTags.RemoveRange(removedTag);

                if (updateBlogVM.MainPhoto is not null)
                {
                    if (!updateBlogVM.MainPhoto.CheckContent("image/"))
                    {
                        ModelState.AddModelError("MainPhoto", "Please enter correct format");
                        return View();
                    }
                    BlogImages existMainPhoto = existBlog.BlogImages.FirstOrDefault(p => p.IsPrime == true);
                    existMainPhoto.ImgUrl.DeleteFile(_environment.WebRootPath, @"\Upload\BlogImages\");
                    existBlog.BlogImages.Remove(existMainPhoto);
                    BlogImages blogImage = new BlogImages()
                    {
                        ImgUrl = updateBlogVM.MainPhoto.Upload(_environment.WebRootPath, @"\Upload\BlogImages\"),
                        BlogId = existBlog.Id,
                        IsPrime = true
                    };
                    existBlog.BlogImages.Add(blogImage);
                }
                if (updateBlogVM.HoverPhoto is not null)
                {
                    if (!updateBlogVM.HoverPhoto.CheckContent("image/"))
                    {
                        ModelState.AddModelError("HoverPhoto", "Please enter correct format");
                        return View();
                    }
                    BlogImages existHoverPhoto = existBlog.BlogImages.FirstOrDefault(p => p.IsPrime == false);
                    existHoverPhoto.ImgUrl.DeleteFile(_environment.WebRootPath, @"\Upload\BlogImages\");
                    existBlog.BlogImages.Remove(existHoverPhoto);
                    BlogImages blogImage = new BlogImages()
                    {
                        ImgUrl = updateBlogVM.HoverPhoto.Upload(_environment.WebRootPath, @"\Upload\BlogImages\"),
                        BlogId = existBlog.Id,
                        IsPrime = false
                    };
                    existBlog.BlogImages.Add(blogImage);
                }

                updateBlogVM.ImageIds = new();
                // if ImageIds == null => oldBook.BookImages.RemoveAll(x => x.isPrime == null)
                // else =>

                List<BlogImages> removeList = existBlog.BlogImages.Where(b => !updateBlogVM.ImageIds.Exists(imgId => imgId == b.Id) && b.IsPrime == null).ToList();
                if (removeList.Count > 0)
                {
                    foreach (var item in removeList)
                    {
                        existBlog.BlogImages.Remove(item);
                        item.ImgUrl.DeleteFile(_environment.WebRootPath, @"\Upload\BlogImages\");
                    }
                }

                TempData["Error"] = "";
                if (updateBlogVM.Photos is not null)
                {
                    foreach (IFormFile addImage in updateBlogVM.Photos)
                    {
                        if (!addImage.CheckContent("image/"))
                        {
                            TempData["Error"] += $"{addImage.FileName} is not Image File";
                            continue;
                        }
                        BlogImages blogImages = new BlogImages()
                        {
                            IsPrime = null,
                            ImgUrl = addImage.Upload(_environment.WebRootPath, "/Upload/BlogImages/"),
                            BlogId = existBlog.Id
                        };
                        existBlog.BlogImages.Add(blogImages);
                    }
                }

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            #endregion
            #region Delete
            public IActionResult Delete(int id)
            {
                Blog blog = _context.Blogs.Include(b => b.BlogImages).FirstOrDefault(b => b.Id == id);
                if (blog is null)
                {
                    return View("Error");
                }
                foreach (var item in blog.BlogImages)
                {
                    item.ImgUrl.DeleteFile(_environment.WebRootPath, "/Upload/BlogImages/");
                }

                _context.Blogs.Remove(blog);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            #endregion
        }
    }