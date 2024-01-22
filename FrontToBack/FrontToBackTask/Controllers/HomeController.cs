using FrontToBackTask.Models;
using FrontToBackTask.ViewsModel;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.Design;
using System.Xml.Linq;

namespace FrontToBackTask.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            List<Men> menlist = new List<Men>();
            menlist.Add(new Men()
            {
                Id = 1,
                Name = "Men Coat",
                imgUrl = "coat.jpg",
                Price = 90


            });
            menlist.Add(new Men()
            {
                Id = 2,
                Name = "Men Trousers",
                imgUrl = "trousersmen.jpg",
                Price = 78

            });

            menlist.Add(new Men()
            {
                Id = 3,
                Name = "Men  Shoes",
                imgUrl = "jordan.jpg",
                Price = 250

            });

            List<Women> womenlist = new List<Women>();
            womenlist.Add(new Women()
            {
                Id = 1,
                Name = "Women Coat",
                imgUrl = "coatwm.jpg",
                Price = 80

            });
            womenlist.Add(new Women()
            { 
               Id = 2,
                Name = " Women T-Shirt",
                imgUrl = "blouse.jpg",
                Price = 120

            });
            womenlist.Add(new Women()
            {
                Id = 3,
                Name = "Women Shoes",
                imgUrl = "kablok.jpg",
                Price = 100

            });


            List <Kids> kidslist = new List<Kids>();
            kidslist.Add(new Kids()
            {
                Id = 1,
                Name = "Kids Mont",
                imgUrl = "kidkurtk.jpg",
                Price = 90
            });
            kidslist.Add(new Kids()
            {
                Id = 2,
                Name = "Kids Trousers",
                imgUrl = "kidstr.jpg",
                Price = 35
            });
            kidslist.Add(new Kids()
            {
                Id = 3,
                Name = " Kids Shoes",
                imgUrl = "kidsshoes.jpg",
                Price = 50
            });


            HomeWM homeWM = new HomeWM();
            homeWM.MensProduct = menlist;
            homeWM.WomenProduct = womenlist;
            homeWM.KidsProduct = kidslist;






            return View(homeWM);
        }





    }
}
