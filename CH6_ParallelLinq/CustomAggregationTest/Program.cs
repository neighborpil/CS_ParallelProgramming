using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomAggregationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var sum1 = Enumerable.Range(0, 1000)
                .Aggregate(0, (i, acc) => acc += (int)Math.Pow(i, 2));
            Console.WriteLine($"Sum1 = {sum1}");

            var sum = ParallelEnumerable.Range(1, 1000)
                .Aggregate(
                 0,
                 (partialSum, i) => partialSum += i,
                 (total, subtotal) => total += subtotal,
                 i => i);
            Console.WriteLine($"Sum = {sum}");

            Console.ReadKey();

        }
    }
}
