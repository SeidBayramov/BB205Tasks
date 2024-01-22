using Pustok_Temp.Models;

namespace Pustok_Temp.Areas.Manage.ViewModel
{
    public class CreateBlogVM
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImgUrl { get; set; }
        public List<BlogTag>? BlogTags { get; set; }
        public List<BlogImages>? BlogImages { get; set; }
        public Tag? Tag { get; set; }
        public List<int>? TagIds { get; set; }
        [Required]
        public IFormFile MainPhoto { get; set; }
        [Required]
        public IFormFile HoverPhoto { get; set; }
        public List<IFormFile>? Photos { get; set; }
    }
}
