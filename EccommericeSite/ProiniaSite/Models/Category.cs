using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;

namespace ProiniaSite.Models
{
    public class Category:BaseEntity
    {

        [StringLength(maximumLength:10,ErrorMessage ="Uzunluq 10 olmalidi")]
        public string Name { get; set; }

        public List<Product>? Products { get; set; }



    }
}
