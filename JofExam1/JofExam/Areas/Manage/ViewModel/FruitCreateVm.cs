using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace JofExam.Areas.Manage.ViewModel
{
    public class FruitCreateVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string SubTitle { get; set; }
        [Required]
        public IFormFile? Image { get; set; }
    }
    public class FruitUpdateVmValidator : AbstractValidator<FruitCreateVm>
    {
        public FruitUpdateVmValidator()
        {
            RuleFor(x=>x.Name).NotEmpty();
            RuleFor(x=>x.SubTitle).NotEmpty();

        }
    }

}