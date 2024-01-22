using System.ComponentModel.DataAnnotations;

namespace ExamMaxim.ViewModels
{
    public class LoginVm
    {
        [Required]
        public string UserNameorEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

}
