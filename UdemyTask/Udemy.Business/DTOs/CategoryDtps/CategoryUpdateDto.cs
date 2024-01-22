using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.Business.DTOs.BaseDtos;

namespace Udemy.Business.DTOs.CategoryDtps
{
    public class CategoryUpdateDto : BaseEntityDto
    {
        public string Name { get; set; }
        public int? ParentCategoryId { get; set; }
    }
    public class CategoryUpdateDtoValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(50)
                .WithMessage("Name cannot be longer than 50 characters");
        }
    }
}
