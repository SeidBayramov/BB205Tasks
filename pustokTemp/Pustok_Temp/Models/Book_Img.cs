namespace Pustok_Temp.Models
{
    public class Book_Img
    {
        public int Id { get; set; }
        public string? ImgUrl { get; set; }
        public int? BookId { get; set; }
        public Book? Books { get; set; }
    }
}
