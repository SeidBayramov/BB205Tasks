using Pustok_Temp.Models;

namespace Pustok_Temp.ViewModels
{
    public class HomeVM
    {
        public List<Book> books { get; set; }
        public List<Category> categories { get; set; }
        public List<Blog> blogs { get; set; }
    }
}
