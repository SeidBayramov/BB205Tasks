using Azure;

namespace DianaTemp.Models
{
	public class ProductMaterial:BaseEntity
	{
		public int ProductId { get; set; }

		public Product Product { get; set; }

		public int MaterialId { get; set; }

		public Material Material { get; set; }
	}
}
