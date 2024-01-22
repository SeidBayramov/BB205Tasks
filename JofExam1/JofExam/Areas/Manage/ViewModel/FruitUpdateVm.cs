using System.ComponentModel.DataAnnotations;

namespace JofExam.Areas.Manage.ViewModel
{
    public class FruitUpdateVm
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SubTitle { get; set; }
        public string? ImageUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
}
