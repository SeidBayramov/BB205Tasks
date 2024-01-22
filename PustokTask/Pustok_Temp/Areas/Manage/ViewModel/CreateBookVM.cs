namespace Pustok_Temp.Areas.Manage.ViewModel
{
    public class CreateBookVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string BookCode { get; set; }
        public double Price { get; set; }
        public string? ImgUrl { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<Tag>? Tag { get; set; }
        public List<int>? TagIds { get; set; }
        [Required]
        public IFormFile MainPhoto { get; set; }
        [Required]
        public IFormFile HoverPhoto { get; set; }
        public List<IFormFile>? Photos { get; set; }

    }
}
