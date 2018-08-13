using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ChildTaskTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var parent = new Task(() =>
            {
                var child = new Task(() =>
                {
                    Console.WriteLine("Child task starting");
                    Thread.Sleep(3000);
                    throw new Exception();
                    Console.WriteLine("Child task finishing");

                }, TaskCreationOptions.AttachedToParent);

                var completionHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Hooray, task {t.Id}'s status is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion);

                var failHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"opps, task {t.Id}'s status is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted);

                child.Start();
            });
            parent.Start();

            try
            {
                parent.Wait();
            }
            catch(AggregateException ae)
            {
                ae.Handle(e => true);
            }
            Console.ReadKey();
        }
    }
}
