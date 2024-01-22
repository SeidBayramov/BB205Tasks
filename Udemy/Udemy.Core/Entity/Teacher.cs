 using Udemy.Core.Common;

namespace Udemy.Api.Entity
{
    public class Teacher: BaseAudiTable
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set;}
        public ICollection<Course> Courses { get; set; }

    }
}
