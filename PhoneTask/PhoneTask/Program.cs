namespace PhoneTask
{
    internal class Program
    {
        static void Main(string[] args)
        {
                Phone phone1 = new Phone(1, "Iphone", " 14 Pro", 4000, 10);
                Phone phone2 = new Phone(2, "Samsung", "Note 22", 2000, 5);
                Phone phone3 = new Phone(3, "Xiomi", "Note8", 699.99, 8);

                Store myStore = new Store("AppleStore");
                myStore.AddPhone(phone1);
                myStore.AddPhone(phone2);
                myStore.AddPhone(phone3);

                myStore.ShowAllPhones();
                myStore.ShowPhonesForPrice(500, 3000);

                myStore.RemovePhone(2);
                myStore.ShowAllPhones();
            }
        }
    }