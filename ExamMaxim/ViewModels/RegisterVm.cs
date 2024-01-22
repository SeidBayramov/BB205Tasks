using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace ExamMaxim.ViewModels
{
    public class RegisterVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
    public class RegisterVmValidator:AbstractValidator<RegisterVm>
    {
        public RegisterVmValidator()
        {
            RuleFor(x=>x.Name).NotEmpty().WithMessage("Plase write Name");
            RuleFor(x=>x.Surname).NotEmpty().WithMessage("Plase write Surname");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Plase write Username");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Please your password");
           RuleFor(x => x.ConfirmPassword)
           .Equal(x => x.Password)
            .WithMessage("Passwords must match");

        }
    }
}
