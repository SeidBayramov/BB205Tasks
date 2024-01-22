using IndigoTemplateTask.Models.Base;

namespace IndigoTemplateTask.Models
{
    public class Product:BaseEntity
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public List<ProductImage> ProductImages { get; set; }

    }
}
