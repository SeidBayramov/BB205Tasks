using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.CORE.Entities.Common
{
    public class BaseAuditableEntity:BaseEntity
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
