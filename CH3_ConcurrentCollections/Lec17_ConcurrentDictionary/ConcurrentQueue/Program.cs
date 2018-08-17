using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcurrentQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            var queue = new ConcurrentQueue<int>();

            queue.Enqueue(1);
            queue.Enqueue(2);

            if(queue.TryDequeue(out int result))
            {
                Console.WriteLine($"Dequeued : {result}");
            }

            if(queue.TryPeek(out result))
            {
                Console.WriteLine($"Peeked : {result}");
            }

            Console.ReadKey();
        }
    }
}
