
using Size = DianaTemp.Models.Size;

namespace DianaTemp.ViewModels
{
	public class HomeVm
	{
		public List<Product> Products { get; set; }
		public List<Slider> Sliders { get; set; }
		public List<Category> Categories { get; set; }
        public List<Colour> Colors { get; set; }
        public List<Size> Sizes { get; set; }
        public List<Material> Materials { get; set; }

    }
}
