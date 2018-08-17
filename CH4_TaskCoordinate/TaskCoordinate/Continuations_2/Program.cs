using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Continuations_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = Task.Factory.StartNew(() => "Task1");
            var task2 = Task.Factory.StartNew(() => "Task2");

            //var task3 = Task.Factory.ContinueWhenAll(new []{ task, task2 }, tasks =>
            // {
            //     Console.WriteLine("Task completed");
            //     foreach (var t in tasks)
            //         Console.WriteLine($" - {t.Result}");
            //     Console.WriteLine("All tasks done");
            // });
            //task3.Wait();

            var task4 = Task.Factory.ContinueWhenAny(new[] { task, task2 }, t =>
             {
                 Console.WriteLine("Task Completed");
                 Console.WriteLine($" - {t.Result}");
                 Console.WriteLine("All tasks done");
             });

            task4.Wait();
            Console.ReadKey();
        }
    }
}
