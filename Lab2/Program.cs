using System;
using System.Threading.Tasks;

namespace Lab2
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Завдання 1");
            Ex1.Start();

            Console.WriteLine("\nЗавдання 2");
            Ex2.Start();

            Console.WriteLine("\nЗавдання 3");
            await Ex3.StartAsync();

            Console.ReadLine();
        }
    }
}
