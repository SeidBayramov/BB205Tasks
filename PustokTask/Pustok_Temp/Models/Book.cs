using Azure;

namespace Pustok_Temp.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string BookCode { get; set; }
        public double Price { get; set; }
        public string? ImgUrl { get; set; }
        public int CategoryId { get; set; }

        public bool IsDeleted { get; set; }

        public Category? Category { get; set; }
        public List<Tag>? Tag { get; set; }
        public List<BookTag>? BookTags { get; set; }
        public List<BookImages>? BookImages { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}
