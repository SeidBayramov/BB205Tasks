namespace WebSite.Models
{
    public class Stories
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string LongTitle { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public DateTime Date { get; set; }
        public Category Category { get; set; }
    }
}