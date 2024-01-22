namespace Pustok_Temp.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public List<Book>? Books { get; set; }
    }
}
