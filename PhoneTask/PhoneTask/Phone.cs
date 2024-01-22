using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PhoneTask
{
    internal class Phone
    {

        private int _id;
        private string _name;
        private string _brandname;
        private double _price;
        private double _count;
            
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Id must be greater than zero");

                }
                else
                {
                    _id = value;
                }


            }
        }
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value.Length < 2)
                {
                    Console.WriteLine("Name must be at least 2 letters long");
                }
                else
                {
                    _name = value;
                }
            }
        }
        public string BrandName
        {
            get
            {
                return _brandname;
            }
            set
            {
                if (value.Length < 3)
                {
                    Console.WriteLine("BrandName must be at least 3 letters");
                }
                else
                {
                    _brandname = value;
                }

            }
        }
        public double Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Price must be greater than zero");
                }
                else
                {
                    _price = value;
                }
            }
        }


        public double Count {
            get
            {
                return _count;
            }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Count must be greather than zero");
                }
                else
                {
                    _count = value;
                }
            }
    }


        public Phone(int Id, string Name, string BrandName, double Price, int Count)
        {

            this.Id = Id;
            this.Name = Name;
            this.BrandName = BrandName;
            this.Price = Price;
            this.Count = Count;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Phone ID: {Id}");
            Console.WriteLine($"Name: {Name}");
            Console.WriteLine($"Brand: {BrandName}");
            Console.WriteLine($"Price: ${Price}");
            Console.WriteLine($"Count: {Count}");
        }
    }
}
