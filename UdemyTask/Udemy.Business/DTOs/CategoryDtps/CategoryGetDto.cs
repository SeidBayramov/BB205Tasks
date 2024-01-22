using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.Business.DTOs.BaseDtos;

namespace Udemy.Business.DTOs.CategoryDtps
{
    public class CategoryGetDto : BaseAuditableEntityDto
    {
        public string Title { get; set; }
    }
    public class CategoryGetDtoValidator : AbstractValidator<CategoryGetDto>
    {
        public CategoryGetDtoValidator() 
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title can't be empty.")
                .MaximumLength(50)
                .WithMessage("Title length can't be more than 50 characters.");
        }
    }
}
