using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agency.Core.Common
{
    public class BaseAudiTable:BaseEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? UpdateAt { get; set; }
    }
}
