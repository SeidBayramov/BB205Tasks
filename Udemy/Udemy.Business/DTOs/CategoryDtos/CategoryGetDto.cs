using Udemy.Api.DTOs.BaseDtos;

namespace Udemy.Api.DTOs.CategoryDtos
{
    public class CategoryGetDto:BaseAuditableEntityTableDto
    {
        public string Title { get; set; }
    }
}
