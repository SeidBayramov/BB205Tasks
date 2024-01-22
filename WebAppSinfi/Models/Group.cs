namespace WebAppl.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Student> students;


        public Group()
        {
            students = new List<Student>();
        }

    }
}
