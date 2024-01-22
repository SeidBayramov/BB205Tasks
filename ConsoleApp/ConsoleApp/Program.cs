    internal class Program
    {
        static void Main(string[] args)
        {
          Group group=null;

            while (true)
            {
                Console.WriteLine("====== Menu ======");
                Console.WriteLine("1. Create a group");
                Console.WriteLine("2. Show all students");
                Console.WriteLine("3. Add student");
                Console.WriteLine("4. Filter students by name");
                Console.WriteLine("0. Quit");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.Write("Enter group number: ");
                        string groupNo = Console.ReadLine();

                        Console.Write("Enter student limit: ");
                        int studentLimit = int.Parse(Console.ReadLine());
                        if(studentLimit >= 5 && studentLimit <= 18)
                        {
                            group = new Group(groupNo, studentLimit);
                            Console.WriteLine("Group created.");
                        }
                        else
                        {
                            break;
                        }
                        break;

                    case "2":
                        if (group != null)
                        {
                            Console.WriteLine("Students in the group:");
                            foreach (var student in group.FilterByName(""))
                            {
                                if (student != null)
                                {
                                    Console.WriteLine($"{student.Name} {student.Surname} (AvgPoint: {student.AvgPoint})");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Group not created yet.");
                        }
                        break;

                    case"3":

                        if (group != null)
                        {
                            Console.Write("Enter student name: ");
                            string name = Console.ReadLine();
                            Console.Write("Enter student surname: ");
                            string surname = Console.ReadLine();
                            Console.Write("Enter average point (0-100): ");
                            double avgPoint = double.Parse(Console.ReadLine());

                            Student student = new Student(name, surname, avgPoint);
                            group.AddStudent(student);
                            Console.WriteLine("Succesfull register:");
                            Console.WriteLine(student.Name+" "+student.Surname);
                        }
                        else
                        {
                            Console.WriteLine("Group not created yet.");
                        }
                        break;

                    case "4":
                        if (group != null)
                        {
                            Console.Write("Enter filter string: ");
                            string filterString = Console.ReadLine();
                            Student[] filteredStudents = group.FilterByName(filterString);
                            Console.WriteLine("Students matching the filter:");
                            foreach (var student in filteredStudents)
                            {
                                if (student != null)
                                {
                                    Console.WriteLine($"{student.Name} {student.Surname} (AvgPoint: {student.AvgPoint})");
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Group not created yet.");
                        }
                        break;

                    case "0":
                        return;
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
        }
    }
}
