using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace JofExam.ViewModel.Account
{
    public class RegisterVm
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]

        public string ConfirmPassword { get; set; }
    }
    public class RegisterVmValidator : AbstractValidator<RegisterVm>
    {
        public RegisterVmValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("User name is required.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email format.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required.");
            RuleFor(x => x.ConfirmPassword)
        .    Equal(x => x.Password)
            .WithMessage("Passwords do not match");

        }
    }
}
