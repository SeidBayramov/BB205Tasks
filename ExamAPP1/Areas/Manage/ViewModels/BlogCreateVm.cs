using Microsoft.AspNetCore.Http;

namespace ExamAPP1.Areas.Manage.ViewModels
{
    public class BlogCreateVm
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
