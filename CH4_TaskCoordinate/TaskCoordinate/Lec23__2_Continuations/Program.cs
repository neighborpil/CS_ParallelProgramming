using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec23__2_Continuations
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = Task.Factory.StartNew(() => "Task 1");
            var task2 = Task.Factory.StartNew(() => "Task 2");

            //  ContinueWhenAll : 모두 기다린다
            //var task3 = Task.Factory.ContinueWhenAll(new[] { task, task2 }, tasks =>
            // {
            //     Console.WriteLine("Task completed!");
            //     foreach (var t in tasks)
            //         Console.WriteLine(" - " + t.Result);
            //     Console.WriteLine("All tasks done");
            // });
            //task3.Wait();


            // ContinueWhenAny : 하나라도 되면 ㄱㄱ
            var task3 = Task.Factory.ContinueWhenAny(new[] { task, task2 }, t =>
            {
                Console.WriteLine("Task completed!");
                //foreach (var t in tasks)
                    Console.WriteLine(" - " + t.Result);
                Console.WriteLine("All tasks done");
            });
            task3.Wait();

            Console.ReadKey();
        }
    }
}
