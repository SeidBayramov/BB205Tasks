using App.BUSINESS.DTOs.Student;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BUSINESS.DTOs.Teacher
{
    public class CreateTeacherDto
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int? Age { get; set; }
    }
    public class TeacherCreateDtoValidator : AbstractValidator<CreateTeacherDto>
    {
        public TeacherCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("ad mutleqdir.")
                .MinimumLength(3).WithMessage("ad en az 3 herf olmalıdır.")
                .MaximumLength(100).WithMessage("ad en cox 55 herf olmalıdır.");
            RuleFor(x => x.Surname)
               .NotEmpty().WithMessage("soyad mutleqdir.")
               .MinimumLength(3).WithMessage("soyad en az 3 herf olmalıdır.")
               .MaximumLength(100).WithMessage("soyad en cox 55 herf olmalıdır.");
            RuleFor(x => x.Age)
                .NotEmpty().WithMessage("yas mutleqdir");
        }
    }
}
