using Pustok_Temp.Models;

namespace Pustok_Temp.Areas.Manage.ViewModel
{
    public class CreateCategoryVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? ParentCategoryId { get; set; }
        public ICollection<Category>? categories { get; set; }
        public List<Book>? Books { get; set; }
    }
}
