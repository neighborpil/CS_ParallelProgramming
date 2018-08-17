using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeOptions
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Enumerable.Range(1, 20).ToArray();
            Stopwatch watch = new Stopwatch();
            watch.Start();
            #region without Parallel
            //var results = numbers
            //        .Select(x =>
            //        {
            //            var result = Math.Log10(x);
            //            Console.WriteLine($"P {result}");
            //            return result;
            //        });

            //Console.WriteLine();
            //foreach (var result in results)
            //{
            //    Console.WriteLine($"C {result}\t");
            //} 
            #endregion

            #region parallel

            var results = numbers.AsParallel()
                .WithMergeOptions(ParallelMergeOptions.FullyBuffered)
                .Select(x =>
                {
                    var result = Math.Log10(x);
                    Console.WriteLine($"P {result}");
                    return result;
                });

            Console.WriteLine();
            foreach (var result in results)
            {
                Console.WriteLine($"C {result}\t");
            }


            #endregion
            watch.Stop();
            Console.WriteLine(watch.Elapsed);

            Console.ReadKey();
        }
    }
}
