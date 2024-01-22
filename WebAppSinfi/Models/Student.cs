namespace WebAppl.Models
{
    public class Student
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public string ImagePath { get; set; }

        public int GroupId { get; set; }
        public Group Groups { get; set; }




    }
}

