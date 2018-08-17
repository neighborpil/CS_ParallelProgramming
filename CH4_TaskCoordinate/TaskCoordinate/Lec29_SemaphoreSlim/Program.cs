using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lec29_SemaphoreSlim
{
    class Program
    {
        static void Main(string[] args)
        {
            var semaphore = new SemaphoreSlim(2, 10); // 시작시의 카운트, 최대 카운트

            for (int i = 0; i < 20; i++)
            {
                Task.Factory.StartNew(() =>
                {
                    Console.WriteLine($"Entering Task {Task.CurrentId}");
                    semaphore.Wait(); // release count -- (2 - 1)
                    Console.WriteLine($"Processing Task {Task.CurrentId}");

                });
            }

            while(semaphore.CurrentCount <= 2)
            {
                Console.WriteLine($"Semaphore count : {semaphore.CurrentCount}");
                Console.ReadKey();
                semaphore.Release(2); //release count += 2
            }
        }
    }
}
