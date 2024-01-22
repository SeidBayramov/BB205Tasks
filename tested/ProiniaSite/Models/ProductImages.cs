namespace ProiniaSite.Models
{
    public class ProductImages
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public bool IsPrime { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
