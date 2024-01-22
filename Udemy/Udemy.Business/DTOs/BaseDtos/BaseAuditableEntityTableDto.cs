namespace Udemy.Api.DTOs.BaseDtos
{
    public class BaseAuditableEntityTableDto:BaseEntityDto
    {
        public DateTime? CreatedAt { get; set; }  
        public DateTime? UpdatedAt { get; set;}
        public bool IsDeleted { get; set; }
    }
}
