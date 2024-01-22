namespace DianaTemp.Models
{
	public class Colour:BaseEntity
	{
		public string Name { get; set; }
		public List<ProductColour> ProductColours { get; set; }
	}
}
