using Udemy.Api.DTOs.BaseDtos;

namespace Udemy.Api.DTOs.CategoryDtos
{
    public class CategoryUpdateDto:BaseAuditableEntityTableDto
    {
        public string Title { get; set; }
        public int  ParentCategoryId { get; set; }
    }
}
