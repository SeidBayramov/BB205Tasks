namespace Pustok_Temp.Areas.Manage.ViewModel
{
    public class UpdateBlogVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        public List<BlogTag>? BlogTags { get; set; }
        public List<int>? TagIds { get; set; }
        public IFormFile? MainPhoto { get; set; }
        public IFormFile? HoverPhoto { get; set; }
        public List<IFormFile>? Photos { get; set; }
        public List<BlogImageVm>? BlogImagesVm { get; set; }
        public List<int>? ImageIds { get; set; }
    }
    public class BlogImageVm
    {
        public int Id { get; set; }
        public bool? IsPrime { get; set; }
        public string ImgUrl { get; set; }
    }
}
