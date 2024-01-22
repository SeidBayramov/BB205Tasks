using System.Text;

namespace CustomsMethod
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();

            person.FullName = "Said Bayramov";
            person.Age = 19;
            person.PhoneNumber = "                   051-588-85-25         ";

            Console.WriteLine("Full Name: " + person.FullName);
            Console.WriteLine("Age: " + person.Age);
            Console.WriteLine("Phone Number: " + person.PhoneNumber);


                bool containsS = Person.CustomContains(person.FullName, "S");
                Console.WriteLine(containsS);

                string replace = Person.CustomReplace(person.FullName, "S", "T");
                Console.WriteLine(replace);

                string subsrting = Person.CustomSubstring(person.FullName, 0, 5);
                Console.WriteLine(subsrting);

                string trimS = Person.CustomTrim(person.PhoneNumber);
                Console.WriteLine(trimS);

            }

        }
    }

