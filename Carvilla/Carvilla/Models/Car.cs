using Carvilla.Models.Common;

namespace Carvilla.Models
{
    public class Car:BaseEntity
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public string ImageUrl { get; set; }
    }
}
