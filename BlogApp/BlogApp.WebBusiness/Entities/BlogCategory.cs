using BlogApp.Core.Common;
using BlogApp.WebBusiness.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Core.Entities
{
    public class BlogCategory:BaseEntity
    {
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int BlogId { get; set; }
        public Blog Blog { get; set; }

    }
}
