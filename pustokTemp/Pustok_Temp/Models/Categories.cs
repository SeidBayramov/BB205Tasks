namespace Pustok_Temp.Models
{
    public class Categories
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? ParentCategoryId { get; set; }
        public ParentCategory? ParentCategory { get; set; }
    }
}
