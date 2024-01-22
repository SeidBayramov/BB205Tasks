using System.ComponentModel.DataAnnotations;

namespace DianaTemp.Areas.ViewModels
{
    public class CreateProductVm
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int? CategoryId { get; set; }

        public List<int>? SizeIds { get; set; }

        public List<int>? MaterialsIds { get; set; }

        public List<int>? ColorIds { get; set; }

        [Required]
        public IFormFile MainPhoto { get; set; }

        public List<IFormFile> Photos { get; set; }

    }
}
