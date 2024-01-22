using JofExam.Models.Common;

namespace JofExam.Models
{
    public class Fruit:BaseEntity 
    {
        public string Name { get; set; }
        public string SubTitle { get; set; }
        public string? ImageUrl { get; set; }

    }
}
