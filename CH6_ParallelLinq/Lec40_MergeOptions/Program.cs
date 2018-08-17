using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec40_MergeOptions
{
    class Program
    {
        static void Main(string[] args)
        {
            var numbers = Enumerable.Range(1, 20).ToArray();
            var results = numbers.AsParallel()
                .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                .Select(x =>
            {
                var result = Math.Log10(x);
                Console.Write($"P {result}\t");
                return result;
            });

            foreach (var result in results)
            {
                Console.WriteLine($"C {result}\t");
            }
        }
    }
}
