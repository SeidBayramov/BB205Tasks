namespace DianaTemp.Models
{
	public class Material : BaseEntity
	{
		public string Name { get; set; }
		public List<ProductMaterial> ProductMaterials { get; set; }

	}
}
