using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Pustok_Temp.Models
{
    public class Book
    {
        public int Id { get; set; }

        public string? Title { get; set; }


        [StringLength(maximumLength: 100)]
        public string? ImgUrl { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
        public double? Price { get; set; }
        public int? AuthorId { get; set; }
        public Author? Authors { get; set; }
        public List<Book_Img>? Bookimages { get; set; }

    }
}
