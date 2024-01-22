using FluentValidation;

namespace Carvilla.Areas.Manage.ViewModels
{
    public class CarCreateVm
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public IFormFile Image { get; set; }
    }
    public class CreateVmValidator : AbstractValidator<CreateVmValidator>
    {
        public CreateVmValidator()
        {
        }
    }
}
