using Microsoft.AspNetCore.Mvc;
using WebAppl.Models;

namespace WebAppl.Controllers
{
    public class TeacherController : Controller
    {


        public IActionResult Index()
        {
            List<Teacher> teachers = new List<Teacher>();
            Teacher teacher = new Teacher()
            {
                Id = 1,
                Name = "Narmin",
            };

            Teacher teacher1 = new Teacher()
            {
                Id = 2,
                Name = "Resul"
            };

            teachers.Add(teacher);
            teachers.Add (teacher1);

            return View();
        }
    }
}
