using Microsoft.AspNetCore.Mvc;
using WebAppl.Models;

namespace WebAppl.Controllers
{
    public class GroupController : Controller
    {

        public IActionResult Index()
        {
            List<Group> groups = new List<Group>();
            Group group = new Group()
            {
                Id = 1,
                Name = "BB205"
            };

            Group group1 = new Group()
            {
                Id = 2,
                Name = "605.21"
            };


            groups.Add(group);
            groups.Add(group1);
            

            return View();
        }
    }
}
