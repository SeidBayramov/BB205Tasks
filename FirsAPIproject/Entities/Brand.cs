using FirsAPIproject.Entities.Base;

namespace FirsAPIproject.Entites
{
    public class Brand :BaseEntity
    {
        public string Name { get; set; }

        public List<Car> Cars { get; set; }

    }
}
