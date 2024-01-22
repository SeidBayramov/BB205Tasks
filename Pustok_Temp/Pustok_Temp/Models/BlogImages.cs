namespace Pustok_Temp.Models
{
    public class BlogImages : BaseEntity
    {
        public bool? IsPrime { get; set; }
        public string ImgUrl { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
