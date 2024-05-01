using System;
using System.IO;
using System.Diagnostics;
using System.Threading.Tasks;


namespace Lab2
{
    public class Ex3
    {
        public static async Task StartAsync()
        {
            var filesToProcess = new[]
            {
                ("Email-1k.txt", "Email-1k-SORTED.txt"),
                ("Email-10k.txt", "Email-10k-SORTED.txt"),
                ("Email-100k.txt", "Email-100k-SORTED.txt"),
                ("Email-1000k.txt", "Email-1000k-SORTED.txt")
            };

            foreach (var file in filesToProcess)
            {
                var parallelSortTask = Task.Run(() => ProcessAndSaveSortedEmailsAsync(file.Item1, file.Item2));

                Stopwatch parallelSortStopwatch = Stopwatch.StartNew();
                await parallelSortTask;
                parallelSortStopwatch.Stop();
                long parallelSortTime = parallelSortStopwatch.ElapsedMilliseconds;

                Stopwatch librarySortStopwatch = Stopwatch.StartNew();
                LibrarySort(file.Item1);
                librarySortStopwatch.Stop();
                long librarySortTime = librarySortStopwatch.ElapsedMilliseconds;

                double accCoef = (double)librarySortTime / parallelSortTime;

                Console.WriteLine($"File: {file.Item1}");
                Console.WriteLine($"Parallel sort time: {parallelSortTime} ms");
                Console.WriteLine($"Library sort time: {librarySortTime} ms");
                Console.WriteLine($"Acceleration coefficient: {accCoef}");
                Console.WriteLine();
            }

            AppDomain.CurrentDomain.ProcessExit += (sender, e) =>
            {
                foreach (var file in filesToProcess)
                {
                    if (File.Exists(file.Item2))
                    {
                        File.Delete(file.Item2);
                    }
                }
            };
        }

        private static string[] LibrarySort(string inputFile)
        {
            var emails = File.ReadAllLines(inputFile);
            Array.Sort(emails);
            return emails;
        }

        private static async Task ProcessAndSaveSortedEmailsAsync(string inputFile, string outputFile)
        {
            var emails = File.ReadAllLines(inputFile);
            await ParallelMergeSortAsync(emails, 0, emails.Length - 1);
            File.WriteAllLines(outputFile, emails);
        }

        private static async Task ParallelMergeSortAsync(string[] array, int left, int right)
        {
            if (left < right)
            {
                int mid = (left + right) / 2;
                await Task.WhenAll(
                    ParallelMergeSortAsync(array, left, mid),
                    ParallelMergeSortAsync(array, mid + 1, right)
                );
                Merge(array, left, mid, right);
            }
        }

        private static void Merge(string[] array, int left, int mid, int right)
        {
            string[] temp = new string[right - left + 1];
            int i = left;
            int j = mid + 1;
            int k = 0;

            while (i <= mid && j <= right)
            {
                if (array[i].CompareTo(array[j]) <= 0)
                {
                    temp[k++] = array[i++];
                }

                else
                {
                    temp[k++] = array[j++];
                }
            }

            while (i <= mid)
            {
                temp[k++] = array[i++];
            }

            while (j <= right)
            {
                temp[k++] = array[j++];
            }

            for (i = left, k = 0; i <= right; i++, k++)
            {
                array[i] = temp[k];
            }
        }

    }
}
