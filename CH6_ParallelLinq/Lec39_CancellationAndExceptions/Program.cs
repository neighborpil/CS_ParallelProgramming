using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lec39_CancellationAndExceptions
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();

            var items = ParallelEnumerable.Range(1, 20);

            var results = items.WithCancellation(cts.Token).Select(i =>
            {
                double result = Math.Log10(i);

                //if (result > 1)
                //    throw new InvalidOperationException();

                Console.WriteLine($"i = {i}, tid = {Task.CurrentId}");
                return result;
            });

            try // 지연실행이 되기 때문에 여기서 trycatch를 해준다
            {
                foreach (var c in results)
                {
                    if (c > 1)
                        cts.Cancel();

                    Console.WriteLine($"result = {c}");
                }
            }
            catch(AggregateException ae) 
            {
                ae.Handle(e =>
                {
                    Console.WriteLine($"{e.GetType().Name}: {e.Message}");
                    return true;
                });
            }
            catch(OperationCanceledException e)
            {
                Console.WriteLine("Canceled");
            }
            Console.ReadKey();
        }
    }
}
