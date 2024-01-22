using EntityTask.Models;
using EntityTask.Services;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

namespace EntityTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            StudentService studentService = new StudentService();
            GroupService groupService = new GroupService();

            Student student = new Student()
            {
                Name = "Seid",
                Age = 19
            };
            Student student2 = new Student()
            {
                Name = "Ferid",
                Age = 19,
            };

            Group group = new Group()
            {
                Name = "BB205"
            };

            Group group1 = new Group()
            {
                Name = "BB211"
            };

            //Add Method

            studentService.AddStudent(student);
            groupService.AddGroup(group);

            studentService.AddStudent(student2);
            groupService.AddGroup(group1);

            //Remove Method

            studentService.RemoveStudent(2);
            groupService.RemoveGroup(2);

            //GetAll Method

            studentService.GetAllStudents();
            groupService.GetAllGroups();


            //Update Method

            studentService.UpdateStudent(1);
            groupService.UpdateGroup(1);



        }
    }
}