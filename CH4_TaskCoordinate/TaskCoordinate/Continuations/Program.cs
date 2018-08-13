using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Continuations
{
    class Program
    {
        static void Main(string[] args)
        {
            var task = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("Boiling Water");
            });

            var task2 = task.ContinueWith(t =>
            {
                Console.WriteLine($"Completed task {t.Id}, pour into cup");
            });

            task2.Wait();

            Console.ReadKey();
        }
    }
}
