using System;

namespace tasks
{
    internal class Person
    {
        private string _name;
        public string surname;
        public int age;

        public Person(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

        public Person(string surname, int age)
        {
            this.surname = surname;
            this.age = age;
        }

        public string name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value.Length >= 2)
                {
                    _name = value;
                }
                else
                {
                    Console.WriteLine("İsim en az 2 harfli olmalıdır.");
                }
            }
        }
    }
}
