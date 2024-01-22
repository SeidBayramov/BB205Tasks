namespace Carvilla.Areas.Manage.ViewModels
{
    public class CarUpdateVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string? ImageUrl { get; set; }

        public IFormFile? Image { get; set; }

    }
}
