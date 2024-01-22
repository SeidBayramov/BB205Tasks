namespace Pustok_Temp.Areas.Manage.ViewModel
{

    public class UpdateBookVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public string BookCode { get; set; }
        public double Price { get; set; }
        public string? ImgUrl { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public List<BookTag>? BookTags { get; set; }
        public List<int>? TagIds { get; set; }
        public IFormFile? MainPhoto { get; set; }
        public IFormFile? HoverPhoto { get; set; }
        public List<IFormFile>? Photos { get; set; }
        public List<BookImageVm>? BookImagesVm { get; set; }
        public List<int>? ImageIds { get; set; }
    }
    public class BookImageVm
    {
        public int Id { get; set; }
        public bool? IsPrime { get; set; }
        public string ImgUrl { get; set; }
    }
}
