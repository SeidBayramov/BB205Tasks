using System.ComponentModel.DataAnnotations;

namespace FinalProcetStudent.ViewModel
{
    public class RegisterVm
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [MaxLength(50)]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }



    }
}
