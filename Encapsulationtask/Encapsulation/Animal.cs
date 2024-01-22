using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encapsulation
{
    internal class Animal
    {
        public string Name;
        private int _age;
        public string Breed;

        public int Age
        {
            get { return _age; }
            set
            {
                if (value >= 0)
                {
                    _age = value;
                }
                else
                {
                    Console.WriteLine("Age is more than 0");
                }
            }
        }
        public Animal(string Name, int Age)
        {
            this.Name = Name;
            this.Age = Age;

        }
        public Animal(string Name, int Age, string Breed)
        {
            this.Name = Name;
            this.Age = Age;
            this.Breed = Breed;
        }

            public string GetInfo()
        {
           return $"This animal's breed is {Breed}, name is {Name}, and age is {Age}.";
        }
    }
    }

