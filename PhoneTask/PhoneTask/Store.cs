using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PhoneTask
{
    internal class Store
    {
        private string _name;
        public Phone[] Phones;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value.Length < 3)
                {
                    Console.WriteLine(("Store name must be at least 3 letters"));
                }
                else
                {
                    _name = value;
                }
            }
        }
               
        public Store(string name)
        { 
            this.Name = name;
            this.Phones = new Phone[0];
        }

        public void AddPhone(Phone phone)
        {
            Phones = Phones.Concat(new[] { phone }).ToArray();
        }

        public void ShowAllPhones()
        {
            Console.WriteLine($"Phones in {Name}:");

            foreach (var phone in Phones)
            {
                phone.ShowInfo();
                Console.WriteLine();
            }
        }

        public void ShowPhonesForPrice(double minPrice, double maxPrice)
        {
            Console.WriteLine($"Phones in {Name} between ${minPrice} and ${maxPrice}:");

            bool found = false;

            foreach (var phone in Phones)
            {
                if (phone.Price >= minPrice && phone.Price <= maxPrice)
                {
                    phone.ShowInfo();
                    Console.WriteLine();
                    found = true;
                }
            }

            if (!found)
            {
                Console.WriteLine("No phones found in the price range.");
            }
        }
        public void RemovePhone(int id)
        {
            int indexToRemove = -1;

            for (int i = 0; i < Phones.Length; i++)
            {
                if (Phones[i].Id == id)
                {
                    indexToRemove = i;
                    break;
                }
            }

            if (indexToRemove >= 0)
            {
                Phone[] newPhones = new Phone[Phones.Length - 1];
                for (int i = 0, j = 0; i < Phones.Length; i++)
                {
                    if (i != indexToRemove)
                    {
                        newPhones[j] = Phones[i];
                        j++;
                    }
                }
                Phones = newPhones;
            }
        }
    }
}

