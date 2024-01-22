using App.BUSINESS.DTOs.BaseDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BUSINESS.DTOs.Course
{
    public class UpdateCourseDto:BaseEntityDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
    public class CourseUpdateDtoValidator : AbstractValidator<UpdateCourseDto>
    {
        public CourseUpdateDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Ttile adı mutleqdir.")
                .MinimumLength(3).WithMessage("Title adı en az 3 herf olmalıdır.")
                .MaximumLength(100).WithMessage("Category adı en cox 55 herf olmalıdır.");
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description bos qoula bilmez!")
                .MinimumLength(15).WithMessage("Description en az 15 herfden ibaret olmalidir");
        }
    }
}
