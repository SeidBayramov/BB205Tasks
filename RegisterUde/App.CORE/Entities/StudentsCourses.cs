using App.CORE.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.CORE.Entities
{
    public class StudentsCourses:BaseAuditableEntity
    {
        public int? StudentId { get; set; }
        public Student? Student { get; set; }
        public int? CourseId { get; set; }
        public Course? Course { get; set; }
    }
}
