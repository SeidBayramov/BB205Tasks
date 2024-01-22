using System.Threading.Channels;

namespace tasks
{
    internal class Program
    {
        static void Main(string[] args)
        {
            {
                Employee employee = new Employee("J", "Doe",220);
                int salary = employee.Salary;
                Console.WriteLine(employee.name);
                Console.WriteLine(employee.Salary);

            }
        }
    }


}

    