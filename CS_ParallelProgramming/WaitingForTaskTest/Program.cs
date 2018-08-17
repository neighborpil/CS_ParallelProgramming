using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WaitingForTaskTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var token = cts.Token;

            var t1 = new Task(() =>
            {
                Console.WriteLine("t1 starts working");

                Enumerable.Range(0, 5).ToList().ForEach(i => token.WaitHandle.WaitOne(1000));
                Console.WriteLine("I'm done");

            }, token);
            t1.Start();

            Task t2 = Task.Factory.StartNew(() => token.WaitHandle.WaitOne(3000), token);

            Task.WaitAll(new[] { t1, t2 }, 4000, token);

            Console.WriteLine($"Task1 status : {t1.Status}");
            Console.WriteLine($"Task2 status : {t2.Status}");

            Console.WriteLine("Main program done");
            Console.ReadKey();

        }
    }
}
