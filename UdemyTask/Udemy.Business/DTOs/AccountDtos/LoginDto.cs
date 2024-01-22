using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy.Business.DTOs.AccountDtos
{
    public record LoginDto
    {
        public string UserNameorEmail { get; set; }
        public string Password { get; set; }
    }

    public class LoginValidator : AbstractValidator<LoginDto>
    {
        public LoginValidator()
        {
            RuleFor(p => p.UserNameorEmail)
                .NotEmpty()
                .WithMessage("UserName or Email bos ola bilmez");
            RuleFor(p => p.Password)
                .NotEmpty()
                .WithMessage("Password bos ola bilmez");
        }
    }
}
