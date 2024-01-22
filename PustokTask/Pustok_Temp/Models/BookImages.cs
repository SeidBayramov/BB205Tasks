namespace Pustok_Temp.Models
{
    public class BookImages : BaseEntity
    {
        public bool? IsPrime { get; set; }
        public string ImgUrl { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
    }
}