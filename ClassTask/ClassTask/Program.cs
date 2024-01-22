namespace ClassTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Developer developer = new Developer("Rasul", "Rustamli", 27, 6);
            BackEnd BackEndDeveloper = new BackEnd("Said", "Bayramov", 7, 3);
            FrontEnd FrontEndDeveloper = new FrontEnd("Ferid", "Buludlu", 4, 2);

            Console.WriteLine("Developer:");
            Console.WriteLine($"Name: {developer.Name}");
            Console.WriteLine($"Surname: {developer.Surname}");
            Console.WriteLine($"Age: {developer.Age}");
            Console.WriteLine($"Experience: {developer.Experience}");

            Console.WriteLine("\nBackend Developer:");
            Console.WriteLine($"Name: {BackEndDeveloper.Name}");
            Console.WriteLine($"Surname: {BackEndDeveloper.Surname}");
            Console.WriteLine($"Experience: {BackEndDeveloper.Experience}");
            Console.WriteLine($"SQL Experience Years: {BackEndDeveloper.SqlExperienceYear}");

            Console.WriteLine("\nFrontend Developer:");
            Console.WriteLine($"Name: {FrontEndDeveloper.Name}");
            Console.WriteLine($"Surname: {FrontEndDeveloper.Surname}");
            Console.WriteLine($"Experience: {FrontEndDeveloper.Experience}");
            Console.WriteLine($"React Experience Years: {FrontEndDeveloper.ReactExperienceYear}");
        }
    }
}
