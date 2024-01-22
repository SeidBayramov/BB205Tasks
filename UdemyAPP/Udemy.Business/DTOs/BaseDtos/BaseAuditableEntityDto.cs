using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Udemy.Core.Entities.Common;

namespace Udemy.Business.DTOs.BaseDtos
{
    public class BaseAuditableEntityDto : BaseEntityDto
    {
        public bool IsDeleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
