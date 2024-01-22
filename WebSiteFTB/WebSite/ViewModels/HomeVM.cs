using WebSite.Models;

namespace WebSite.ViewModels
{
    namespace StoriesApp.ViewModels
    {
        public class HomeVM
        {
            public List<Category> _categories { get; set; }
            public List<Recipes> _recipes { get; set; }
            public List<Stories> _stories { get; set; }
        }
    }
}
