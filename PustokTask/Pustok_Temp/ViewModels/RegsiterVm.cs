namespace Pustok_Temp.ViewModels
{
	public class RegsiterVm
	{
		[Required]
		[MinLength(3)]
		[MaxLength(50)]
		public string Name { get; set; }

		[Required]
		[MinLength(3)]
		[MaxLength(50)]
		public string Surname { get; set; }
		public string UserName { get; set; }



		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[DataType(DataType.Password)]
		[MinLength(8)]
		public string Password { get; set; }

		[DataType(DataType.Password)]
		[Compare(nameof(Password))]
		public string ConfrimPassword { get; set; }

	}

}
