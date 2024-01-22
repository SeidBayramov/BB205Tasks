using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Agency.Busines.ViewModel.Account
{
    public class LoginVm
    {
        public string UsernameOrEmail { get; set; }
        public string Password { get; set; }

    }
    public class LoginVmValidator : AbstractValidator<LoginVm>
    {
        public LoginVmValidator()
        {
            RuleFor(x => x.UsernameOrEmail)
                .NotEmpty()
                .WithMessage("Username/Email cannot be empty.");
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
           
