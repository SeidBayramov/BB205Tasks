namespace ConsoleApp13
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Vehicle car = new Car();
            Vehicle bus = new Bus();


            car.Drive();
            bus.Drive();

            Console.WriteLine("===============================");

            bool car1 = car is Car;
            Console.WriteLine(car1);
            if (car1)
            {
                Car myCar = car as Car;
                myCar.Drive();
            }
            Bus bus1 = bus as Bus;
            Console.WriteLine("Bus=========");
            if (bus1 != null)
            {
                bus1.Drive();
            }

        }
    }
}


















