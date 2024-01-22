using FinalProcetStudent.Models.Base;

namespace FinalProcetStudent.Models
{
    public class Student : BaseEntity
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public string UserName { get; set; }

        public byte Age { get; set; }

        public DateTime BornTime { get; set; }

        public  double Point { get; set; }

        public List<StudentImage> StudentImages { get; set; }



    }
}
