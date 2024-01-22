namespace Pustok_Temp.Models
{

    public class Tag : BaseEntity
    {
        public string Name { get; set; }
        public List<BookTag> Books { get; set; }
        public List<BlogTag> Blogs { get; set; }
    }
}
