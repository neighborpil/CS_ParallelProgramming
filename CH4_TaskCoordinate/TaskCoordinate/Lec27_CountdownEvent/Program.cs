using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lec27_CountdownEvent
{
    class Program
    {
        private static int taskCount = 5;
        static CountdownEvent cte = new CountdownEvent(taskCount);
        static Random random = new Random();
        static void Main(string[] args)
        {
            for (int i = 0; i < taskCount; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Entering task {Task.CurrentId}");
                    Thread.Sleep(random.Next(3000));
                    cte.Signal(); // countdown이 끝났다는걸 알림
                    Console.WriteLine($"Exiting task {Task.CurrentId}");
                });
            }

            var finishTask = Task.Factory.StartNew(() =>
            {
                Console.WriteLine($"Waiting for other tasks to complete in {Task.CurrentId}");
                cte.Wait(); //blocking
                Console.WriteLine("All tasks completed");
            });
            finishTask.Wait();

            Console.ReadKey();
        }
    }
}
