using System;
using System.Threading;

namespace Lab2
{
    static public class Ex2
    {
        private const int N = 30;

        private static int[] Q = new int[N];
        private static int[] X = new int[N];
        private static int[] Y = new int[N];

        public static void Start()
        {
            var T0 = new Thread(InitializeX);
            var T1 = new Thread(InitializeY);

            T0.Start();
            T1.Start();
            T0.Join();
            T1.Join();

            var T2 = new Thread(CalculateQ);

            T2.Start();
            T2.Join();

            Console.WriteLine(String.Join(", ", Q));
        }

        private static void InitializeX()
        {
            for (int i = 0; i < N; i++)
            {
                X[i] = 1;
            }
        }

        private static void InitializeY()
        {
            for (int i = 0; i < N; i++)
            {
                Y[i] = i + 1;
            }
        }

        private static void CalculateQ()
        {
            for (int i = 0; i < N; i++)
            {
                Q[i] = 3 * X[i] + 5 * Y[i];
            }
        }
    }
}
