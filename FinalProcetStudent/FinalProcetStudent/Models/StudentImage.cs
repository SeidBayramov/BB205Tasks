using FinalProcetStudent.Models.Base;

namespace FinalProcetStudent.Models
{
    public class StudentImage:BaseEntity
    {
        public string ImgUrl { get; set; }

        public bool? IsPrime { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }


    }
}
