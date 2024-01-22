using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Udemy.Business.DTOs.CategoryDtps
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
    }
    public class CreateCategoryDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(50)
                .WithMessage("Name cannot be longer than 50 characters");
        }
    }
}
