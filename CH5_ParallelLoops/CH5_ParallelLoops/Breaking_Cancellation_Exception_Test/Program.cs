using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Breaking_Cancellation_Exception_Test
{
    class Program
    {
        static ParallelLoopResult result;

        static void Main(string[] args)
        {
            try
            {
                Demo();
            }
            catch(AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine(e.Message);
                    return true;
                });
            }
            catch(OperationCanceledException op)
            {
                Console.WriteLine(op.Message);
            }

            Console.ReadKey();
        }

        private static void Demo()
        {
            var cts = new CancellationTokenSource();

            ParallelOptions po = new ParallelOptions();
            po.CancellationToken = cts.Token;

            result = Parallel.For(0, 20, po, (i, state) =>
            {
                Console.WriteLine($"{i}[{Task.CurrentId}]");
                if(i == 10)
                {
                    //state.Stop(); 
                    state.Break();
                    //cts.Cancel();
                }
            });

            Console.WriteLine();
            Console.WriteLine($"was completed? {result.IsCompleted}");
            if(result.LowestBreakIteration.HasValue)
                Console.WriteLine($"Lowest break iteration is {result.LowestBreakIteration}");
        }
    }
}
