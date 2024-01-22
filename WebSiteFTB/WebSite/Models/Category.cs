namespace WebSite.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string İmageUrl { get; set; }
        public List<Stories> Stories { get; set; }
        public List<Recipes> Recipes { get; set; }
    }
}