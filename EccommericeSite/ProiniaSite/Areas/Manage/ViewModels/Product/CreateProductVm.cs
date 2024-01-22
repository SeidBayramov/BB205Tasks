namespace ProiniaSite.Areas.Manage.ViewModels.Product
{
    public class CreateProductVm
    {
        public string Name { get; set; }

        public string SKU { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int? CategoryId { get; set; }

        public List<int>? TagIds { get; set; }

        [Required]
        public IFormFile MainPhoto { get; set; }

        [Required]
        public IFormFile HoverPhoto { get; set; }

        public List<IFormFile> Photos { get; set; }


    }
}