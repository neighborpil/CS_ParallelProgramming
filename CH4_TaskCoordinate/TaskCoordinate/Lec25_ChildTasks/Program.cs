using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lec25_ChildTasks
{
    class Program
    {
        static void Main(string[] args)
        {
            var parent = new Task(() =>
            {
                //detached
                var child = new Task(() =>
                {
                    Console.WriteLine("Child task starting");
                    Thread.Sleep(3000);
                    //  throw new Exception();
                    Console.WriteLine("Child task finishing");
                }, TaskCreationOptions.AttachedToParent);

                var completionHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Hooray, task {t.Id}'s status is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnRanToCompletion); // 성공시만

                var failHandler = child.ContinueWith(t =>
                {
                    Console.WriteLine($"Opps, task {t.Id}'s status is {t.Status}");
                }, TaskContinuationOptions.AttachedToParent | TaskContinuationOptions.OnlyOnFaulted); // 실패시만

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
