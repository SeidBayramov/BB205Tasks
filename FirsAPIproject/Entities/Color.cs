using FirsAPIproject.Entities.Base;

namespace FirsAPIproject.Entites
{
    public class Color:BaseEntity
    {

        public string Name { get; set; }

        public List<Car> Cars { get; set; }
    }
}
