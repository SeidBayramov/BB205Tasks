namespace Pustok_Temp.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string UserNameOrEmail { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool IsRemember { get; set; }

    }
}
