using Priona.Core.Entity.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Priona.Core.Entity
{
    public class Sliders:BaseEntity
    {
        [Required, StringLength(25, ErrorMessage = "The Lenghest must be 10 words")]
        public string Title { get; set; }

        public string SubTitle { get; set; }

        public string Description { get; set; }

        [StringLength(maximumLength: 100)]
        public string? ImgUrl { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }
    }
}

