
using BlogApp.Core.Common;
using BlogApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.WebBusiness.Entities
{
    public  class Category:BaseEntity
    {
        public string Name { get; set; }
        public string LogoUrl { get; set; }
        public IEnumerable<BlogCategory> BlogCategories { get; set; }
    }
}
