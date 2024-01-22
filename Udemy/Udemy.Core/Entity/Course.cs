using Udemy.Core.Common;

namespace Udemy.Api.Entity
{
    public class Course: BaseAudiTable
    {
        public string  Title { get; set; }
        public string Description { get; set; }
        public  int TeacherId { get; set; }
        public ICollection<StudentCourse> studentCourses { get; set;}
        public Teacher Teacher { get; set; }
    }
}
