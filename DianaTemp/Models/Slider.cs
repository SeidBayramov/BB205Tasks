
namespace DianaTemp.Models
{
	public class Slider : BaseEntity
	{

		[Required, StringLength(25, ErrorMessage = "The Lenghest must be 10 words")]

		public string? Title { get; set; }

		public string? SubTitle { get; set; }

		public string? Description { get; set; }

		[StringLength(maximumLength: 100)]
		public string? ImgUrl { get; set; }

		[NotMapped]

		public IFormFile? ImageFile { get; set; }
	}
}