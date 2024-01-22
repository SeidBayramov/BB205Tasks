using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encapsulation
{
    internal class Snake : Animal
    {
        private bool _isPoisonous;
        public bool IsPoisonous
        {
            get { return _isPoisonous; }

            set {
                if (value)
                {
                    Console.WriteLine("Poisonous snakes are not accepted.");
                }
                else
                {
                    _isPoisonous = value;
                }
                    
            }

        }

        public Snake(string Name, int Age) : base(Name, Age)
        {
        }
        public Snake(string Name, int Age, string Breed) : base(Name, Age, Breed)
        {

        }
        public  string GetInfo()
        {
            return $"This snake's breed is {Breed}, name is {Name}, age is {Age}";
        }
    }
}
