using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _18_ConcurrentQueue
{
    class Program
    {
        static void Main(string[] args)
        {
            var q = new ConcurrentQueue<int>();
            q.Enqueue(1);
            q.Enqueue(2);

            // 2 1 <- front
            int result;
            if (q.TryDequeue(out result))
            {
                Console.WriteLine($"Removed element {result}");
            }

            if (q.TryPeek(out result))
            {
                Console.WriteLine($"Front element is {result}");
            }


            Console.ReadKey();
        }
    }
}
