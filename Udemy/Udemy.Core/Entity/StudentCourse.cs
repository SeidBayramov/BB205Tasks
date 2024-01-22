using Udemy.Core.Common;

namespace Udemy.Api.Entity
{
    public class StudentCourse: BaseAudiTable
    {
        public int StudentId { get; set; }
        public int CourseId { get; set;}
        public Student Student { get; set; }
        public Course Course { get; set; }
    }
}
