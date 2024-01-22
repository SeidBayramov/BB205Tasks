using IndigoTemplateTask.Models.Base;

namespace IndigoTemplateTask.Models
{
    public class ProductImage:BaseEntity
    {

        public string? ImgUrl { get; set; }

        public int ProductId { get; set; }

        public bool? IsImage { get; set; }
        

        public Product Product { get; set; }

    }
}
