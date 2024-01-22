using System;

namespace tasks
{
    internal class Employee : Person
    {
        private int _salary;

        public Employee(string name, string surname, int salary) : base(name, surname)
        {
            Salary = salary;
        }

        public int Salary
        {
            get
            {
                return _salary;
            }
            set
            {
                if (value > 0)
                {
                    _salary = value;
                }
                else
                {
                    Console.WriteLine( "Salary cannot be less than 0.");
                }
            }
        }
    }
}
