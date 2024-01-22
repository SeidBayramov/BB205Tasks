using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiniaSite.Models
{
    public class Sliders
    {
        public int Id { get; set; }

        [Required,StringLength(25,ErrorMessage ="The Lenghest must be 10 words")]

        public string Title { get; set; }

        public string  SubTitle { get; set; }

        public string Description { get; set; }

        [StringLength(maximumLength: 100)]
        public string? ImgUrl { get; set; }

        [NotMapped]

        public IFormFile ImageFile  { get; set; }
    }
}
