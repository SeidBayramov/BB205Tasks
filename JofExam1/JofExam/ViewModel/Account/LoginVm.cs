using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace JofExam.ViewModel.Account
{
    public class LoginVm
    {
        [Required]
        public string UserNameorEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
    public class LoginVmFluentValidation : AbstractValidator<LoginVm>
    {
        public LoginVmFluentValidation()
        {
            RuleFor(vm => vm.UserNameorEmail).NotNull().NotEmpty()
               .WithMessage("Please provide an UserName or Email");

            RuleFor(vm => vm.Password).NotNull().NotEmpty()
                .WithMessage("Please provide a Password.");

        }
    }
}
