using Microsoft.AspNetCore.Mvc;
using WebAppl.Models;

namespace WebAppl.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            List<Student> students = new List<Student>();
            Student student = new Student()
            {
                Id = 1,
                Name = "Seid",
                Surname = "Bayramov",
                ImagePath = "asjdajdkjdsui",
            };
            Student student2 = new Student()
            {
                Id = 2,
                Name = "Ferid",
                Surname = "Alizade",
                ImagePath = "FIFTYCENT"
                
            };

            students.Add(student);
            students.Add(student2);
            
            return View();
        }
    }
}
