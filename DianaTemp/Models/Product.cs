using System.ComponentModel.DataAnnotations.Schema;

namespace DianaTemp.Models
{
	public class Product:BaseEntity
	{
        public string Name { get; set; }
		public string Description { get; set; }

		public double Price { get; set; }

		public bool IsDeleted { get; set; }

		public int? CategoryId { get; set; }

		public Category? Category { get; set; }

		public List<ProductSize>? ProductSizes { get; set; }
		public List<ProductMaterial>? ProductMaterial { get; set; }
		public List<ProductColour>? ProductColours { get; set; }
		public List<ProductImages>? ProductImages { get; set; }

        //[NotMapped]
        //public IFormFile ImageFile { get; set; }

    }
}
