namespace Pustok_Temp.Models
{
    public class Blog : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        public List<BlogTag>? BlogTags { get; set; }
        public Tag? Tag { get; set; }
        public List<BlogImages>? BlogImages { get; set; }

    }
}
