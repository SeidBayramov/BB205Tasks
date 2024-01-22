using System.ComponentModel.DataAnnotations;

namespace FinalProcetStudent.ViewModel
{
    public class LoginVm
    {
        [Required]
        public string UserNameorEmail { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        public bool RememberMe { get; set; }


    }
}
