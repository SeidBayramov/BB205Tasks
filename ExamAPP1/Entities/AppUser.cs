using Microsoft.AspNetCore.Identity;

namespace ExamAPP1.Entities
{
    public class AppUser:IdentityUser
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
