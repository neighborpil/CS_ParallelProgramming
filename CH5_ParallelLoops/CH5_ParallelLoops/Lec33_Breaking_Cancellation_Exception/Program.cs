using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lec33_Breaking_Cancellation_Exception
{
    class Program
    {
        private static ParallelLoopResult result;
        public static void Demo()
        {
            var cts = new CancellationTokenSource();

            ParallelOptions po = new ParallelOptions();
            po.CancellationToken = cts.Token;

            // 정지하는 법 : 
            result = Parallel.For(0, 20, po, (int x, ParallelLoopState state) =>
            {
                Console.WriteLine($"{x}[{Task.CurrentId}]\t");

                if(x == 10)
                {
                    // 바로 중지되지는 않는다. 새로 생성되는 task를 정지시킬 뿐이다
                    // 기존에 있던 task에서 이미 작업을 다끝냈을 수도 있다
                    // state.Stop(); // 중지
                    //state.Break(); // iteration을 중지하는 방법, break와 비슷하다
                    //throw new Exception();
                    cts.Cancel();
                }
            });

            Console.WriteLine();
            Console.WriteLine($"was loop completed? {result.IsCompleted}");
            if(result.LowestBreakIteration.HasValue)
                Console.WriteLine($"Lowest break iteration is {result.LowestBreakIteration}");
        }
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
            catch(OperationCanceledException oe)
            {
                Console.WriteLine(oe.Message);
            }
            Console.ReadKey();
        }
    }
}
