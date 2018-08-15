using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Parallel_Invoke_For_Foreach_Test
{
    class Program
    {
        static CancellationTokenSource cts = new CancellationTokenSource();
        static Random random = new Random();
        static IEnumerable<int> Range(int start, int end, int step)
        {
            for (int i = start; i < end; i+=step)
            {
                yield return i;
            }
        }

        static void Main(string[] args)
        {
            Parallel.Invoke(() =>
            {
                Console.WriteLine($"First {Task.CurrentId}");
            }, () =>
            {
                Console.WriteLine($"Second {Task.CurrentId}");
            }, () =>
            {
                Console.WriteLine($"Third {Task.CurrentId}");
            });

            //
            var po = new ParallelOptions();
            po.CancellationToken = cts.Token;

            Task.Factory.StartNew(() =>
            {
                Parallel.For(1, 11, po, i =>
                {
                    Console.WriteLine($"Parallel Start {Task.CurrentId}");
                    Thread.Sleep(random.Next(3000, 5000));
                    Console.WriteLine($"Parallel End {Task.CurrentId}");
                });
            });

            Console.ReadKey();
            Console.WriteLine("Cancel");
            cts.Cancel();

            Parallel.ForEach(Range(1, 20, 3), i =>
            {
                Console.WriteLine($"Number : {i} (Task : {Task.CurrentId})");
            });

            string[] words = { "oh", "What", "a", "night" };
            Parallel.ForEach(words, word =>
            {
                Console.WriteLine($"{word} has length {word.Length}");
            });


            Console.ReadKey();
        }
    }
}
