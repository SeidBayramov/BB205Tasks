using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.Core.Entities.Common;

namespace Udemy.Core.Entities
{
    public class Course : BaseAuditableEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }
        List<StudentsCourses> studentsCourses { get; set; }
    }
}
