using App.CORE.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.CORE.Entities
{
    public class Teacher:BaseAuditableEntity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int? Age { get; set; }
        public List<Course> courses { get; set; }
    }
}
