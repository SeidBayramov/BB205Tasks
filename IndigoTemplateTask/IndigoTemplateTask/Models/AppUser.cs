using Microsoft.AspNetCore.Identity;

namespace IndigoTemplateTask.Models
{
    public class AppUser : IdentityUser
    {

        public string Name { get; set; }

        public string Surnma { get; set; }

        public bool IsRemained { get; set; }


    }
}
