using FluentValidation;
using System.Text.RegularExpressions;

namespace Carvilla.ViewModels
{
	public class LoginVm
	{
		public string UserNameorEmail { get; set; }
		public string Password { get; set; }
	}
	public class LoginVmValidator : AbstractValidator<LoginVm>
	{
		public LoginVmValidator()
		{

            RuleFor(x => x.UserNameorEmail)
       .NotEmpty().MinimumLength(3).WithMessage("Username/Email ve ya password yanlisdir");

            RuleFor(x => x.Password)
                .Length(5, 15).NotEmpty();

        }
    
    }
}

