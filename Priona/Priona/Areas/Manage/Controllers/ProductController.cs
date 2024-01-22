using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Priona.Business.Helpers;
using Priona.Business.ViewModels.Product;
using Priona.Core.Entity;
using Priona.DAL.Context;

namespace Priona.Areas.Manage.Controllers
{

    [Area("Manage")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        AppDbContext _context { get; set; }

        IWebHostEnvironment _env { get; set; }


        public async Task<IActionResult> Index()
        {
            List<Product> product = await _context.Products.Where(p => p.IsDeleted == false).Include(product => product.Category)
                .Include(product => product.ProductTags)
                .ThenInclude(product => product.Tag)
                .Include(p => p.ProductImages)
                .ToListAsync();

            return View(product);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Tags = await _context.Tags.ToListAsync();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVm createProductVm)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Tags = await _context.Tags.ToListAsync();

            if (!ModelState.IsValid)
            {
                return View("Eror");
            }
            bool resultCategory = await _context.Categories.AnyAsync(c => c.Id == createProductVm.CategoryId);

            if (!resultCategory)
            {
                ModelState.AddModelError("CategoryId", "Bele bir category ID yoxdu");
            }

            Product product = new Product()
            {
                Name = createProductVm.Name,
                Price = createProductVm.Price,
                Description = createProductVm.Description,
                SKU = createProductVm.SKU,
                CategoryId = (int)createProductVm.CategoryId,
                ProductImages = new List<ProductImages>()

            };
            if (createProductVm.TagIds != null)
            {


                foreach (int TagId in createProductVm.TagIds)
                {

                    bool resultTag = await _context.Tags.AnyAsync(c => c.Id == TagId);

                    if (!resultTag)
                    {
                        ModelState.AddModelError("TagIds", $"{TagId}  Tag ID yoxdu");

                        return View();
                    }


                    ProductTag productTag = new ProductTag()
                    {
                        Product = product,
                        TagId = TagId,
                    };

                    _context.ProductTags.Add(productTag);


                }
            }
            if (!createProductVm.MainPhoto.CheckContent("image/"))
            {
                ModelState.AddModelError("MainPhoto", "Duzgun formatda sekil daxil edin:");
                return View();
            }
            if (!createProductVm.MainPhoto.CheckLenght(3000000))
            {
                ModelState.AddModelError("MainPhoto", "3mb dan boyukdur ");
                return View();
            }
            if (!createProductVm.HoverPhoto.CheckContent("image/"))
            {
                ModelState.AddModelError("HoverPhoto", "Duzgun formatda sekil daxil edin:");
                return View();
            }
            if (!createProductVm.HoverPhoto.CheckLenght(3000000))
            {
                ModelState.AddModelError("HoverPhoto", "3mb dan boyukdur ");
                return View();
            }

            ProductImages mainImages = new ProductImages()
            {
                IsPrime = true,
                ImgUrl = createProductVm.MainPhoto.Upload(_env.WebRootPath, @"\Upload\Product\"),
                Product = product
            };

            ProductImages hoverImages = new ProductImages()
            {
                IsPrime = false,
                ImgUrl = createProductVm.HoverPhoto.Upload(_env.WebRootPath, @"\Upload\Product\"),
                Product = product
            };
            product.ProductImages.Add(hoverImages);
            product.ProductImages.Add(mainImages);

            TempData["Error"] = "";

            if (createProductVm.Photos != null)
            {
                foreach (var phto in createProductVm.Photos)
                {


                    if (!phto.CheckContent("image/"))
                    {
                        TempData["Error"] += $"{phto.FileName} type duzgun deyil \t";
                        continue;
                    }
                    if (!phto.CheckLenght(3000000))
                    {
                        TempData["Error"] += $"{phto.FileName} 3mb dan yuksek deyil \t";
                        continue;
                    }

                    ProductImages newPhoto = new ProductImages()
                    {
                        IsPrime = null,
                        ImgUrl = phto.Upload(_env.WebRootPath, @"\Upload\Product\"),
                        Product = product
                    };
                    product.ProductImages.Add(newPhoto);
                }

                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            else
            {
                return View();
            }


        }
        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id <= 0)
            {
                return View("Eror");
            }
            Product existProduct = await _context.Products.Where(p => p.IsDeleted == false)
                .Include(p => p.Category)
                .Include(p => p.ProductTags)
                .ThenInclude(pt => pt.Tag)
                .Include(p => p.ProductImages)
                .Where(c => c.Id == id).FirstOrDefaultAsync();

            if (existProduct == null) { return View("Eror"); }
            UpdateProductVm updateProductVm = new UpdateProductVm()
            {
                Id = existProduct.Id,
                Name = existProduct.Name,
                SKU = existProduct.SKU,
                Description = existProduct.Description,
                Price = existProduct.Price,
                CategoryId = existProduct.CategoryId,
                TagIds = existProduct.ProductTags.Select(p => p.TagId).ToList(),
                ProductImages = new List<ProductImagesVm>()
            };


            foreach (ProductImages item in existProduct.ProductImages)
            {
                ProductImagesVm productImageVm = new ProductImagesVm()
                {
                    Id = item.Id,
                    IsPrime = item.IsPrime,
                    ImgUrl = item.ImgUrl
                };
                updateProductVm.ProductImages.Add(productImageVm);
            }
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Tags = await _context.Tags.ToListAsync();

            return View(updateProductVm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductVm updateProductVm)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Tags = await _context.Tags.ToListAsync();

            Product existProduct = await _context.Products.Where(p => p.IsDeleted == false)
                .Include(p => p.Category)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductTags)
                .ThenInclude(pt => pt.Tag)
                .Where(c => c.Id == updateProductVm.Id).FirstOrDefaultAsync();

            if (existProduct == null)
            {
                return View("Eror");
            }

            if (!ModelState.IsValid) { return View(); }

            TempData["Error"] = "";
            bool resultCategory = await _context.Categories.AnyAsync(c => c.Id == updateProductVm.CategoryId);
            if (!resultCategory)
            {
                ModelState.AddModelError("CategoryId", "There is no such category");
                return View();
            }
            foreach (int item in updateProductVm.TagIds)
            {
                bool resultTag = await _context.Tags.AnyAsync(c => c.Id == item);
                if (!resultTag)
                {
                    ModelState.AddModelError("TagIds", "There is no such tag");
                    return View();
                }
            }
            List<int> newSelectTagId = updateProductVm.TagIds.Where(tagId => !existProduct.ProductTags.Exists(p => p.TagId == tagId)).ToList();
            existProduct.SKU = updateProductVm.SKU;
            existProduct.Price = updateProductVm.Price;
            existProduct.Description = updateProductVm.Description;
            existProduct.Name = updateProductVm.Name;
            existProduct.CategoryId = updateProductVm.CategoryId;
            foreach (int newTagId in newSelectTagId)
            {
                ProductTag productTag = new ProductTag()
                {
                    TagId = newTagId,
                    ProductId = updateProductVm.Id,
                };
                _context.ProductTags.Add(productTag);
            }
            List<ProductTag> removedTag = existProduct.ProductTags.Where(pt => !updateProductVm.TagIds.Contains(pt.TagId)).ToList();
            _context.ProductTags.RemoveRange(removedTag);
            if (updateProductVm.MainPhoto != null)
            {
                if (!updateProductVm.MainPhoto.CheckContent("image/"))
                {
                    ModelState.AddModelError("MainPhoto", "Please enter correct format");
                    return View();
                }
                ProductImages existMainPhoto = existProduct.ProductImages.FirstOrDefault(p => p.IsPrime == true);
                existMainPhoto.ImgUrl.DeleteFile(_env.WebRootPath, @"\Upload\Product\");
                existProduct.ProductImages.Remove(existMainPhoto);
                ProductImages productImage = new ProductImages()
                {
                    ImgUrl = updateProductVm.MainPhoto.Upload(_env.WebRootPath, @"\Upload\Product\"),
                    ProductId = existProduct.Id,
                    IsPrime = true
                };
                existProduct.ProductImages.Add(productImage);
            }
            if (updateProductVm.HoverPhoto != null)
            {
                if (!updateProductVm.HoverPhoto.CheckContent("image/"))
                {
                    ModelState.AddModelError("HoverPhoto", "Duzgun format daxil edin");
                    return View();
                }
                var existMainPhoto = existProduct.ProductImages.FirstOrDefault(p => p.IsPrime == false);
                existMainPhoto.ImgUrl.DeleteFile(_env.WebRootPath, @"/Upload/Product/");
                existProduct.ProductImages.Remove(existMainPhoto);
                ProductImages productImage = new ProductImages()
                {
                    ImgUrl = updateProductVm.MainPhoto.Upload(_env.WebRootPath, @"\Upload\Product\"),
                    ProductId = existProduct.Id,
                    IsPrime = false
                };
                existProduct.ProductImages.Add(productImage);
            }

            if (updateProductVm.ImageIds == null) { existProduct.ProductImages.RemoveAll(x => x.IsPrime == null); }
            else
            {
                List<ProductImages> removeList = existProduct.ProductImages.Where(p => !updateProductVm.ImageIds.Contains(p.Id) && p.IsPrime == null).ToList();
                if (removeList.Count > 0)
                {
                    foreach (var item in removeList)
                    {
                        existProduct.ProductImages.Remove(item);
                        item.ImgUrl.DeleteFile(_env.WebRootPath, @"\Upload\Product\");
                    }
                }
            }
            if (updateProductVm.Photos != null)
            {
                foreach (IFormFile imgFile in updateProductVm.Photos)
                {
                    if (!imgFile.CheckContent("image/"))
                    {
                        TempData["Error"] += $"{imgFile.FileName} is not in correct format ";
                        continue;
                    }
                    ProductImages productImage = new ProductImages()
                    {
                        IsPrime = null,
                        ProductId = existProduct.Id,
                        ImgUrl = imgFile.Upload(_env.WebRootPath, @"\Upload\Product\")
                    };
                    existProduct.ProductImages.Add(productImage);

                }
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.Where(p => p.IsDeleted == false).FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return View("Eror");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
