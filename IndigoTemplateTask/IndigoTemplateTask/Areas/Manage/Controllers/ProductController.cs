using IndigoTemplateTask.Areas.Manage.VIewModels;
using IndigoTemplateTask.DAL;
using IndigoTemplateTask.Helpers;
using IndigoTemplateTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static IndigoTemplateTask.Areas.Manage.VIewModels.UpdateProductVm;

namespace IndigoTemplateTask.Areas.Manage.Controllers
{

    [Area("Manage")]

    public class ProductController : Controller
    {
        AppDbContext _context { get; set; }

        IWebHostEnvironment _env { get; set; }

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {

            List<Product> products = await _context.Products
                .Where(p => p.IsDeleted == false)
                .Include(p => p.ProductImages)
                .ToListAsync();

            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductVm createProductVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            Product product = new Product()
            {
                Title = createProductVm.TItle,
                Description = createProductVm.Description,
                ProductImages = new List<ProductImage>()
            };


            if (!createProductVm.Image.CheckContent("image/"))
            {
                ModelState.AddModelError("Image", "Please check your image type");
                return View();
            }

            if (!createProductVm.Image.CheckLenght(3000000))
            {
                ModelState.AddModelError("Image", "3mb dan boyukdur ");
                return View();
            }



            ProductImage image = new ProductImage()
            {
                IsImage = true,
                ImgUrl = createProductVm.Image.Upload(_env.WebRootPath, @"\Upload\Product\"),
                Product = product,
            };
            product.ProductImages.Add(image);

            await _context.Products.AddAsync(product);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id <= 0)
            {
                return View();
            }
            Product product = await _context.Products.Where(p => p.IsDeleted == false).Include(p => p.ProductImages).Where(c => c.Id == id).FirstOrDefaultAsync();

            if (product == null) { return View("Eror"); }


            UpdateProductVm updateProductVm = new UpdateProductVm()
            {
                Id = product.Id,
                TItle = product.Title,
                Description = product.Description,
                ProductImages = new List<ProductImagesVm>()
            };


            foreach (ProductImage item in product.ProductImages)
            {
                ProductImagesVm productImageVm = new ProductImagesVm()
                {
                    Id = item.Id,
                    IsPrime = item.IsImage,
                    ImgUrl = item.ImgUrl
                };
                updateProductVm.ProductImages.Add(productImageVm);
            }

            return View(updateProductVm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateProductVm updateProductVm)
        {
            Product existProduct = await _context.Products.Where(p => p.IsDeleted == false)
                .Include(p => p.ProductImages)
                .Where(c => c.Id == updateProductVm.Id).FirstOrDefaultAsync();


            if (existProduct == null)
            {
                return View();
            }

            if (!ModelState.IsValid) { return View(); }

            existProduct.Title = updateProductVm.TItle;
            existProduct.Description = updateProductVm.Description;


            if (updateProductVm.Image != null)
            {
                if (!updateProductVm.Image.CheckContent("image/"))
                {
                    ModelState.AddModelError("MainPhoto", "Please enter correct format");
                    return View();
                }

                ProductImage existMainPhoto = existProduct.ProductImages.FirstOrDefault(p => p.IsImage == true);


                existMainPhoto.ImgUrl.DeleteFile(_env.WebRootPath, @"\Upload\Product\");

                existProduct.ProductImages.Remove(existMainPhoto);

                ProductImage productImage = new ProductImage()
                {
                    ImgUrl = updateProductVm.Image.Upload(_env.WebRootPath, @"\Upload\Product\"),
                    ProductId = existProduct.Id,
                    IsImage = true
                };
                existProduct.ProductImages.Add(productImage);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

                return View();
            }
            else
            {
                return View();
            }


        }
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.Where(p => p.IsDeleted == false).FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return View();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}

