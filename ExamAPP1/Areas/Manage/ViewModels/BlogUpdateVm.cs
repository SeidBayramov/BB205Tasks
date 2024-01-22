namespace ExamAPP1.Areas.Manage.ViewModels
{
    public class BlogUpdateVm
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
