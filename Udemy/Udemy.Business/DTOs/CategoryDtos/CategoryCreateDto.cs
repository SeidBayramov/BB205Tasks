using Udemy.Api.DTOs.BaseDtos;

namespace Udemy.Api.DTOs.CategoryDtos
{
    public class CategoryCreateDto:BaseAuditableEntityTableDto
    {
        public string Title { get; set; }
        public int ParentCategoryId { get; set; }

    }
}
