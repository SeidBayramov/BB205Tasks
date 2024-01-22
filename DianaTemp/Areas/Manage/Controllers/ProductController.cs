using DianaTemp.Areas.ViewModels;
using System.Data;

namespace DianaTemp.Areas.Manage.Controllers
{
    [Area("Manage")]
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
            List<Product> products = await _context.Products
                .Include(p=>p.Category)
                .Include(p => p.ProductImages)
                .Include(p => p.ProductColours)
                .ThenInclude(pc => pc.Colour)
                .Include(p => p.ProductSizes)
                .ThenInclude(ps => ps.Size)
                .Include(p => p.ProductMaterial)
                .ThenInclude(pm => pm.Material).ToListAsync();
            return View(products);
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Size = await _context.Sizes.ToListAsync();
            ViewBag.Colour = await _context.Colours.ToListAsync();
            ViewBag.Material = await _context.Materials.ToListAsync();

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVm createProductVm)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Size = await _context.Sizes.ToListAsync();
            ViewBag.Colour = await _context.Colours.ToListAsync();
            ViewBag.Material = await _context.Materials.ToListAsync();

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
                CategoryId = (int)createProductVm.CategoryId,
                ProductImages = new List<ProductImages>()

            };
            if (createProductVm.ColorIds != null)
            {

                foreach (int colorId in createProductVm.ColorIds)
                {

                    bool resultTag = await _context.Colours.AnyAsync(c => c.Id == colorId);

                    if (!resultTag)
                    {
                        ModelState.AddModelError("TagIds", $"{colorId}  Tag ID yoxdu");

                        return View();
                    }


                    ProductColour productcolor = new ProductColour()
                    {
                        Product = product,
                        ColourId = colorId,
                    };

                    _context.ProductColours.Add(productcolor);


                }
            }
            if (createProductVm.SizeIds != null)
            {

                foreach (int sizeId in createProductVm.SizeIds)
                {

                    bool resultTag = await _context.Colours.AnyAsync(c => c.Id == sizeId);

                    if (!resultTag)
                    {
                        ModelState.AddModelError("TagIds", $"{sizeId}  Tag ID yoxdu");

                        return View();
                    }


                    ProductSize productsize = new ProductSize()
                    {
                        Product = product,
                        SizeId = sizeId,
                    };

                    _context.ProductSizes.Add(productsize);


                }
            }
            if (createProductVm.MaterialsIds != null)
            {

                foreach (int MaterialID in createProductVm.MaterialsIds)
                {

                    bool resultTag = await _context.Colours.AnyAsync(c => c.Id == MaterialID);

                    if (!resultTag)
                    {
                        ModelState.AddModelError("TagIds", $"{MaterialID}  Tag ID yoxdu");

                        return View();
                    }


                    ProductMaterial productmaterial = new ProductMaterial()
                    {
                        Product = product,
                        MaterialId = MaterialID,
                    };

                    _context.ProductMaterials.Add(productmaterial);


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


            ProductImages mainImages = new ProductImages()
            {
                IsPrime = true,
                ImgUrl = createProductVm.MainPhoto.Upload(_env.WebRootPath, @"\Upload\Product\"),
                Product = product
            };

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
                           .Include(p => p.ProductImages)
                           .Include(p => p.ProductColours)
                           .ThenInclude(pt => pt.Colour)
                           .Include(p => p.ProductMaterial)
                           .ThenInclude(pt => pt.Material)
                           .Include(p => p.ProductSizes)
                           .ThenInclude(pt => pt.Size).FirstOrDefaultAsync(p=>p.Id==id);

            if (existProduct == null) { return View("Eror"); }
            UpdateProductVm updateProductVm = new UpdateProductVm()
            {
                Id = existProduct.Id,
                Name = existProduct.Name,
                Description = existProduct.Description,
                Price = existProduct.Price,
                CategoryId = existProduct.CategoryId,
                SizeIds = existProduct.ProductSizes.Select(p => p.SizeId).ToList(),
                ColorIds = existProduct.ProductColours.Select(p => p.ColourId).ToList(),
                MaterialsIds = existProduct.ProductMaterial.Select(p => p.MaterialId).ToList(),
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
            ViewBag.Size = await _context.Sizes.ToListAsync();
            ViewBag.Colour = await _context.Colours.ToListAsync();
            ViewBag.Material = await _context.Materials.ToListAsync();

            return View(updateProductVm);
        }
        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductVm updateProductVm)
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Size = await _context.Sizes.ToListAsync();
            ViewBag.Colour = await _context.Colours.ToListAsync();
            ViewBag.Material = await _context.Materials.ToListAsync();

            Product existProduct = await _context.Products.Where(p => p.IsDeleted == false)
                            .Include(p => p.Category)
                            .Include(p => p.ProductImages)
                           .Include(p => p.ProductColours)
                           .ThenInclude(pt => pt.Colour)
                           .Include(p => p.ProductMaterial)
                           .ThenInclude(pt => pt.Material)
                           .Include(p => p.ProductSizes)
                           .ThenInclude(pt => pt.Size)
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
            foreach (int item in updateProductVm.SizeIds)
            {
                bool resultTag = await _context.Sizes.AnyAsync(c => c.Id == item);
                if (!resultTag)
                {
                    ModelState.AddModelError("SizeIds", "There is no such size");
                    return View();
                }
            }
            foreach (int item in updateProductVm.ColorIds)
            {
                bool resultTag = await _context.Colours.AnyAsync(c => c.Id == item);
                if (!resultTag)
                {
                    ModelState.AddModelError("ColorIds", "There is no such Color");
                    return View();
                }
            }

            foreach (int item in updateProductVm.MaterialsIds)
            {
                bool resultTag = await _context.Materials.AnyAsync(c => c.Id == item);
                if (!resultTag)
                {
                    ModelState.AddModelError("MaterialsIds", "There is no such Materials");
                    return View();
                }
            }
            List<int> newSelectSizeId = updateProductVm.SizeIds.Where(tagId => !existProduct.ProductSizes.Exists(p => p.SizeId == tagId)).ToList();
            List<int> newSelectMaterialId = updateProductVm.MaterialsIds.Where(tagId => !existProduct.ProductMaterial.Exists(p => p.MaterialId == tagId)).ToList();
            List<int> newSelectColourId = updateProductVm.ColorIds.Where(tagId => !existProduct.ProductColours.Exists(p => p.ColourId == tagId)).ToList();
           
            
            existProduct.Price = updateProductVm.Price;
            existProduct.Description = updateProductVm.Description;
            existProduct.Name = updateProductVm.Name;
            existProduct.CategoryId = updateProductVm.CategoryId;
            foreach (int newSizeId in newSelectSizeId)
            {
                ProductSize productSize = new ProductSize()
                {
                    SizeId = newSizeId,
                    ProductId = updateProductVm.Id,
                };
                _context.ProductSizes.Add(productSize);
            }
            foreach (int newMatId in newSelectMaterialId)
            {
                ProductMaterial productMat = new ProductMaterial()
                {
                    MaterialId = newMatId,
                    ProductId = updateProductVm.Id,
                };
                _context.ProductMaterials.Add(productMat);
            }
            foreach (int newColorId in newSelectColourId)
            {
                ProductColour productColor = new ProductColour()
                {
                    ColourId = newColorId,
                    ProductId = updateProductVm.Id,
                };
                _context.ProductColours.Add(productColor);
            }
            List<ProductSize> removedSize = existProduct.ProductSizes.Where(pt => !updateProductVm.SizeIds.Contains(pt.SizeId)).ToList();
            List<ProductMaterial> removedMaterial = existProduct.ProductMaterial.Where(pt => !updateProductVm.MaterialsIds.Contains(pt.MaterialId)).ToList();
            List<ProductColour> removedColour = existProduct.ProductColours.Where(pt => !updateProductVm.ColorIds.Contains(pt.ColourId)).ToList();
            _context.ProductSizes.RemoveRange(removedSize);
            _context.ProductMaterials.RemoveRange(removedMaterial);
            _context.ProductColours.RemoveRange(removedColour);
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