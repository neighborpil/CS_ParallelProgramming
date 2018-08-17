using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentBag
{
    class Program
    {
        static void Main(string[] args)
        {
            var bag = new ConcurrentBag<int>();
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                var i1 = i;
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    bag.Add(i);
                    Console.WriteLine($"{Task.CurrentId} has added {i1}");
                    if(bag.TryPeek(out int result))
                    {
                        Console.WriteLine($"{Task.CurrentId} has peeked the value{result}");

                    }
                }));
            }
            Task.WaitAll(tasks.ToArray());

            if(bag.TryTake(out int last))
            {
                Console.WriteLine($"I got {last}");

            }
            Console.ReadKey();

        }
    }
}
