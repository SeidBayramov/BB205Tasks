namespace DianaTemp.ViewModels
{
    public class RegisterVm
    {
        [Required]
        [MinLength(5)]
        [MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(40)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(30)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}