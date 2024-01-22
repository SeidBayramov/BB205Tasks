using ExamAPP1.Entities.Common;

namespace ExamAPP1.Entities
{
    public class Blog:BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }

    }
}
