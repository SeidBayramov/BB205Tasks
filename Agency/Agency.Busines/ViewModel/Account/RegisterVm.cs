using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Agency.Busines.ViewModel.Account
{
    public class RegisterVm
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }
    public class RegisterValidator : AbstractValidator<RegisterVm>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name bos ola bilmez");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Surname bos ola bilmez");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email bos ola bilmez");
            RuleFor(customer => customer.Password)
          .NotEmpty().WithMessage("Password  boş geçilemez!")
          .Must(IsPasswordValid).WithMessage("Password en az sekiz karakter, en az bir harf ve bir sayı içermelidir!");
            RuleFor(c => c)
                .Must(c => c.Password == c.ConfirmPassword);

        }
        private bool IsPasswordValid(string arg)
        {
            Regex regex = new Regex(@"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$");
            return regex.IsMatch(arg);
        }

    }


}
