using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Carvilla.ViewModels
{
    public class RegisterVm
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Username { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class RegisterVmValidator : AbstractValidator<RegisterVm>
    {
        public RegisterVmValidator()
        {
            RuleFor(x => x.Name)
             .MaximumLength(20).WithMessage("Is your first name that long?? Really??")
                .NotEmpty().MinimumLength(3);
            RuleFor(x => x.Surname)
           .MaximumLength(30).WithMessage("Is your lastname that long?? Really??")
           .NotEmpty().MinimumLength(3);

            RuleFor(x => x.Username)
         .MaximumLength(40).WithMessage("Is your username that long?? Really??")
         .NotEmpty().MinimumLength(3);

            RuleFor(x => x.Email).EmailAddress().NotEmpty();

            RuleFor(x => x.Password)
                 .Length(5, 15).NotEmpty();
                

            RuleFor(x => x.ConfirmPassword)
                   .Equal(x => x.Password)
                  .WithMessage("Passwords must match");
        }

     
    }

}
