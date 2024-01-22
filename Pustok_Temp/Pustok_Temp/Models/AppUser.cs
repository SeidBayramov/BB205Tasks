using Microsoft.AspNetCore.Identity;

namespace Pustok_Temp.Models
{
	public class AppUser : IdentityUser
	{
        public string Fullname { get; set; }
		

    }
}
