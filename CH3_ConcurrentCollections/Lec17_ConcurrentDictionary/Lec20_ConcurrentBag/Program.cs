using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lec20_ConcurrentBag
{
    class Program
    {
        static void Main(string[] args)
        {
            // stack FILO
            // queue FIFO
            // bag no ordering

            var bag = new ConcurrentBag<int>();
            var tasks = new List<Task>();
            for (int i = 0; i < 10; i++)
            {
                var i1 = i;
                tasks.Add(Task.Factory.StartNew(() =>
                {
                    bag.Add(i1); //concurrent bag은 스레드를 기억하여 스레드별로 아이템을 보관한다
                    Console.WriteLine($"{Task.CurrentId} has added {i1}");
                    int result;
                    if(bag.TryPeek(out result)) // 스레드에서 저장한 것만 보인다
                    {
                        Console.WriteLine($"{Task.CurrentId} has peeked the value{result}");
                    }
                }));
            }
            Task.WaitAll(tasks.ToArray());

            int last;
            if(bag.TryTake(out last)) // 순서를 보장하지 않는다. 단지 저장한 것중 순서 없이 무작위로 반환한다. 하지만 속도는 빠르다
            {
                Console.WriteLine($"I got {last}");
            }

            Console.ReadKey();
        }
    }
}
