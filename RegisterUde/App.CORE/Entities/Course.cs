using App.CORE.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.CORE.Entities
{
    public class Course:BaseAuditableEntity
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? TeacherId { get; set; }
        public Teacher? teacher { get; set; }
        public List<StudentsCourses>? studentsCourses { get; set; }

    }
}
