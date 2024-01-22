using FinalProcetStudent.Areas.Admin.ViewModel;
using FinalProcetStudent.DAL;
using FinalProcetStudent.Helpers;
using FinalProcetStudent.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static FinalProcetStudent.Areas.Admin.ViewModel.UpdateStudentVm;

namespace FinalProcetStudent.Areas.Admin.Controllers
{
    [Area("Admin")]

    public class StudentController : Controller
    {
        AppDbContext _context { get; set; }

        IWebHostEnvironment _env { get; set; }

        public StudentController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {

            List<Student> products = await _context.Students
                .Include(p => p.StudentImages)
                .ToListAsync();

            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentVm createProductVm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            Student product = new Student()
            {
                Name = createProductVm.Name,
                Surname = createProductVm.Surname,
                Point = createProductVm.Point,
                Age = createProductVm.Age,
                UserName = createProductVm.UserName,
                BornTime=createProductVm.BornTime,
                StudentImages = new List<StudentImage>()
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



            StudentImage image = new StudentImage()
            {
                IsPrime = true,
                ImgUrl = createProductVm.Image.Upload(_env.WebRootPath, @"\Upload\Student\"),
                Student = product,
            };
            product.StudentImages.Add(image);

            await _context.Students.AddAsync(product);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id <= 0)
            {
                return View();
            }
            Student product = await _context.Students.Include(p => p.StudentImages).Where(c => c.Id == id).FirstOrDefaultAsync();

            if (product == null) { return View("Eror"); }


            UpdateStudentVm updateProductVm = new UpdateStudentVm()
            {
                Id = product.Id,
                Name = product.Name,
                Surname = product.Surname,
                Age = product.Age,
                BornTime = product.BornTime,
                UserName = product.UserName,
                Point=product.Point,
                StudentsImagesVms = new List<StudentsImagesVm>()

            };


            foreach (StudentImage item in product.StudentImages)
            {
                StudentsImagesVm productImageVm = new StudentsImagesVm()
                {
                    Id = item.Id,
                    IsPrime = item.IsPrime,
                    ImgUrl = item.ImgUrl
                };
                updateProductVm.StudentsImagesVms.Add(productImageVm);
            }

            return View(updateProductVm);
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateStudentVm updateProductVm)
        {
            Student existProduct = await _context.Students
                .Include(p => p.StudentImages)
                .Where(c => c.Id == updateProductVm.Id).FirstOrDefaultAsync();


            if (existProduct == null)
            {
                return View();
            }

            if (!ModelState.IsValid) { return View(); }

            existProduct.Name = updateProductVm.Name;
            existProduct.Surname = updateProductVm.Surname;
            existProduct.UserName = updateProductVm.UserName;
            existProduct.Age = updateProductVm.Age;
            existProduct.Point=updateProductVm.Point;
            existProduct.BornTime = updateProductVm.BornTime;


            if (updateProductVm.Image != null)
            {
                if (!updateProductVm.Image.CheckContent("image/"))
                {
                    ModelState.AddModelError("MainPhoto", "Please enter correct format");
                    return View();
                }

                StudentImage existMainPhoto = existProduct.StudentImages.FirstOrDefault(p => p.IsPrime == true);


                existMainPhoto.ImgUrl.DeleteFile(_env.WebRootPath, @"\Upload\Student\");

                existProduct.StudentImages.Remove(existMainPhoto);

                StudentImage productImage = new StudentImage()
                {
                    ImgUrl = updateProductVm.Image.Upload(_env.WebRootPath, @"\Upload\Student\"),
                    StudentId = existProduct.Id,
                    IsPrime = true
                };
                existProduct.StudentImages.Add(productImage);

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
            var product = await _context.Students.FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return View();
            }

            _context.Students.Remove(product);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
