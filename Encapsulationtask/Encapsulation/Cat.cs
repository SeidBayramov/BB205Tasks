﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encapsulation
{
    internal class Cat : Animal
    {
        public Cat(string Name, int Age) : base(Name, Age)
        {
        }
        public Cat(string Name, int Age, string Breed) : base(Name, Age, Breed)
        {

        }

        public string GetInfo()
        {
            return $"This animal's breed is {Breed}, name is {Name}, and age is {Age}.";
        }
    }
}