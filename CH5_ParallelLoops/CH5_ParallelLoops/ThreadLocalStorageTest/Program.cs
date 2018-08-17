using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadLocalStorageTest
{
    class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;

            Parallel.For(0, 1001,
                localInit: () => 0,
                body: (int x, ParallelLoopState state, int tls) =>
                {
                    tls += x;
                    return tls;
                },
                localFinally: (int partialSum) =>
                {
                    Interlocked.Add(ref sum, partialSum);
                });
            Console.WriteLine($"Sum : {sum}");
            Console.ReadKey();
        }
    }
}
