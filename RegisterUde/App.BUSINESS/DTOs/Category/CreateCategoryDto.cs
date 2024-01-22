using App.CORE.Entities.Common;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BUSINESS.DTOs.Category
{
    public class CreateCategoryDto
    {
        public string? Name { get; set; }
        public IFormFile? LogoImg { get; set; }
        public int? ParentCategoryId { get; set; }

    }

    public class CategoryCreateDtoValidator : AbstractValidator<CreateCategoryDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category adı mutleqdir.")
                .MinimumLength(3).WithMessage("Category adı en az 3 herf olmalıdır.")
                .MaximumLength(100).WithMessage("Category adı en cox 55 herf olmalıdır.");
        }
    }
}
