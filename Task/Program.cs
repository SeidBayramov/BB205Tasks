using System.Runtime.CompilerServices;

namespace Task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Students[] students = new Students[3];
            students[0] = new Students("Said", "Bayramov", 91);
            students[1] = new Students("Ferid", "Buludlu", 98);
            students[2] = new Students("Rufet", "Quliyev", 55);



            Group studentGroup = new Group("BB205");

            string filterName = "Ferid";

            Students[] filteredStudents = studentGroup.FilterByName(filterName);

            Console.WriteLine("Filtered Students:");

            foreach (var student in filteredStudents)
            {
                if (student != null)
                {
                    Console.WriteLine(student.Name + " " + student.Surname + " - " + student.AvgPoint);
                }
            }
        }
    }
}