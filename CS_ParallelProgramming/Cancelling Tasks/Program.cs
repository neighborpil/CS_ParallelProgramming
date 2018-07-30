using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Cancelling_Tasks
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
            //        #region soft way of cancelling

            //        //if (token.IsCancellationRequested)
            //        //    break;
            //        //else
            //        //    Console.WriteLine($"{i++}\t");
            //        #endregion
            //        #region canonical way of cancelling
            //        //if (token.IsCancellationRequested)
            //        //{
            //        //    throw new OperationCanceledException;
            //        //} 
            //        //else
            //        //    Console.WriteLine($"{i++}\t");
            //        #endregion
            //        token.ThrowIfCancellationRequested(); // canonical way
            //        Console.WriteLine($"{i++}\t");
            //    }
            //}, token);
            //t.Start();

            //Task.Factory.StartNew(() =>
            //{
            //    token.WaitHandle.WaitOne();
            //    Console.WriteLine("Wait handle released, cancelation was requested");
            //});

            //Console.ReadKey();
            //cts.Cancel();

            var planned = new CancellationTokenSource();
            var preventative = new CancellationTokenSource();
            var emergency = new CancellationTokenSource();

            var paranoid = CancellationTokenSource.CreateLinkedTokenSource(
                planned.Token, preventative.Token, emergency.Token);

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

            Console.WriteLine("Main program done.");
            Console.ReadKey(); 
        }
    }
}
