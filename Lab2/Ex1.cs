using System;
using System.Threading;

namespace Lab2
{
    static public class Ex1
    {
        private static int x = 1;
        private static int y = 5;

        private static Semaphore S1 = new Semaphore(0, 1);
        private static Semaphore S2 = new Semaphore(0, 1);
        private static Semaphore S3 = new Semaphore(0, 1);
        private static Semaphore S4 = new Semaphore(0, 1);


        public static void Start()
        {
            Console.WriteLine($"x = {x}; y = {y}");

            var T1 = new Thread(() =>
            {
                x *= 5;
                S1.Release();
            });
            var T2 = new Thread(() =>
            {
                y += 2;
                S2.Release();
            });
            var T3 = new Thread(() =>
            {
                S1.WaitOne();
                x += 2;
                S3.Release();
            });
            var T4 = new Thread(() =>
            {
                S2.WaitOne();
                y -= 3;
                S4.Release();
            });
            var T5 = new Thread(() =>
            {
                S3.WaitOne();
                S4.WaitOne();
                y *= x;
            });

            T1.Start();
            T2.Start();
            T3.Start();
            T4.Start();
            T5.Start();
            T1.Join();
            T2.Join();
            T3.Join();
            T4.Join();
            T5.Join();


            Console.WriteLine($"x = {x}; y = {y}");
        }
    }
}
