using Pustok_Temp.Models;

namespace Pustok_Temp.ViewModels
{
    public class HomeVM
    {
        public List<Author> authors { get; set; }
        public List<Book> books { get; set; }
        public List<Book_Img> books_img { get; set;}
        public List<Categories> categories { get; set; }
        public List<Slider> sliders {  get; set; }

        public List<ParentCategory> parentcats { get; set; }

    }
}
