using System;

namespace Encapsulation
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Dog dog = new Dog("Dazy", 3, "Golden Retriever");
            Cat cat = new Cat("Tom", 2, "Siamese");
            Snake snake = new Snake("ColdSkin", 4, "Python");
            Snake poisonousSnake = new Snake("Venom", 5, "Cobra");
            Dolphin dolphin = new Dolphin("Flipp", 8, "Bottleneos");

            Console.WriteLine(dog.GetInfo());
            Console.WriteLine(cat.GetInfo());
            Console.WriteLine(snake.GetInfo());
            poisonousSnake.Age = -3; 
            Console.WriteLine(poisonousSnake.GetInfo());
            Console.WriteLine(dolphin.GetInfo());

        }
    }
    }