using FluentValidation;
using System.Text.RegularExpressions;

namespace ExamAPP1.ViewModels
{
    public class LoginVm
    {
        public string UserNameorEmail { get; set; }
        public string Password { get; set; }
    }

    public class LoginVmValidator:AbstractValidator<LoginVm>
    {
        public LoginVmValidator()
        {
            RuleFor(x => x.UserNameorEmail).EmailAddress().NotEmpty().WithMessage("Please enter the UserName or Email");
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
        }
    }
}

