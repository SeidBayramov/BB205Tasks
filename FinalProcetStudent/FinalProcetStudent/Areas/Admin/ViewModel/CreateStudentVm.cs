using System.ComponentModel.DataAnnotations;

namespace FinalProcetStudent.Areas.Admin.ViewModel
{
    public class CreateStudentVm
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public byte Age { get; set; }

        public DateTime BornTime { get; set; }

        public double Point { get; set; }

        [Required]
        public IFormFile Image { get; set; }


    }
}
