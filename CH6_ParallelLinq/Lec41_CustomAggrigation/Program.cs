﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec41_CustomAggrigation
{
    class Program
    {
        static void Main(string[] args)
        {
            var sum = Enumerable.Range(1, 1000).Sum();
            Console.WriteLine($"Sum = {sum}");

            var sum1 = Enumerable.Range(1, 1000)
                .Aggregate(0, (i, acc) => i + acc);
            Console.WriteLine($"Sum1 = {sum1}");

            var sum2 = ParallelEnumerable.Range(1, 1000)
                .Aggregate(
                 0,
                 (partialSum, i) => partialSum += i,
                 (total, subtotal) => total += subtotal,
                 i => i);
            Console.WriteLine($"Sum2 = {sum2}");


            Console.ReadKey();
        }
    }
}
