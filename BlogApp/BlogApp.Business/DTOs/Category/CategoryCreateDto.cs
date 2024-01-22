using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;

namespace BlogApp.Business.DTOs.Category
{
    public record CategoryCreateDto
    {
        public string? Name { get; set; }
        public IFormFile? Logo { get; set; }
    }

    public class CreateCategoryDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CreateCategoryDtoValidator()
        {
            RuleFor(dto => dto.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(50).WithMessage("Name cannot be longer than 50 characters")
                .MinimumLength(3).WithMessage("Name must be at least 3 characters long");

            RuleFor(dto => dto.Logo)
                .NotNull()
                .WithMessage("The picture cannot be null");
        }
    }
}
