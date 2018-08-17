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
            // ParallelMergeOptions.FullyBuffered : 모든 아이템이 다 계산이 되면 결과를 보여준다
            // ParallelMergeOptions.NotBuffered : 계산이 되는대로 바로바로 보여준다
            var numbers = Enumerable.Range(1, 20).ToArray();
            var results = numbers.AsParallel()
                .WithMergeOptions(ParallelMergeOptions.NotBuffered)
                .Select(x =>
            {
                var result = Math.Log10(x);
                Console.Write($"P {result}\t");
                return result;
            });

            Console.WriteLine();
            foreach (var result in results)
            {
                Console.WriteLine($"C {result}\t");
            }
            Console.ReadKey();
        }
    }
}
