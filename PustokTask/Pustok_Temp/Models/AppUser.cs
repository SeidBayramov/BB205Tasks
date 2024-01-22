
using Microsoft.AspNetCore.Identity;

namespace Pustok_Temp.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }

		public bool IRemained { get; set; }
        public List<BasketItem> BasketItems { get; set; }
        //public List<Order> Order { get; set; }
    }
}
