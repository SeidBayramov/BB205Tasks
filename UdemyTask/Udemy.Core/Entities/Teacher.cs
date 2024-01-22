using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.Core.Entities.Common;

namespace Udemy.Core.Entities
{
    public class Teacher : BaseAuditableEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        ICollection<Course> courses { get; set; }
    }
}
