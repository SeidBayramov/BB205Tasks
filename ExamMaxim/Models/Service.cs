using ExamMaxim.Models.Common;

namespace ExamMaxim.Models
{
    public class Service:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }

    }
}
