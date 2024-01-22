using Azure;
using DianaTemp.Migrations;
using DianaTemp.Models;
using System.Reflection.Metadata;

namespace DianaTemp.Areas.ViewModels
{
    public class AdminVM
    {
        public List<Product>? products;
        public List<Category> categories;
        public List<Size> Sizes;
        public List<Material> Materials;
        public List<Colour> Colours;

    }
}