using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Test2_Cancelling
{
    class Program
    {
        static void Main(string[] args)
        {
            //var cts = new CancellationTokenSource();
            //var token = cts.Token;

            //token.Register(() =>
            //{
            //    Console.WriteLine("Cancelation has been requested");
            //});

            //var t = new Task(() =>
            //{
            //    int i = 0;
            //    while (true)
            //    {
            //        //if (token.IsCancellationRequested)
            //        //    break;
            //        //else
            //        //    Console.WriteLine($"{i++}");

            //        //if (token.IsCancellationRequested)
            //        //    throw new OperationCanceledException;
            //        //else
            //        //    Console.WriteLine($"{i++}");

            //        token.ThrowIfCancellationRequested();
            //        Console.WriteLine($"{i++}");
            //    }
            //}, token);
            //t.Start();

            //Console.ReadKey();
            //cts.Cancel();

            var planned = new CancellationTokenSource();
            var preventive = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();

            var paranoid = CancellationTokenSource.CreateLinkedTokenSource(
                planned.Token, preventive.Token, emergency.Token);

            Task.Factory.StartNew(() =>
            {
                int i = 0;
                while (true)
                {
                    paranoid.Token.ThrowIfCancellationRequested();
                    Console.WriteLine($"{i++}");
                    Thread.Sleep(1000);
                }
            }, paranoid.Token);

            Console.ReadKey();
            emergency.Cancel();

            Console.WriteLine("Main Program done");
            Console.ReadKey();
        }
    }
}
