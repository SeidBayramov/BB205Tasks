using FluentValidation;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace ExamAPP1.ViewModels
{
    public class RegisterVm
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

    }
    public class RegisterVmValidator : AbstractValidator<RegisterVm>
    {
        public RegisterVmValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter the name");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Please enter the Surname");
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Please enter the Username");
            RuleFor(x => x.Email).EmailAddress().NotEmpty();
            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password cannot be empty.")
                .Must(x =>
                {
                    Regex regex = new Regex("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$");
                    Match match = regex.Match(x);
                    return match.Success;
                })
                .WithMessage("Password is not in correct format.");
            RuleFor(c => c)
                .Must(c => c.Password == c.ConfirmPassword);

        }


    }
}
